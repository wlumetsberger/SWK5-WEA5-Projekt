using MvvmValidation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using UltimateFestivalOrganizer.BusinessLogik;
using UltimateFestivalOrganizer.Commander.Util;
using UltimateFestivalOrganizer.DAL.Common.Domain;

namespace UltimateFestivalOrganizer.Commander.ViewModels
{
    public class ArtistVM : BaseVM
    {
        private IAdministrationServices administrationService;
        private IIOService ioService;
        private Artist artist;
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ChangePictureCommand { get; set; }
        public ObservableCollection<Catagory> Catagories { get; private set; }
        public ObservableCollection<string> Countries { get; private set; }


        public ArtistVM(Artist artist, IAdministrationServices service, IIOService ioservice)
        {
            this.administrationService = service;
            this.ioService = ioservice;
            this.artist = artist;
            this.Catagories = new ObservableCollection<Catagory>();
            this.Countries = new ObservableCollection<string>();
            LoadData();
            
            SaveCommand = new RelayCommand(async s => {
                if (!(await Validator.ValidateAllAsync()).IsValid)
                {                  
                    return;
                }
                artist = administrationService.SaveArtist(artist);
                if (artist != null && artist.Id > 0)
                {
                    AppMessages.ShowSuccessMessage.Send($"Artist {artist.Name} saved ");
                    AppMessages.ArtistChanged.Send(AppMessages.ChangeType.Change);
                    return;
                }
                AppMessages.ShowErrorMessage.Send($"Error occured while saving Artist {artist.Name} ");
            });

            DeleteCommand = new RelayCommand(c =>
            {
                if(artist.Id == null)
                {
                    AppMessages.ArtistChanged.Send(AppMessages.ChangeType.Remove);
                    return;
                }
                if (administrationService.DeleteArtist(artist))
                {
                    AppMessages.ShowSuccessMessage.Send($"Artist {artist.Name} removed ");
                    AppMessages.ArtistChanged.Send(AppMessages.ChangeType.Remove);
                    return;
                }
                AppMessages.ShowErrorMessage.Send($"Error occured while removing Artist {artist.Name} ");

            });
            ChangePictureCommand = new RelayCommand(c =>
            {
                this.artist.Picture = ioService.openFileBase64Encoded();
                RaisePropertyChangedEvent(nameof(Picture));
            });

            AddValidationRules();
        }

        private void AddValidationRules()
        {
            Validator.AddRule(() => Name, () => RuleResult.Assert(!string.IsNullOrWhiteSpace(Name), "Name is required"));
            Validator.AddRule(() => Catagory, () => RuleResult.Assert((Catagory != null), "Catagory is required"));
            Validator.AddRule(() => Email, () => RuleResult.Assert(!string.IsNullOrWhiteSpace(Email), "Email is required"));
            Validator.AddRule(() => Email,
                              () =>
                              {
                                  const string regexPattern =
                                      @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$";
                                  return RuleResult.Assert(Regex.IsMatch(Email, regexPattern),
                                                           "Email must by a valid email address");
                              });
            Validator.AddAsyncRule(() => Email,
                                   async () =>{
                                       bool isAvailable = await administrationService.CheckEmailIsAvailable(Email, this.artist);

                                       
                                       
                                       return RuleResult.Assert(isAvailable,
                                           string.Format("Email {0} is taken. Please choose a different one.", Email));

                                   });

        }

        private void LoadData()
        {
            this.Catagories.Clear();
            this.Countries.Clear();
            IList<Catagory> catagories = administrationService.GetCatagories();
            foreach(Catagory cat in catagories)
            {
                this.Catagories.Add(cat);
            }
            RaisePropertyChangedEvent(nameof(Catagories));
            CultureInfo.GetCultures(CultureTypes.AllCultures).ToList().ForEach((c) =>
            {
                if (!Countries.Contains(c.ThreeLetterWindowsLanguageName.ToUpper())){
                    Countries.Add(c.ThreeLetterWindowsLanguageName.ToUpper());
                }
            });
            // Don't know why but AUT is not in AllCultures :( add Manually
            if (!Countries.Contains("AUT"))
            {
                Countries.Add("AUT");
            }
            RaisePropertyChangedEvent(nameof(Countries));
        }

        public string Name {
            get { return this.artist.Name; }
            set
            {
                if(this.artist.Name != value)
                {
                    this.artist.Name = value;
                    Validator.Validate(() => Name);
                    RaisePropertyChangedEvent();
                }
            }

        }

        public string Email
        {
            get { return this.artist.Email; }
            set
            {
                if(this.artist.Email != value)
                {
                    this.artist.Email = value;
                    Validator.ValidateAsync(() => Email);
                    RaisePropertyChangedEvent();
                }
            }
        }

        public string Link
        {
            get { return this.artist.Link; }
            set
            {
                if(this.artist.Link != value)
                {
                    this.artist.Link = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

        public string Picture
        {
            get { return this.artist.Picture; }
            set
            {
                if(this.artist.Picture != value)
                {
                    this.artist.Picture = value;
                    RaisePropertyChangedEvent();
                }
            }
        }
        
        public Catagory Catagory
        {
            get
            {
                if(artist.Catagory != null)
                {
                    foreach(Catagory cat in Catagories)
                    {
                        if(cat.Id == artist.Catagory.Id)
                        {
                            return cat;
                        }
                    }
                }
                return null;
            }
            set
            {
                if(this.artist.Catagory != value)
                {
                    this.artist.Catagory = value;
                    Validator.Validate(() => Catagory);
                    RaisePropertyChangedEvent();
                }
            }
        }
        public string Country
        {
            get
            {
                if(artist.Country != null)
                {
                    foreach(string c in Countries)
                    {
                        if(artist.Country == c)
                        {
                            return c;
                        }
                    }
                }
                return null;
            }
            set
            {
                if(this.artist?.Country != value)
                {
                    this.artist.Country = value;
                    RaisePropertyChangedEvent();
                }
            }
        }
        
    }
}
