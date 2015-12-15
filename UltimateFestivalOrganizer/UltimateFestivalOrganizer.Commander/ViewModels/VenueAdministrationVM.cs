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
    public class VenueAdministrationVM : BaseVM, ITabView
    {
        private IAdministrationServices administrationService;
        private VenueVM currentVenue;
        public ObservableCollection<VenueVM> Venues { get; set; }
        public ICommand Add {get; private set;}


        public VenueAdministrationVM(IAdministrationServices service)
        {
            this.administrationService = service;
            Venues = new ObservableCollection<VenueVM>();
            Add = new RelayCommand(c =>
            {
                CurrentVenue = new VenueVM(new Venue(), administrationService);
                Venues.Add(CurrentVenue);
                RaisePropertyChangedEvent(nameof(Venues));
                RaisePropertyChangedEvent(nameof(CurrentVenue));
            });

            AppMessages.VenueChanged.Register(this, (type) =>
            {
                if (type == AppMessages.ChangeType.Remove)
                {
                    LoadItems();
                }
                RaisePropertyChangedEvent(nameof(CurrentVenue));
                RaisePropertyChangedEvent(nameof(Venues));
            });

            this.LoadItems();
        }

        public void LoadItems()
        {
            Venues.Clear();
            foreach (Venue v in administrationService.GetVenues())
            {
                Venues.Add(new VenueVM(v, administrationService));
            }
            CurrentVenue = Venues.First();
            RaisePropertyChangedEvent(nameof(Venues));
            RaisePropertyChangedEvent(nameof(CurrentVenue));

        }

        public VenueVM CurrentVenue
        {
            get { return this.currentVenue; }
            set
            {
                if(this.currentVenue != value)
                {
                    this.currentVenue = value;
                    RaisePropertyChangedEvent();
                }
            }
        }
    }
}
