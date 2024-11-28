using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using CommunityToolkit.Mvvm.DependencyInjection;
using MvvmDialogs;
using NKnife.TDMSDataViewer.ViewModels;
using NKnife.TDMSDataViewer.ViewModels.Common;
using NKnife.TDMSDataViewer.ViewModels.Dialogs;
using NKnife.TDMSDataViewer.Views;

namespace NKnife.TDMSDataViewer
{

    public static class BootStrapper
    {
        private static ILifetimeScope? s_rootScope;
        private static WorkbenchViewModel? s_workbenchViewModel;

        public static IViewModel RootVisual
        {
            get
            {
                if (s_rootScope == null)
                {
                    Start();
                }

                s_workbenchViewModel = s_rootScope!.Resolve<WorkbenchViewModel>();
                return s_workbenchViewModel;
            }
        }

        public static void Start()
        {
            if (s_rootScope != null)
            {
                return;
            }

            var assemblies = new[] { Assembly.GetExecutingAssembly() };
            var builder = new ContainerBuilder();

            var vmInterface = typeof(IViewModel);
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => vmInterface.IsAssignableFrom(t))
                .AsSelf()
                .AsImplementedInterfaces();
            builder.RegisterType<Workbench>().AsSelf().AsImplementedInterfaces();

            s_rootScope = builder.Build();
            Ioc.Default.ConfigureServices(new AutofacServiceProvider(s_rootScope));
        }

        public static void Stop()
        {
            s_rootScope?.Dispose();
        }

        public static T Resolve<T>() where T : notnull
        {
            if (s_rootScope == null)
            {
                throw new Exception("Bootstrapper hasn't been started!");
            }

            return s_rootScope.Resolve<T>([]);
        }

        public static T Resolve<T>(Parameter[] parameters) where T : notnull
        {
            if (s_rootScope == null)
            {
                throw new Exception("Bootstrapper hasn't been started!");
            }

            return s_rootScope.Resolve<T>(parameters);
        }
    }
}
