using System.Diagnostics;

namespace HelloMAUI
{
    public partial class App : Application
    {
        private readonly AppShell _shell;

        public App(AppShell shell)
        {
            InitializeComponent();
            _shell=shell;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            base.OnStart();
            Trace.WriteLine("******** App started ********");
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            base.OnSleep();
            Trace.WriteLine("******** App slept ********");
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            base.OnResume();

            Trace.WriteLine("******** App resumed ********");
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(_shell);
        }
    }
}