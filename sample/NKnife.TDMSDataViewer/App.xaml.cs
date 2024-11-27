using Autofac;
using Autofac.Util;
using Microsoft.VisualBasic;
using NKnife.TDMSDataViewer.Views;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using NLog;
using CommunityToolkit.Mvvm.DependencyInjection;
using NKnife.TDMSDataViewer.ViewModels;

namespace NKnife.TDMSDataViewer
{
    public partial class App : Application
    {
        private static readonly NLog.Logger s_logger = LogManager.GetCurrentClassLogger();
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            Application.Current.DispatcherUnhandledException += DispatcherOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            s_logger.Info("Starting");
            s_logger.Info("Dispatcher managed thread identifier = {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
            s_logger.Info("WPF rendering capability (tier) = {0}", RenderCapability.Tier / 0x10000);
            RenderCapability.TierChanged += (s, a) =>
            {
                s_logger.Info("WPF rendering capability (tier) = {0}", RenderCapability.Tier / 0x10000);
            };

            base.OnStartup(e);

            BootStrapper.Start();

            var workbench = new Workbench();

            //workbench.DataContext = BootStrapper.RootVisual;

            workbench.Closed += (s, a) =>
            {
                BootStrapper.Stop();
            };

            Current.Exit += (s, a) =>
            {
                s_logger.Info("Bye Bye!");
                LogManager.Flush();
            };

            workbench.Show();

            s_logger.Info("Started");
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            s_logger.Info("Unhandled app domain exception");
            if(args.ExceptionObject is Exception e)
                HandleException(e);
        }

        private void DispatcherOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            s_logger.Info("Unhandled dispatcher thread exception");
            args.Handled = true;

            HandleException(args.Exception);
        }

        private void TaskSchedulerOnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs args)
        {
            s_logger.Info("Unhandled task exception");
            args.SetObserved();

            HandleException(args.Exception.GetBaseException());
        }

        private void HandleException(Exception exception)
        {
            s_logger.Error(exception);
        }
    }

    class ViewModelLocator
    {
        public ViewModelLocator()
        {
            Workbench = Ioc.Default.GetRequiredService<WorkbenchViewModel>();
        }
        public WorkbenchViewModel Workbench { get; set; }
    }
}
