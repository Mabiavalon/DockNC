using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Mabiavalon.DockNC;

namespace DockTestApplication
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            App.AttachDevTools(this);
            var button = this.FindControl<Button>("TestButton");
            button.Click += (sender, args) =>
            {
                var buttun = new Button();
                buttun.Content = "TestButton In Left";

                var docker = this.FindControl<DockControl>("Docker");
                docker.Dock(buttun, Dock.Left);
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
