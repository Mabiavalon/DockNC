﻿using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using Mabiavalon.DockNC;
using Mabiavalon.DockNC.Docking;

namespace DockTestApplication
{
    public class MainWindow : Window
    {
        private readonly DockControl _docker;

        public MainWindow()
        {
            InitializeComponent();
            App.AttachDevTools(this);

            ItemIteration = 1;

            _docker = this.FindControl<DockControl>("Docker");

            var topButton = this.FindControl<Button>("DockTop");
            topButton.Click += (sender, args) => { NestInternal(DockTarget.Top); };

            var bottomButton = this.FindControl<Button>("DockBottom");
            bottomButton.Click += (sender, args) => { NestInternal(DockTarget.Bottom); };

            var leftButtun = this.FindControl<Button>("DockLeft");
            leftButtun.Click += (sender, args) => { NestInternal(DockTarget.Left); };

            var rightButton = this.FindControl<Button>("DockRight");
            rightButton.Click += (sender, args) => { NestInternal(DockTarget.Right); };
        }

        public int ItemIteration { get; set; }

        private void NestInternal(DockTarget dockTarget)
        {
            _docker.Dock(CreateActionButton(), dockTarget);
            IterationItem();
        }

        private void SplitInternal(Branch branch, BranchItem branchItem)
        {
            _docker.Dock(CreateActionButton(), RandomEnum<DockTarget>(), branch, branchItem);
            IterationItem();
        }

        private Button CreateActionButton()
        {
            var button = new Button {Content = $"Item {ItemIteration} (Splits in random position)"};
            button.Click += Button_Click;

            return button;
        }

        private void Button_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var button = (Button) sender;

            var parentBranch = button?.GetVisualAncestors().OfType<Branch>().FirstOrDefault();

            if (parentBranch == null) return;

            // Assumed for now, I see this not working as well as I think.
            var branchItem = parentBranch.FirstItem == button ? BranchItem.First : BranchItem.Second;

            SplitInternal(parentBranch, branchItem);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void IterationItem()
        {
            ItemIteration++;
        }

        public T RandomEnum<T>()
        {
            T[] values = (T[])Enum.GetValues(typeof(T));
            return values[new Random().Next(0, values.Length)];
        }
    }
}