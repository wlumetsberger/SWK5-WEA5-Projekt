using MvvmValidation;
using System;
using System.Collections.Generic;
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
    public class VenueVM : BaseVM
    {
        private IAdministrationServices administrationService;
        private Venue venue;
        public ICommand Save { get; private set; }
        public ICommand Remove { get; private set; }

        public VenueVM(Venue venue, IAdministrationServices service)
        {
            this.venue = venue;
            this.administrationService = service;

            Save = new RelayCommand(c => 
            {
                if (!Validator.ValidateAll().IsValid)
                {
                    return;
                }
                this.venue = administrationService.SaveVenue(this.venue);
                if (this.venue != null && this.venue.Id > 0)
                {
                    AppMessages.ShowSuccessMessage.Send($"Venue {this.venue.Description} saved ");
                    AppMessages.VenueChanged.Send(AppMessages.ChangeType.Change);
                }
                else
                {
                    AppMessages.ShowErrorMessage.Send($"Error occured while saving Venue {this.venue.Description} ");
                }
            });

            Remove = new RelayCommand(c =>
            {
                try {
                    if (administrationService.DeleteVenue(this.venue))
                    {
                        AppMessages.ShowSuccessMessage.Send($"Venue {this.venue.Description} removed ");
                        AppMessages.VenueChanged.Send(AppMessages.ChangeType.Remove);
                        return;
                    }
                    else
                    {
                        AppMessages.ShowErrorMessage.Send($"Error occured while removing Venue {this.venue.Description} ");
                    }
                }catch(ElementInUseException e)
                {
                    AppMessages.ShowErrorMessage.Send(e.Message);
                }
            });
            ApplyValidationRules();

        }

        private void ApplyValidationRules()
        {
            Validator.AddRule(() => Description, ()=> RuleResult.Assert(!string.IsNullOrWhiteSpace(Description), "Description is required"));
            Validator.AddRule(() => ShortDescription, () => RuleResult.Assert(!string.IsNullOrWhiteSpace(ShortDescription), "ShortDescription is required"));
            Validator.AddRule(() => Address, () => RuleResult.Assert(!string.IsNullOrWhiteSpace(Address), "Address is required"));
        }

      

        public string Description
        {
            get { return venue.Description; }
            set
            {
                if(venue.Description != value)
                {
                    venue.Description = value;
                    Validator.Validate(() => Description);
                    RaisePropertyChangedEvent();
                }
            }
        }

        public string ShortDescription
        {
            get { return venue.ShortDescription; }
            set
            {
                if(venue.ShortDescription != value)
                {
                    venue.ShortDescription = value;
                    Validator.Validate(() => ShortDescription);
                    RaisePropertyChangedEvent();
                }
            }

        }

        public string Address
        {
            get { return venue.Address; }
            set
            {
                if(venue.Address != value)
                {
                    venue.Address = value;
                    Validator.Validate(() => Address);
                    RaisePropertyChangedEvent();
                }
            }
        }

        public int Longitude
        {
            get { return venue.Longitude; }
            set
            {
                if(venue.Longitude != value)
                {
                    venue.Longitude = value;
                    RaisePropertyChangedEvent();
                         
                }
            }
        }
        
        public int Latitude
        {
            get { return venue.Latitude; }
            set
            {
                if(venue.Latitude != value)
                {
                    venue.Latitude = value;
                    RaisePropertyChangedEvent();
                }
            }
        }



    }
}
