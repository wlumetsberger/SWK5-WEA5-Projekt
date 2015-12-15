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
    public class ArtistAdministrationVM: BaseVM, ITabView
    {
        private IAdministrationServices administrationService;
        private IIOService ioService;
        private ArtistVM currentArtist;
        public ICommand AddNewArtistCommand { get; set; }
        public ObservableCollection<ArtistVM> Artists { get; set; }


        public ArtistAdministrationVM(IAdministrationServices services, IIOService ioService)
        {
            this.administrationService = services;
            this.ioService = ioService;
            Artists = new ObservableCollection<ArtistVM>();

            AddNewArtistCommand = new RelayCommand(s => {
                CurrentArtist = new ArtistVM(new Artist(),administrationService, ioService);
                Artists.Add(CurrentArtist);
                RaisePropertyChangedEvent(nameof(CurrentArtist));
                RaisePropertyChangedEvent(nameof(Artists));
            });
           

            AppMessages.ArtistChanged.Register(this, (type) =>
            {
                if(type == AppMessages.ChangeType.Remove)
                {
                    LoadArtists();
                }
                RaisePropertyChangedEvent(nameof(CurrentArtist));
                RaisePropertyChangedEvent(nameof(Artists));
            });

            AppMessages.CatagoryChanged.Register(this, (type) =>
            {
                this.LoadItems();
                RaisePropertyChangedEvent(nameof(Artists));
                RaisePropertyChangedEvent(nameof(Catagory));
            });

            LoadArtists();
        }

        public ArtistVM CurrentArtist
        {
            get { return currentArtist; }
            set
            {
                if(currentArtist != value)
                {
                    currentArtist = value;
                    RaisePropertyChangedEvent(nameof(CurrentArtist));
                }
            }
        }

        private  void LoadArtists()
        {
            
                Artists.Clear();
                IList<Artist> artists = administrationService.GetArtists();
                foreach (Artist artist in artists)
                {
                    Artists.Add(new ArtistVM(artist, this.administrationService, ioService));
                }
                CurrentArtist = Artists.First();
                var view = CollectionViewSource.GetDefaultView(Artists);
                view.GroupDescriptions.Clear();
                view.GroupDescriptions.Add(new PropertyGroupDescription("Catagory.Name"));
           
        }
    

        public void LoadItems()
        {
            this.LoadArtists();
        }
    }
}
