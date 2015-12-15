using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UltimateFestivalOrganizer.BusinessLogik;
using UltimateFestivalOrganizer.Commander.Util;
using UltimateFestivalOrganizer.DAL.Common.Domain;

namespace UltimateFestivalOrganizer.Commander.ViewModels
{
    public class CatagoryAdministrationVM : BaseVM,ITabView
    {
        private IAdministrationServices administrationService;
        private CatagoryVM currentCatagory;
        public ObservableCollection<CatagoryVM> Catagories { get; set; }
        public ICommand Add { get; set; }


        public CatagoryAdministrationVM(IAdministrationServices service)
        {
            this.administrationService = service;
            Catagories = new ObservableCollection<CatagoryVM>();
            Add = new RelayCommand((x) =>
            {
                CurrentCatagory = new CatagoryVM(new Catagory(), administrationService);
                Catagories.Add(CurrentCatagory);
                RaisePropertyChangedEvent(nameof(Catagories));
                RaisePropertyChangedEvent(nameof(CurrentCatagory));
            });
            AppMessages.CatagoryChanged.Register(this, (type) => 
            {
                if (type == AppMessages.ChangeType.Remove)
                {
                    this.LoadData();
                }
                RaisePropertyChangedEvent(nameof(Catagories));
                RaisePropertyChangedEvent(nameof(CurrentCatagory));
                
            });


            this.LoadData();
        }

        public CatagoryVM CurrentCatagory
        {
            get { return this.currentCatagory; }
            set
            {
                if(this.currentCatagory != value)
                {
                    this.currentCatagory = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

        private void LoadData()
        {
            Catagories.Clear();
            IList<Catagory> catList = this.administrationService.GetCatagories();
            foreach(Catagory cat in catList)
            {
                Catagories.Add(new CatagoryVM(cat, administrationService));
            }
            CurrentCatagory = Catagories.First();
            RaisePropertyChangedEvent(nameof(Catagories));
            RaisePropertyChangedEvent(nameof(CurrentCatagory));
        }

        public void LoadItems()
        {
            this.LoadData();
        }
    }
}
