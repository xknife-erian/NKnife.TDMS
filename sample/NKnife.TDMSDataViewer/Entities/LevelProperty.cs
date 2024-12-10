using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKnife.TDMSDataViewer.Entities
{
    public class LevelProperty : ObservableObject
    {
        public string? Name
        {
            get;
            set => SetProperty(ref field, value);
        }

        public string? Value
        {
            get;
            set => SetProperty(ref field, value);
        }
    }
}
