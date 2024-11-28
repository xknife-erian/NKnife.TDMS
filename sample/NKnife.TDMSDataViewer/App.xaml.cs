using CommunityToolkit.Mvvm.DependencyInjection;
using NKnife.TDMSDataViewer.Views;
using NLog;
using System.Configuration;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace NKnife.TDMSDataViewer
{
    public partial class App : Application
    {
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();

        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            Current.DispatcherUnhandledException       += DispatcherOnUnhandledException;
            TaskScheduler.UnobservedTaskException      += TaskSchedulerOnUnobservedTaskException;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            s_logger.Info("Starting");
            s_logger.Info("Dispatcher managed thread identifier = {0}", Thread.CurrentThread.ManagedThreadId);
            s_logger.Info("WPF rendering capability (tier) = {0}", RenderCapability.Tier / 0x10000);
            RenderCapability.TierChanged += (s, a) =>
            {
                s_logger.Info("WPF rendering capability (tier) = {0}", RenderCapability.Tier / 0x10000);
            };
            Current.Exit += (s, a) =>
            {
                s_logger.Info("Bye Bye!");
                LogManager.Flush();
            };

            BootStrapper.Start();

            var workbench = Ioc.Default.GetService<Workbench>();

            if(workbench == null)
                throw new ConfigurationErrorsException("Workbench not found");

            workbench.DataContext =  BootStrapper.RootVisual;
            workbench.Closed      += (s, a) => { BootStrapper.Stop(); };
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
}