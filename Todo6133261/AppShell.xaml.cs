namespace Todo6133261
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Views.TodoItemPage), typeof(Views.TodoItemPage));
        }
    }
}
