using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MvvmDialogs;
using NKnife.TDMSDataViewer.ViewModels.Common;
using NKnife.TDMSDataViewer.Views.Controls;

namespace NKnife.TDMSDataViewer.ViewModels
{
    internal class SampleDataCreateWizardViewModel : BaseViewModel, IModalDialogViewModel
    {
        public LevelInfoControlViewModel FileInfoVm { get; set; } = new(true);

        /// <inheritdoc />
        public bool? DialogResult { get; }
    }
}
