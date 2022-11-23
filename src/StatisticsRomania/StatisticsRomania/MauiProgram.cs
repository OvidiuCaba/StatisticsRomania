using DevExpress.Maui;
using Plugin.MauiMTAdmob;

namespace StatisticsRomania;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseDevExpress()
			.UseMauiApp<App>()
            .UseMauiMTAdmob();

		return builder.Build();
	}
}
