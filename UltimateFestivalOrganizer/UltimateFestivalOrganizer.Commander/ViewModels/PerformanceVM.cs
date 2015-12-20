using MvvmValidation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using UltimateFestivalOrganizer.BusinessLogik;
using UltimateFestivalOrganizer.Commander.Util;
using UltimateFestivalOrganizer.DAL.Common.Domain;

namespace UltimateFestivalOrganizer.Commander.ViewModels
{
    public class PerformanceVM : BaseVM
    {
        private Venue venue;
        private PerformanceAdministrationVM parent;
        public Performance Col1 { get; private set; }
        public Performance Col2 { get; private set; }
        public Performance Col3 { get; private set; }
        public Performance Col4 { get; private set; }
        public Performance Col5 { get; private set; }
        public IAdministrationServices administrationService;
        public ObservableCollection<Artist> Artists { get; private set; }
        public ICommand RemoveEntryCol1 { get; private set; }
        public ICommand RemoveEntryCol2 { get; private set; }
        public ICommand RemoveEntryCol3 { get; private set; }
        public ICommand RemoveEntryCol4 { get; private set; }
        public ICommand RemoveEntryCol5 { get; private set; }
        public ICommand SendEmailCol1 { get; private set; }
        public ICommand SendEmailCol2 { get; private set; }
        public ICommand SendEmailCol3 { get; private set; }
        public ICommand SendEmailCol4 { get; private set; }
        public ICommand SendEmailCol5 { get; private set; }

        public PerformanceVM(Venue v, Performance p1, Performance p2, Performance p3, Performance p4, Performance p5,IAdministrationServices service, PerformanceAdministrationVM parent)
        {
            this.Artists = new ObservableCollection<Artist>();
            this.administrationService = service;
            this.parent = parent;
            venue = v;
            Col1 = p1;
            Col2 = p2;
            Col3 = p3;
            Col4 = p4;
            Col5 = p5;

            this.Artists.Clear();
            IList<Artist> arts = administrationService.GetArtists();
            foreach (Artist artist in arts)
            {
                Artists.Add(artist);
            }

            RemoveEntryCol1 = new RelayCommand(c => {
                Col1.Artist = null;
                RaisePropertyChangedEvent(nameof(ArtistCol1));
                RaisePropertyChangedEvent(nameof(ArtistNameCol1));
                RaisePropertyChangedEvent(nameof(CountryCol1));
                RaisePropertyChangedEvent(nameof(CatagoryCol1));
                RaisePropertyChangedEvent(nameof(ColorCol1));
            });
            RemoveEntryCol2 = new RelayCommand(c => {
                Col2.Artist = null;
                RaisePropertyChangedEvent(nameof(ArtistCol2));
                RaisePropertyChangedEvent(nameof(ArtistNameCol2));
                RaisePropertyChangedEvent(nameof(CountryCol2));
                RaisePropertyChangedEvent(nameof(CatagoryCol2));
                RaisePropertyChangedEvent(nameof(ColorCol2));
            });
            RemoveEntryCol3 = new RelayCommand(c => {
                Col3.Artist = null;
                RaisePropertyChangedEvent(nameof(ArtistCol3));
                RaisePropertyChangedEvent(nameof(ArtistNameCol3));
                RaisePropertyChangedEvent(nameof(CountryCol3));
                RaisePropertyChangedEvent(nameof(CatagoryCol3));
                RaisePropertyChangedEvent(nameof(ColorCol3));
            });
            RemoveEntryCol4 = new RelayCommand(c => {
                Col4.Artist = null;
                RaisePropertyChangedEvent(nameof(ArtistCol4));
                RaisePropertyChangedEvent(nameof(ArtistNameCol4));
                RaisePropertyChangedEvent(nameof(CountryCol4));
                RaisePropertyChangedEvent(nameof(CatagoryCol4));
                RaisePropertyChangedEvent(nameof(ColorCol4));
            });
            RemoveEntryCol5 = new RelayCommand(c => {
                Col5.Artist = null;
                RaisePropertyChangedEvent(nameof(ArtistCol5));
                RaisePropertyChangedEvent(nameof(ArtistNameCol5));
                RaisePropertyChangedEvent(nameof(CountryCol5));
                RaisePropertyChangedEvent(nameof(CatagoryCol5));
                RaisePropertyChangedEvent(nameof(ColorCol5));
            });

            SendEmailCol1 = new RelayCommand(c =>
            {
                IList<Performance> toSend = new List<Performance>();
                toSend.Add(Col1);
                administrationService.SendMail(toSend,toSend);
                AppMessages.ShowSuccessMessage.Send("Mail sent");
            });
            SendEmailCol2 = new RelayCommand(c =>
            {
                IList<Performance> toSend = new List<Performance>();
                toSend.Add(Col2);
                administrationService.SendMail(toSend, toSend);
                AppMessages.ShowSuccessMessage.Send("Mail sent");
            });
            SendEmailCol3 = new RelayCommand(c =>
            {
                IList<Performance> toSend = new List<Performance>();
                toSend.Add(Col3);
                administrationService.SendMail(toSend, toSend);
                AppMessages.ShowSuccessMessage.Send("Mail sent");
            });
            SendEmailCol4 = new RelayCommand(c =>
            {
                IList<Performance> toSend = new List<Performance>();
                toSend.Add(Col4);
                administrationService.SendMail(toSend, toSend);
                AppMessages.ShowSuccessMessage.Send("Mail sent");
            });
            SendEmailCol5 = new RelayCommand(c =>
            {
                IList<Performance> toSend = new List<Performance>();
                toSend.Add(Col5);
                administrationService.SendMail(toSend, toSend);
                AppMessages.ShowSuccessMessage.Send("Mail sent");
            });
        }

        public void GroupCheckBox()
        {
            var view = CollectionViewSource.GetDefaultView(Artists);
            view.GroupDescriptions.Clear();
            view.GroupDescriptions.Add(new PropertyGroupDescription("Catagory.Name"));
        }

        public bool Validate()
        {
            bool success = true;
            Validator.RemoveAllRules();
            if (!this.ValidateCol1()) { success = false; }
            if (!this.ValidateCol2()) { success = false; }
            if (!this.ValidateCol3()) { success = false; }
            if (!this.ValidateCol4()) { success = false; }
            if (!this.ValidateCol5()) { success = false; }
            return success;
        }

        private IList<Performance> GetPerformancesToValidate()
        {
            IList<Performance> performancesToValidate = new List<Performance>();
            foreach (PerformanceVM vm in parent.Performances)
            {
                if (vm.Col1.Artist != null && vm.Col1.Venue != null)
                {
                    performancesToValidate.Add(vm.Col1);
                }
                if (vm.Col2.Artist != null && vm.Col2.Venue != null)
                {
                    performancesToValidate.Add(vm.Col2);
                }
                if (vm.Col3.Artist != null && vm.Col3.Venue != null)
                {
                    performancesToValidate.Add(vm.Col3);
                }
                if (vm.Col4.Artist != null && vm.Col4.Venue != null)
                {
                    performancesToValidate.Add(vm.Col4);
                }
                if (vm.Col5.Artist != null && vm.Col5.Venue != null)
                {
                    performancesToValidate.Add(vm.Col5);
                }
            }
            return performancesToValidate;
        }
        private bool ValidateOneItem(Performance current)
        {
            foreach (Performance p in this.GetPerformancesToValidate())
            {

                if (p?.Artist?.Id == current?.Artist?.Id && p != current)
                {
                    if (p.StagingTime == current.StagingTime || p.StagingTime.AddHours(-1) == current.StagingTime || current.StagingTime.AddHours(-1) == p.StagingTime)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        
        private bool ValidateCol1()
        {
            if (!ValidateOneItem(Col1))
            {
                Validator.AddRule(() => ArtistCol1, () => RuleResult.Assert(false, "Cannot play twice"));
                Validator.Validate(() => ArtistCol1);
                RaisePropertyChangedEvent("ArtistCol1");
                return false;
            }
            return true;
        }
        private bool ValidateCol2()
        {
            if (!ValidateOneItem(Col2))
            {
                Validator.AddRule(() => ArtistCol2, () => RuleResult.Assert(false, "Cannot play twice"));
                Validator.Validate(() => ArtistCol2);
                RaisePropertyChangedEvent("ArtistCol2");
                return false;
            }
            return true;
        }
        private bool ValidateCol3()
        {
            if (!ValidateOneItem(Col3))
            {
                Validator.AddRule(() => ArtistCol3, () => RuleResult.Assert(false, "Cannot play twice"));
                Validator.Validate(() => ArtistCol3);
                RaisePropertyChangedEvent("ArtistCol3");
                return false;
            }
            return true;
        }
        private bool ValidateCol4()
        {
            if (!ValidateOneItem(Col4))
            {
                Validator.AddRule(() => ArtistCol4, () => RuleResult.Assert(false, "Cannot play twice"));
                Validator.Validate(() => ArtistCol4);
                RaisePropertyChangedEvent("ArtistCol4");
                return false;
            }
            return true;
        }
        private bool ValidateCol5()
        {
            if (!ValidateOneItem(Col5))
            {
                Validator.AddRule(() => ArtistCol5, () => RuleResult.Assert(false, "Cannot play twice"));
                Validator.Validate(() => ArtistCol5);
                RaisePropertyChangedEvent("ArtistCol5");
                return false;
            }
            return true;
        }

        public Artist ArtistCol1
        {
            get
            {
                if(Col1.Artist != null)
                {
                    return Artists.Where(a => a.Id == Col1.Artist.Id).First();
                }
                return null;
            }
            set
            {
                if(Col1.Artist != value)
                {
                    Col1.Artist = value;
                    Col1.Venue = venue;
                    RaisePropertyChangedEvent();
                    RaisePropertyChangedEvent(nameof(Col1));
                    RaisePropertyChangedEvent("ArtistNameCol1");
                    RaisePropertyChangedEvent("CountryCol1");
                    RaisePropertyChangedEvent("CatagoryCol1");
                    RaisePropertyChangedEvent("ColorCol1");
                    this.Validate();
                }
            }
        }
        public Artist ArtistCol2
        {
            get
            {
                if (Col2.Artist != null)
                {
                    return Artists.Where(a => a.Id == Col2.Artist.Id).First();
                }
                return null;
            }
            set
            {
                if (Col2.Artist != value)
                {
                    Col2.Artist = value;
                    Col2.Venue = venue;
                    RaisePropertyChangedEvent();
                    RaisePropertyChangedEvent(nameof(Col2));
                    RaisePropertyChangedEvent("ArtistNameCol2");
                    RaisePropertyChangedEvent("CountryCol2");
                    RaisePropertyChangedEvent("CatagoryCol2");
                    RaisePropertyChangedEvent("ColorCol2");
                    this.Validate();
                }
            }
        }
        public Artist ArtistCol3
        {
            get
            {
                if (Col3.Artist != null)
                {
                    return Artists.Where(a => a.Id == Col3.Artist.Id).First();
                }
                return null;
            }
            set
            {
                if (Col3.Artist != value)
                {
                    Col3.Artist = value;
                    Col3.Venue = venue;
                    RaisePropertyChangedEvent();
                    RaisePropertyChangedEvent(nameof(Col3));
                    RaisePropertyChangedEvent("ArtistNameCol3");
                    RaisePropertyChangedEvent("CountryCol3");
                    RaisePropertyChangedEvent("CatagoryCol3");
                    RaisePropertyChangedEvent("ColorCol3");
                    this.Validate();
                }
            }
        }
        public Artist ArtistCol4
        {
            get
            {
                if (Col4.Artist != null)
                {
                    return Artists.Where(a => a.Id == Col4.Artist.Id).First();
                }
                return null;
            }
            set
            {
                if (Col4.Artist != value)
                {
                    Col4.Artist = value;
                    Col4.Venue = venue;
                    RaisePropertyChangedEvent();
                    RaisePropertyChangedEvent(nameof(Col4));
                    RaisePropertyChangedEvent("ArtistNameCol4");
                    RaisePropertyChangedEvent("CountryCol4");
                    RaisePropertyChangedEvent("CatagoryCol4");
                    RaisePropertyChangedEvent("ColorCol4");
                    this.Validate();
                }
            }
        }
        public Artist ArtistCol5
        {
            get
            {
                if (Col5.Artist != null)
                {
                    return Artists.Where(a => a.Id == Col5.Artist.Id).First();
                }
                return null;
            }
            set
            {
                if (Col5.Artist != value)
                {
                    Col5.Artist = value;
                    Col5.Venue = venue;
                    RaisePropertyChangedEvent();
                    RaisePropertyChangedEvent(nameof(Col5));
                    RaisePropertyChangedEvent("ArtistNameCol5");
                    RaisePropertyChangedEvent("CountryCol5");
                    RaisePropertyChangedEvent("CatagoryCol5");
                    RaisePropertyChangedEvent("ColorCol5");
                    this.Validate();
                }
            }
        }

        public string ArtistNameCol1
        {
            get { return this.Col1?.Artist?.Name; }
        }

        public string CountryCol1
        {
            get { return this.Col1?.Artist?.Country; }
        }
        public string CatagoryCol1
        {
            get { return this.Col1?.Artist?.Catagory?.Name; }
        }

        public string ArtistNameCol2
        {
            get { return this.Col2?.Artist?.Name; }
        }

        public string CountryCol2
        {
            get { return this.Col2?.Artist?.Country; }
        }
        public string CatagoryCol2
        {
            get { return this.Col2?.Artist?.Catagory?.Name; }
        }

        public string ArtistNameCol3
        {
            get { return this.Col3?.Artist?.Name; }
        }

        public string CountryCol3
        {
            get { return this.Col3?.Artist?.Country; }
        }
        public string CatagoryCol3
        {
            get { return this.Col3?.Artist?.Catagory?.Name; }
        }

        public string ArtistNameCol4
        {
            get { return this.Col4?.Artist?.Name; }
        }

        public string CountryCol4
        {
            get { return this.Col4?.Artist?.Country; }
        }
        public string CatagoryCol4
        {
            get { return this.Col4?.Artist?.Catagory?.Name; }
        }

        public string ArtistNameCol5
        {
            get { return this.Col5?.Artist?.Name; }
        }

        public string CountryCol5
        {
            get { return this.Col5?.Artist?.Country; }
        }
        public string CatagoryCol5
        {
            get { return this.Col5?.Artist?.Catagory?.Name; }
        }


        public string Venue
        {
            get { return venue.Description; }
        }

        public System.Windows.Media.Brush ColorCol1
        {

            get { return Util.Util.GetColorForId(Col1?.Artist?.Catagory?.Id); }
        }
        public System.Windows.Media.Brush ColorCol2
        {

            get { return Util.Util.GetColorForId(Col2?.Artist?.Catagory?.Id); }
        }
        public System.Windows.Media.Brush ColorCol3
        {

            get { return Util.Util.GetColorForId(Col3?.Artist?.Catagory?.Id); }
        }
        public System.Windows.Media.Brush ColorCol4
        {

            get { return Util.Util.GetColorForId(Col4?.Artist?.Catagory?.Id); }
        }
        public System.Windows.Media.Brush ColorCol5
        {

            get { return Util.Util.GetColorForId(Col5?.Artist?.Catagory?.Id); }
        }
    }
}
