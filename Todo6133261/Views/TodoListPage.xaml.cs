using System.Collections.ObjectModel;
using Todo6133261.Data;

namespace Todo6133261.Views;

public partial class TodoListPage : ContentPage
{
	TodoItemDatabase database;
	public ObservableCollection<Models.TodoItem> Items { get; set; } = new();
    public TodoListPage(TodoItemDatabase todoItemDatabase)
	{
		InitializeComponent();
        database = todoItemDatabase;
		BindingContext = this;
    }

	protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var items = await database.GetItemsAsync();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Items.Clear();
            foreach (var item in items)
            {
                Items.Add(item);
            }
        });
    }

    async void OnItemAdded(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TodoItemPage), true, new Dictionary<string, object>
        {
            ["Item"] = new Models.TodoItem()
        });
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Models.TodoItem item)
            return;

        await Shell.Current.GoToAsync(nameof(TodoItemPage), true, new Dictionary<string, object>
        {
            ["Item"] = item
        });
    }
}