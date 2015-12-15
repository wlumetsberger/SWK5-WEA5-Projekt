using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateFestivalOrganizer.Commander.Util
{
    public class BaseIOService : IIOService
    {
        public string openFileBase64Encoded()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if(dialog.ShowDialog() == true)
            {
                return Convert.ToBase64String(File.ReadAllBytes(dialog.FileName));
            }
            return "";
        }
    }
}
