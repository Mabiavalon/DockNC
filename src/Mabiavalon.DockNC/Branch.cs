namespace Mabiavalon.DockNC
{
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Controls.Presenters;
    using Avalonia.Controls.Primitives;
    using System;
    using System.Diagnostics;
    using System.Reactive.Linq;

    public class Branch : TemplatedControl
    {
        private GridLength firstOverriden;
        private GridLength secondOverriden;

        public static readonly StyledProperty<Orientation> OrientationProperty =
            AvaloniaProperty.Register<Branch, Orientation>("Orientation");

        public static readonly StyledProperty<object> FirstItemProperty =
            AvaloniaProperty.Register<Branch, object>("FirstItem");

        public static readonly StyledProperty<object> SecondItemProperty =
            AvaloniaProperty.Register<Branch, object>("SecondItem");

        public static readonly StyledProperty<GridLength> FirstItemLengthProperty =
            AvaloniaProperty.Register<Branch, GridLength>("FirstItemLength", new GridLength(0.49999, GridUnitType.Star));

        public static readonly StyledProperty<GridLength> SecondItemLengthProperty =
            AvaloniaProperty.Register<Branch, GridLength>("SecondItemLength", new GridLength(0.50001, GridUnitType.Star));

        static Branch()
        {
            PseudoClass(OrientationProperty, o => o == Orientation.Vertical, ":vertical");
            PseudoClass(OrientationProperty, o => o == Orientation.Horizontal, ":horizontal");
            AffectsMeasure(FirstItemProperty, SecondItemProperty, DataContextProperty);            
        }

        public Branch()
        {
            FirstItemProperty.Changed.Subscribe(o =>
            {
                if(o.OldValue != null)
                {

                }

                if (o.NewValue != null)
                {
                    (o.NewValue as Visual).GetObservable(Visual.IsVisibleProperty).Subscribe(visible =>
                    {
                        Debug.WriteLine("IsVisibleDetected");
                        InvalidateMeasure();
                    });
                }
            });

            SecondItemProperty.Changed.Subscribe(o =>
            {
                if (o.OldValue != null)
                {

                }

                if (o.NewValue != null)
                {
                    (o.NewValue as Visual).GetObservable(Visual.IsVisibleProperty).Subscribe(visible => InvalidateMeasure());
                }
            });
        }

        public Orientation Orientation
        {
            get { return GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public object FirstItem
        {
            get { return GetValue(FirstItemProperty); }
            set { SetValue(FirstItemProperty, value); }
        }

        public object SecondItem
        {
            get { return GetValue(SecondItemProperty); }
            set { SetValue(SecondItemProperty, value); }
        }

        public GridLength FirstItemLength
        {
            get { return GetValue(FirstItemLengthProperty); }
            set { SetValue(FirstItemLengthProperty, value); }
        }

        public GridLength SecondItemLength
        {
            get { return GetValue(SecondItemLengthProperty); }
            set { SetValue(SecondItemLengthProperty, value); }
        }

        public bool BranchFilled => FirstItem != null && SecondItem != null;

        internal ContentPresenter FirstContentPresenter { get; private set; }
        internal ContentPresenter SecondContentPresenter { get; private set; }

        public double GetFirstProportion()
        {
            return 1/(FirstItemLength.Value + SecondItemLength.Value)*FirstItemLength.Value;
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);

            FirstContentPresenter = e.NameScope.Find<ContentPresenter>("PART_FirstContentPresenter");
            SecondContentPresenter = e.NameScope.Find<ContentPresenter>("PART_SecondContentPresenter");
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var firstContentRequiresOverride = false;
            var secondContentRequiresOverride = false;

            if (FirstItem != null)
            {
                var firstChildControl = FirstItem as Control;

                // Might be a POCO with a DataTemplate
                if (firstChildControl == null)
                {
                    firstChildControl = FirstContentPresenter.Child as Control;

                    if (firstChildControl == null)
                        throw new Exception($"Unable to find DataTemplate for 'FirstItem''s value {FirstItem}");
                }

                firstContentRequiresOverride |= !firstChildControl.IsVisible;
            }
            else
                firstContentRequiresOverride = true;

            if (SecondItem != null)
            {
                var secondChildControl = SecondItem as Control;

                // Might be a POCO with a DataTemplate
                if (secondChildControl == null)
                {
                    secondChildControl = SecondContentPresenter.Child as Control;

                    if (secondChildControl == null)
                        throw new Exception($"Unable to find DataTemplate for 'SecondItem''s value {SecondItem}");
                }

                secondContentRequiresOverride |= !secondChildControl.IsVisible;
            }
            else
                secondContentRequiresOverride = true;

            if (firstContentRequiresOverride && secondContentRequiresOverride)
            {
                return Orientation == Orientation.Horizontal ? new Size(Width, 0) : new Size(0, Height);
            }

            if (firstContentRequiresOverride)
            {
                var proportion = 0;

                FirstItemLength = new GridLength(proportion, GridUnitType.Star);
                SecondItemLength = new GridLength(1 - proportion, GridUnitType.Star);
            }
            else if (secondContentRequiresOverride)
            {
                var proportion = 1;

                FirstItemLength = new GridLength(proportion, GridUnitType.Star);
                SecondItemLength = new GridLength(1 - proportion, GridUnitType.Star);
            }

            return base.MeasureOverride(availableSize);
        }
    }
}