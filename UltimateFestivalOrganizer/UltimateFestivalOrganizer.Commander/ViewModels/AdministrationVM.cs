using DAL.Common.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using UltimateFestivalOrganizer.BusinessLogik;
using UltimateFestivalOrganizer.Commander.Util;

namespace UltimateFestivalOrganizer.Commander.ViewModels
{
    public class AdministrationVM : BaseVM
    {
        private IAdministrationServices administrationService;
        private Util.IIOService ioService;
        private ArtistAdministrationVM currentArtistAdministration;
        private CatagoryAdministrationVM currentCatagoryAdministration;
        private VenueAdministrationVM currentVenueAdministration;
        private PerformanceAdministrationVM currentPerformanceAdministration;

        public ICommand Login { get; private set; }
        private User user;
        public bool LoggedIn { get; set; }
        public string UserName { get; set; }
        public string LoginFailedMessage { get; set; }



        public AdministrationVM(IAdministrationServices service, IIOService ioService)
        {
            this.administrationService = service;
            this.ioService = ioService;
            this.currentArtistAdministration = new ArtistAdministrationVM(this.administrationService, ioService);
            this.currentCatagoryAdministration = new CatagoryAdministrationVM(this.administrationService);
            this.currentVenueAdministration = new VenueAdministrationVM(this.administrationService);
            this.currentPerformanceAdministration = new PerformanceAdministrationVM(this.administrationService);
            Login = new RelayCommand((c) => 
            {
                var box = c as PasswordBox;
                var password = box.Password;
                user = service.CheckUser(UserName, password);
                if(user != null)
                {
                    LoggedIn = true;
                    LoginFailedMessage = "";
                    RaisePropertyChangedEvent(nameof(LoggedIn));
                }
                else
                {
                    LoginFailedMessage = "UserId or Password is invalid";
                    RaisePropertyChangedEvent(nameof(LoginFailedMessage));
                }
            });       
        }
        
        public ArtistAdministrationVM CurrentArtistAdministration
        {
            get { return currentArtistAdministration; }
            set
            {
                if(currentArtistAdministration != value)
                {
                    currentArtistAdministration = value;
                    currentArtistAdministration.LoadItems();
                    RaisePropertyChangedEvent(nameof(CurrentArtistAdministration));
                }
            }
        }

        public CatagoryAdministrationVM CurrentCatagoryAdministration
        {
            get { return this.currentCatagoryAdministration; }
            set
            {
                if(this.currentCatagoryAdministration != value)
                {
                    currentCatagoryAdministration = value;
                    currentCatagoryAdministration.LoadItems();
                    RaisePropertyChangedEvent();
                }
            }
        }

        public VenueAdministrationVM CurrentVenueAdministration
        {
            get { return this.currentVenueAdministration; }
            set
            {
                if(this.currentVenueAdministration != value)
                {
                    this.currentVenueAdministration = value;
                    currentVenueAdministration.LoadItems();
                    RaisePropertyChangedEvent();
                }
            }
        }

        public PerformanceAdministrationVM CurrentPerformanceAdministration
        {
            get { return this.currentPerformanceAdministration; }
            set
            {
                if(this.currentPerformanceAdministration != value)
                {
                    this.currentPerformanceAdministration = value;
                    currentPerformanceAdministration.LoadItems();
                    RaisePropertyChangedEvent();
                }
            }
        }




    }
}
