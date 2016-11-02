namespace Mabiavalon.DockNC
{
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Controls.Presenters;
    using Avalonia.Controls.Primitives;
    using System;
    using System.Diagnostics;
    using Avalonia.LogicalTree;

    public class Branch : TemplatedControl
    {
        private IDisposable _firstItemVisibilitDisposable;
        private IDisposable _secondItemVisibilityDisposable;
        private bool _firstItemLastVisibility = true;
        private bool _secondItemLastVisibility = true;
        private GridLength _firstItemLastGridLength;
        private GridLength _secondItemLastGridLength;

        public static readonly StyledProperty<Orientation> OrientationProperty =
            AvaloniaProperty.Register<Branch, Orientation>(nameof(Orientation));

        public static readonly StyledProperty<object> FirstItemProperty =
            AvaloniaProperty.Register<Branch, object>(nameof(FirstItem));

        public static readonly StyledProperty<object> SecondItemProperty =
            AvaloniaProperty.Register<Branch, object>(nameof(SecondItem));

        public static readonly StyledProperty<GridLength> FirstItemLengthProperty =
            AvaloniaProperty.Register<Branch, GridLength>(nameof(FirstItemLength), new GridLength(0.49999, GridUnitType.Star));

        public static readonly StyledProperty<GridLength> SecondItemLengthProperty =
            AvaloniaProperty.Register<Branch, GridLength>(nameof(SecondItemLength), new GridLength(0.50001, GridUnitType.Star));

        static Branch()
        {
            PseudoClass(OrientationProperty, o => o == Orientation.Vertical, ":vertical");
            PseudoClass(OrientationProperty, o => o == Orientation.Horizontal, ":horizontal");
            AffectsMeasure(FirstItemProperty, SecondItemProperty, DataContextProperty);
        }

        private void RegisterVisualChanges(ContentPresenter presenter, ref IDisposable disposable)
        {
            disposable?.Dispose();

            presenter?.UpdateChild();

            var newVisual = presenter?.Child as Visual;

            if (newVisual != null)
            {
                disposable = newVisual.GetObservable(IsVisibleProperty).Subscribe(visible =>
                {
                    Debug.WriteLine("IsVisible Detected");
                    InvalidateMeasure();
                });
            }
            else
            {
                Debug.WriteLine("No visibility observable found");
            }
        }

        public Branch()
        {
            FirstItemProperty.Changed.Subscribe(o => RegisterVisualChanges(FirstContentPresenter, ref _firstItemVisibilitDisposable));

            SecondItemProperty.Changed.Subscribe(o => RegisterVisualChanges(SecondContentPresenter, ref _secondItemVisibilityDisposable));
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
            return 1 / (FirstItemLength.Value + SecondItemLength.Value) * FirstItemLength.Value;
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);

            FirstContentPresenter = e.NameScope.Find<ContentPresenter>("PART_FirstContentPresenter");
            SecondContentPresenter = e.NameScope.Find<ContentPresenter>("PART_SecondContentPresenter");

            RegisterVisualChanges(FirstContentPresenter, ref _firstItemVisibilitDisposable);
            RegisterVisualChanges(SecondContentPresenter, ref _secondItemVisibilityDisposable);
        }

        private void InvalidateVisibilityChanges(bool firstItemVisible, bool secondItemVisible)
        {
            if (firstItemVisible != _firstItemLastVisibility)
            {
                if (firstItemVisible)
                {
                    FirstItemLength = _firstItemLastGridLength;
                }
                else
                {
                    _firstItemLastGridLength = FirstItemLength;

                    FirstItemLength = new GridLength();
                }

                _firstItemLastVisibility = firstItemVisible;
            }

            if (secondItemVisible != _secondItemLastVisibility)
            {
                if (secondItemVisible)
                {
                    SecondItemLength = _secondItemLastGridLength;
                }
                else
                {
                    _secondItemLastGridLength = SecondItemLength;

                    SecondItemLength = new GridLength();
                }

                _secondItemLastVisibility = secondItemVisible;
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var firstItemVisible = false;
            var secondItemVisible = false;

            if (FirstItem != null)
            {
                var firstChildControl = FirstContentPresenter?.Child as Visual;

                if (firstChildControl != null)
                {
                    firstItemVisible = firstChildControl.IsVisible;
                }
            }

            if (SecondItem != null)
            {
                var secondChildControl = SecondContentPresenter?.Child as Visual;

                if (secondChildControl != null)
                {
                    secondItemVisible = secondChildControl.IsVisible;
                }
            }

            InvalidateVisibilityChanges(firstItemVisible, secondItemVisible);

            if (firstItemVisible && secondItemVisible)
            {
                var proportion = GetFirstProportion();

                FirstItemLength = new GridLength(proportion, GridUnitType.Star);
                SecondItemLength = new GridLength(1 - proportion, GridUnitType.Star);
            }
            else if (!firstItemVisible && !secondItemVisible)
            {
                return Orientation == Orientation.Horizontal ? new Size(Width, 0) : new Size(0, Height);
            }

            return base.MeasureOverride(availableSize);
        }
    }
}