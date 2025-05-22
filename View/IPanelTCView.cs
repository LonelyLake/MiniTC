using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniTC.View
{
    interface IPanelTCView
    {        
        string CurrentPath { get; set; }
        string[] Drives { get; set; }
        string[] Items { get; set; }
        string SelectedItem { get; }

        event Action<string> DriveSelected;
        event Action<string> PathItemSelected;
        event Action RefreshRequested;
    }
}
