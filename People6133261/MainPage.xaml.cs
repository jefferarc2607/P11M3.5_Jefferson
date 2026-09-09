using Microsoft.Maui.Controls;
using People6133261.Data;
using People6133261.Models;
namespace People6133261
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnAddPersonClicked(object sender, EventArgs e)
        {
            string name = NameEntry.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                StatusLabel.Text = "Introduce a name.";
                return;
            }
            App.PersonRepo.AddNewPerson(name);
            StatusLabel.Text = App.PersonRepo.StatusMessage;
            NameEntry.Text = string.Empty; 
            OnGetAllPeopleClicked(sender, e);
        }

        private void OnGetAllPeopleClicked(object sender, EventArgs e)
        {
            List<Person> people = App.PersonRepo.GetAllPeople();
            PeopleListView.ItemsSource = people;
            StatusLabel.Text = App.PersonRepo.StatusMessage ?? $"Found {people.Count} people.";
        }
    }
}
