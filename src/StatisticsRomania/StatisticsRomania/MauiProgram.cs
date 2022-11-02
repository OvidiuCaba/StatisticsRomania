
using DevExpress.Maui;

namespace StatisticsRomania;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseDevExpress()
			.UseMauiApp<App>();

		return builder.Build();
	}
}
