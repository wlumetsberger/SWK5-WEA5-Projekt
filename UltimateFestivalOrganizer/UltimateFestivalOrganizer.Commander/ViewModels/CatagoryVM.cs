using MvvmValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UltimateFestivalOrganizer.BusinessLogik;
using UltimateFestivalOrganizer.Commander.Util;
using UltimateFestivalOrganizer.DAL.Common.Domain;

namespace UltimateFestivalOrganizer.Commander.ViewModels
{
    public class CatagoryVM : BaseVM

    {
        private IAdministrationServices service;
        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        private Catagory catagory;
             
        public CatagoryVM(Catagory cat, IAdministrationServices service)
        {
            catagory = cat;
            this.service = service;
            SaveCommand = new RelayCommand((c) =>
            {
                if (!Validator.ValidateAll().IsValid)
                {
                    return;
                }
                catagory = service.SaveCatagory(catagory); 
                if (catagory != null && catagory.Id > 0)
                {
                    AppMessages.CatagoryChanged.Send(AppMessages.ChangeType.Change);
                    AppMessages.ShowSuccessMessage.Send($"Catagory {catagory.Name} saved");
                    return;
                }
                AppMessages.ShowErrorMessage.Send($"Error while saving Catagory {catagory.Name}");
            });
            DeleteCommand = new RelayCommand((c) => 
            {
               
                    try {
                        if (service.DelteCatagory(catagory))
                        {
                            AppMessages.CatagoryChanged.Send(AppMessages.ChangeType.Remove);
                            AppMessages.ShowSuccessMessage.Send($"Catagory {catagory.Name} removed");
                            return;
                        }
                        
                    }catch(ElementInUseException e)
                    {
                        AppMessages.ShowErrorMessage.Send(e.Message);
                        return;
                    }
                    AppMessages.ShowErrorMessage.Send($"Error while removing Catagory {catagory.Name}");
                
            });
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            Validator.AddRule(() => Name, () => RuleResult.Assert(!string.IsNullOrWhiteSpace(Name), "Name is required"));
            Validator.AddRule(() => Description, () => RuleResult.Assert(!string.IsNullOrWhiteSpace(Description), "Description is required"));
        }
        

        public string Name
        {
            get { return catagory.Name; }
            set {
                if(catagory.Name != value)
                {
                    catagory.Name = value;
                    Validator.Validate(() => Name);
                    RaisePropertyChangedEvent();
                }
            }
        }
        public string Description
        {
            get { return catagory.Description; }
            set
            {
                if(catagory.Description != value)
                {
                    catagory.Description = value;
                    Validator.Validate(() => Description);
                    RaisePropertyChangedEvent();
                }
            }
        }

    }
}
