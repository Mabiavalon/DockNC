using Avalonia;
using Avalonia.Controls;
using Avalonia.Diagnostics;
using Avalonia.Logging.Serilog;
using Avalonia.Markup.Xaml;
using Serilog;

namespace DockTestApplication
{
    internal class App : Application
    {
        public override void Initialize()
        { 
            AvaloniaXamlLoader.Load(this);
            base.Initialize();
        }

        private static void Main(string[] args)
        {
            AppBuilder.Configure<App>().UsePlatformDetect().Start<MainWindow>();
        }

        public static void AttachDevTools(Window window)
        {
#if DEBUG
            DevTools.Attach(window);
#endif
        }
    }
}