using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Logging.Serilog;
using Avalonia.Markup.Xaml;
using Mabiavalon.DockNC;
using Serilog;

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
                buttun.Content = "TestButton Bottom";

                buttun.Click += (o, eventArgs) =>
                {
                    var buttun2 = new Button();
                    buttun2.Content = "TestButton In Top With Horizonal Branch";
                    buttun2.Click += (sender1, routedEventArgs) =>
                    {
                        var docker3 = this.FindControl<ContentControl>("Docker");
                        var branch = (docker3.Content as Branch);

                        var horBranch = new Branch {Orientation = Orientation.Horizontal};
                        horBranch.FirstItem = new Button {Content = "Left Button"};
                        horBranch.SecondItem = new Button { Content = "Right Button"};

                        branch.FirstItem = horBranch;
                    };

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
