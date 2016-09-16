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
        public int ItemIteration { get; set; }

        public MainWindow()
        {
            this.InitializeComponent();
            App.AttachDevTools(this);

            ItemIteration = 1;

            var docker = this.FindControl<DockControl>("Docker");


            var topButton = this.FindControl<Button>("DockTop");
            topButton.Click += (sender, args) =>
            {
                DockInternal(docker, DockTarget.Top);
            };
                          
            var bottomButton = this.FindControl<Button>("DockBottom");
            bottomButton.Click += (sender, args) =>
            {
                DockInternal(docker, DockTarget.Bottom);
            };

            var leftButtun = this.FindControl<Button>("DockLeft");
            leftButtun.Click += (sender, args) =>
            {
                DockInternal(docker, DockTarget.Left);
            };

            var rightButton = this.FindControl<Button>("DockRight");
            rightButton.Click += (sender, args) =>
            {
                DockInternal(docker, DockTarget.Right);
            };
        }

        private void DockInternal(DockControl docker, DockTarget dockTarget)
        {
            docker.Dock(new Button {Content = $"Item {ItemIteration}"}, dockTarget);
            IterationItem();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void IterationItem()
        {
            ItemIteration++;
        }
    }
}
