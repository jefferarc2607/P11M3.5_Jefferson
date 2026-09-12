namespace DataBinding6133261
{
     public partial class MainPage : ContentPage
     {
         public MainPage()
         {
            InitializeComponent();
            BindingContext = new Alumno();
         }
     }
}
