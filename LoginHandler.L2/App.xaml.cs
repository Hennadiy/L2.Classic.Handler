using LoginHandler.L2.Engine;
using Ninject;
using System.Windows;

namespace LoginHandler.L2
{
    public partial class App : Application
    {
        private IKernel _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            _container = new StandardKernel();
            _container.Bind<IEngine>().To<LoginEngine>();
        }

        private void ComposeObjects()
        {
            Current.MainWindow = _container.Get<MainWindow>();
        }
    }
}
