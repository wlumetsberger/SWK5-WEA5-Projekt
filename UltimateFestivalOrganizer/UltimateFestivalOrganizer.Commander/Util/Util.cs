using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UltimateFestivalOrganizer.Commander.Util
{
    public class Util
    {
        private static Dictionary<int?, Brush> values = new Dictionary<int?, Brush>();
        public static Brush  GetColorForId(int? id)
        {
            if (id == null)
            {
                return new SolidColorBrush();
            }
            else
            {

               if (!values.ContainsKey(id))
                {
                    values.Add(id, GetNextBrush());                    
                }
                

            }
            return values[id];

        }
        private static SolidColorBrush GetNextBrush()
        {
            SolidColorBrush brush = new SolidColorBrush(GetNext());
            brush.Freeze();

            return brush;

        }

        private static Color GetNext()
        {
            Random r = new Random();
            return Color.FromRgb(
                   (byte)r.Next(255),
                   (byte)r.Next(255),
                   (byte)r.Next(255)
                   );
        }

    }
}
