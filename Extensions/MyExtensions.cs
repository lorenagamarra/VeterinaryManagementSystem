using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VeterinaryManagementSystem.Extensions
{
    public static class MyExtensions
    {

        public static int GetIndex(this ComboBox combobox, string content)
        {
            for (int i = 0; i < combobox.Items.Count; i++)
            {
                var item = combobox.Items[i] as ComboBoxItem;

                if (item.Content.Equals(content))
                {
                    return i;    
                }
            }
            return -1;
        }
    }
}
