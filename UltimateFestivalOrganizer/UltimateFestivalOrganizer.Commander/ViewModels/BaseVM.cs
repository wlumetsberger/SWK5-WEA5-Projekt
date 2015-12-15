using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using MvvmValidation;

namespace UltimateFestivalOrganizer.Commander.ViewModels
{
    public class BaseVM : INotifyPropertyChanged, IDataErrorInfo
    {

        public ValidationHelper Validator { get; private set; }
        private DataErrorInfoAdapter DataErrorInfoAdapter { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public BaseVM()
        {
            Validator = new ValidationHelper();
            DataErrorInfoAdapter = new DataErrorInfoAdapter(Validator);
            Validator.ResultChanged += (o, e) =>
            {
                var propertyName = e.Target as string;

                if (!string.IsNullOrEmpty(propertyName))
                {
                    RaisePropertyChangedEvent(propertyName);
                }
            };
        }
        public string this[string columnName]
        {
            get { return DataErrorInfoAdapter[columnName]; }
        }

        public string Error
        {
            get { return DataErrorInfoAdapter.Error; }
        }


        public void RaisePropertyChangedEvent([CallerMemberName] string propertyName = "")
        { 
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        }

        
    }
}
