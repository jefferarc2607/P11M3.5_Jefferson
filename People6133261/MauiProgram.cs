using Microsoft.Extensions.Logging;
using People6133261.Data;
using People6133261.Helpers;

namespace People6133261
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

            string dbPath = FileAccessHelper.GetLocalFilePath("people.db3");
            builder.Services.AddSingleton<PersonRepository>(s => ActivatorUtilities.CreateInstance<PersonRepository>(s, dbPath));

            builder.Services.AddSingleton<MainPage>();

            return builder.Build();
        }
    }
}
