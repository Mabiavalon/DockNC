using Avalonia.Controls;
using Avalonia.Layout;
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

                buttun.Click += (o, eventArgs) =>
                {
                    var buttun2 = new Button();
                    buttun2.Content = "TestButton In Left";

                    var docker2 = this.FindControl<ContentControl>("Docker");
                    (docker2.Content as Branch).SecondItem = buttun2;
                };

                var docker = this.FindControl<ContentControl>("Docker");
                docker.Content = new Branch {FirstItem = buttun};
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
