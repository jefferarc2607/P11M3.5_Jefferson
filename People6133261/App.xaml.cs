using Microsoft.Extensions.DependencyInjection;
using People6133261.Data;

namespace People6133261
{
    public partial class App : Application
    {
        public static PersonRepository PersonRepo { get; private set; }

        public App(PersonRepository repo)
        {
            InitializeComponent();

            PersonRepo = repo;
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            return new Window(new AppShell());
        }
    }
}