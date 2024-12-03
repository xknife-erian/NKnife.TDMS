using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NKnife.TDMSDataViewer.Views.Controls
{
    public class LevelInfoControlViewModel(bool __isFileInfo) : ObservableObject
    {
        public bool IsFileInfo { get; set; } = __isFileInfo;
        public ObservableCollection<(string Name, object Value)> ValueTuples { get; set; } = [];
    }
}
