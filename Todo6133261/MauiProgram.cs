using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Todo6133261.Views;

namespace Todo6133261
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.TryAddSingleton<TodoListPage>();
            builder.Services.TryAddSingleton<TodoItemPage>();
            builder.Services.TryAddSingleton<Data.TodoItemDatabase>();
            return builder.Build();
        }
    }
}
