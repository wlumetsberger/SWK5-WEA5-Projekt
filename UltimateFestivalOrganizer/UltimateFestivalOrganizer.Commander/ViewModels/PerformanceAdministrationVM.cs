using MvvmValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UltimateFestivalOrganizer.BusinessLogik;
using UltimateFestivalOrganizer.Commander.Util;
using UltimateFestivalOrganizer.DAL.Common.Domain;

namespace UltimateFestivalOrganizer.Commander.ViewModels
{
    public class PerformanceAdministrationVM : BaseVM,ITabView
    {
        private IAdministrationServices administrationService;
        private PerformanceVM currentPerformance;
        public ObservableCollection<PerformanceVM> Performances { get; set; }
        public ICommand ValidatePerformances { get; private set; }
        public ICommand SavePerformances { get; private set; }
        public ICommand SendMailToArtists { get; private set; }

        private DateTime currentDate;

        public PerformanceAdministrationVM(IAdministrationServices service)
        {
            this.administrationService = service;
            this.Performances = new ObservableCollection<PerformanceVM>();
            this.CurrentDate = DateTime.Now;
            this.LoadItems();

            AppMessages.ArtistChanged.Register(this,(chageType) => { this.LoadItems(); });
            AppMessages.CatagoryChanged.Register(this, (chageType) => { this.LoadItems(); });

            ValidatePerformances = new RelayCommand((c) =>
            {
                if (this.ValidateProgram())
                {
                    AppMessages.ShowSuccessMessage.Send("Program is Valid");
                }
                else
                {
                    AppMessages.ShowErrorMessage.Send("Program is not Valid");
                }
            });
            SavePerformances = new RelayCommand((c) =>
            {
                if (!this.ValidateProgram())
                {
                    AppMessages.ShowErrorMessage.Send("Cannot Save Invalid Program");
                    return;
                }
                administrationService.DeletePerformancesByDay(CurrentDate);
                foreach(PerformanceVM vm in Performances)
                {
                    SavePerformanceHelper(vm.Col1);
                    SavePerformanceHelper(vm.Col2);
                    SavePerformanceHelper(vm.Col3);
                    SavePerformanceHelper(vm.Col4);
                    SavePerformanceHelper(vm.Col5);
                   
                }
                AppMessages.ShowSuccessMessage.Send("Program Saved");
            });

            SendMailToArtists = new RelayCommand(c =>
            {
                if (!this.ValidateProgram())
                {
                    AppMessages.ShowErrorMessage.Send("Cannot Send Mails for Invalid Program");
                }
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler((sender, args) => {
                    this.SendMails();
                });
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((sender, args) => {
                    AppMessages.ShowSuccessMessage.Send("Mails sent");
                });
                worker.RunWorkerAsync(System.Reflection.Assembly.GetExecutingAssembly().Location);
                
            });
        }
        private void SendMails()
        {
            IList<Performance> performances = administrationService.GetPerformancesByDay(CurrentDate);
            IList<Performance> toSend = new List<Performance>();
            foreach (Performance p in this.GetAllActivePerformancesFromScreen())
            {
                bool found = false;
                foreach (Performance p2 in performances)
                {
                    if (p.Artist.Id == p2.Artist.Id)
                    {
                        found = true;
                        // check ob sich etwas verändert hat wenn ja sent to mail list
                        if (p.Venue?.Id != p2.Venue?.Id)
                        {
                            toSend.Add(p);
                        }
                        else if (p.StagingTime != p2.StagingTime)
                        {
                            toSend.Add(p);
                        }
                    }
                }
                if (!found)
                {
                    toSend.Add(p);
                }

            }
            // check old ones removed
            foreach (Performance p in performances)
            {
                bool found = false;
                foreach (Performance p2 in GetAllActivePerformancesFromScreen())
                {
                    if (p.Artist.Id == p2.Artist.Id)
                    {
                        found = true;
                    }
                }
                if (!found)
                {
                    toSend.Add(p);
                }
            }
            administrationService.SendMail(toSend, GetAllActivePerformancesFromScreen());
        }
        private IList<Performance> GetAllActivePerformancesFromScreen()
        {
            IList<Performance> p = new List<Performance>();
            foreach(PerformanceVM vm in Performances)
            {
                if(vm.Col1.Artist != null)
                {
                    p.Add(vm.Col1);
                }
                if (vm.Col2.Artist != null)
                {
                    p.Add(vm.Col2);
                }
                if (vm.Col3.Artist != null)
                {
                    p.Add(vm.Col3);

                }
                if (vm.Col4.Artist != null)
                {
                    p.Add(vm.Col4);
                }
                if (vm.Col5.Artist != null)
                {
                    p.Add(vm.Col5);
                }
            }
            return p;
        }

        private void SavePerformanceHelper(Performance current)
        {
            if (current.Artist != null && current.Artist.Id >= 0 && current.Venue != null && current.Venue.Id > 0)
            {
                administrationService.SavePerformance(current);
            }
        }

        public bool ValidateProgram()
        {
            bool success = true;
            foreach(PerformanceVM vm in Performances)
            {
                if (!vm.Validate())
                {
                    success = false;
                }
            }
            return success;
        }
           
        //Load Items Asynchronious 
        public void LoadItems()
        {
            IList<PerformanceVM> calculateModels = new List<PerformanceVM>();
            Performances.Clear();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler((sender, args) =>
            {
                IList<Venue> venues = administrationService.GetVenues();
                foreach (Venue venue in venues)
                {
                    IList<Performance> performances = administrationService.GetPerformancesByVenueAndDay(venue, CurrentDate);
                    if (performances.Count > 4)
                    {
                        return;
                    }
                    DateTime d = CurrentDate.Date;
                    Performance col1 = new Performance();
                    col1.StagingTime = d.AddHours(14);
                    col1.StagingTime = col1.StagingTime.AddMinutes(30);
                    Performance col2 = new Performance();
                    col2.StagingTime = d.AddHours(15);
                    col2.StagingTime = col2.StagingTime.AddMinutes(30);
                    Performance col3 = new Performance();
                    col3.StagingTime = d.AddHours(16);
                    col3.StagingTime = col3.StagingTime.AddMinutes(30);
                    Performance col4 = new Performance();
                    col4.StagingTime = d.AddHours(17);
                    col4.StagingTime = col4.StagingTime.AddMinutes(30);
                    Performance col5 = new Performance();
                    col5.StagingTime = d.AddHours(18);
                    col5.StagingTime = col5.StagingTime.AddMinutes(30);

                    foreach (Performance p in performances)
                    {
                        if (p.StagingTime.Hour >= 18 && p.StagingTime.Minute > 0)
                        {
                            col5 = p;
                        }
                        else if (p.StagingTime.Hour >= 17 && p.StagingTime.Minute > 0)
                        {
                            col4 = p;
                        }
                        else if (p.StagingTime.Hour >= 16 && p.StagingTime.Minute > 0)
                        {
                            col3 = p;
                        }
                        else if (p.StagingTime.Hour >= 15 && p.StagingTime.Minute > 0)
                        {
                            col2 = p;
                        }
                        else if (p.StagingTime.Hour >= 14)
                        {
                            col1 = p;
                        }
                    }

                    calculateModels.Add(new PerformanceVM(venue, col1, col2, col3, col4, col5, administrationService,this));
                   
                }
            });
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((sender, args) => {
                Performances.Clear();
                foreach(PerformanceVM vm in calculateModels)
                {
                    vm.GroupCheckBox();
                    Performances.Add(vm);
                }
                RaisePropertyChangedEvent(nameof(Performances));
            });
            worker.RunWorkerAsync(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        public DateTime CurrentDate
        {
            get { return currentDate; }
            set
            {
                if (currentDate != value)
                {
                    currentDate = value;
                    this.LoadItems();
                    RaisePropertyChangedEvent();
                }
            }
        }
        public PerformanceVM CurrentPerformance
        {
            get { return this.currentPerformance; }
            set
            {
                if(this.currentPerformance != value)
                {
                    this.currentPerformance = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

    }
}
