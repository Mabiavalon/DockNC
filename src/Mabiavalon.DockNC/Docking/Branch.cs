namespace Mabiavalon.DockNC.Docking
{
    using System;
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Controls.Presenters;
    using Avalonia.Controls.Primitives;
    using System.Reactive.Disposables;

    public class Branch : TemplatedControl
    {
        private IDisposable _firstItemVisibilityDisposable;
        private IDisposable _secondItemVisibilityDisposable;
        private bool _firstItemLastVisibility = true;
        private bool _secondItemLastVisibility = true;
        private GridLength _firstItemLastGridLength;
        private GridLength _secondItemLastGridLength;
        private CompositeDisposable _disposables = new CompositeDisposable();

        public static readonly StyledProperty<Orientation> OrientationProperty =
            AvaloniaProperty.Register<Branch, Orientation>(nameof(Orientation));

        public static readonly StyledProperty<object> FirstItemProperty =
            AvaloniaProperty.Register<Branch, object>(nameof(FirstItem));

        public static readonly StyledProperty<object> SecondItemProperty =
            AvaloniaProperty.Register<Branch, object>(nameof(SecondItem));

        public static readonly StyledProperty<bool> GridSplitterVisibleProperty =
            AvaloniaProperty.Register<Branch, bool>(nameof(GridSplitterVisible));

        public static readonly StyledProperty<GridLength> FirstItemLengthProperty =
            AvaloniaProperty.Register<Branch, GridLength>(nameof(FirstItemLength), new GridLength(0.49999, GridUnitType.Star));

        public static readonly StyledProperty<GridLength> SecondItemLengthProperty =
            AvaloniaProperty.Register<Branch, GridLength>(nameof(SecondItemLength), new GridLength(0.50001, GridUnitType.Star));

        public static readonly StyledProperty<bool> IsVisibleProperty = Visual.IsVisibleProperty.AddOwner<Branch>();

        static Branch()
        {
            PseudoClass(OrientationProperty, o => o == Orientation.Vertical, ":vertical");
            PseudoClass(OrientationProperty, o => o == Orientation.Horizontal, ":horizontal");
            AffectsMeasure(FirstItemProperty, SecondItemProperty, DataContextProperty);
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

        public bool GridSplitterVisible
        {
            get { return GetValue(GridSplitterVisibleProperty); }
            set { SetValue(GridSplitterVisibleProperty, value); }
        }

        public bool IsVisible
        {
            get { return GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        public bool BranchFilled => FirstItem != null && SecondItem != null;

        internal ContentPresenter FirstContentPresenter { get; private set; }
        internal ContentPresenter SecondContentPresenter { get; private set; }

        public double GetFirstProportion()
        {
            return 1 / (FirstItemLength.Value + SecondItemLength.Value) * FirstItemLength.Value;
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
        {
            if (_firstItemVisibilityDisposable != null)
            {
                _disposables.Add(_firstItemVisibilityDisposable);
            }

            if (_secondItemVisibilityDisposable != null)
            {
                _disposables.Add(_secondItemVisibilityDisposable);
            }

            _disposables.Dispose();
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);

            FirstContentPresenter = e.NameScope.Find<ContentPresenter>("PART_FirstContentPresenter");
            SecondContentPresenter = e.NameScope.Find<ContentPresenter>("PART_SecondContentPresenter");

            _disposables.Add(FirstContentPresenter.GetObservableWithHistory(ContentPresenter.ContentProperty).Subscribe(o =>
            {
                RegisterVisualChanges(FirstContentPresenter, ref _firstItemVisibilityDisposable);

                UpdateLogicalChildren(o.Item1, o.Item2);
            }));

            _disposables.Add(SecondContentPresenter.GetObservableWithHistory(ContentPresenter.ContentProperty).Subscribe(o =>
            {
                RegisterVisualChanges(SecondContentPresenter, ref _secondItemVisibilityDisposable);

                UpdateLogicalChildren(o.Item1, o.Item2);
            }));

            UpdateLogicalChildren(null, FirstContentPresenter.Content);
            UpdateLogicalChildren(null, SecondContentPresenter.Content);

            InvalidateVisibilityChanges();
            InvalidateMeasure();

            RegisterVisualChanges(FirstContentPresenter, ref _firstItemVisibilityDisposable);
            RegisterVisualChanges(SecondContentPresenter, ref _secondItemVisibilityDisposable);
        }

        private void UpdateLogicalChildren(object oldItem, object newItem)
        {
            var oldChild = (Control)oldItem;

            var newChild = (Control)newItem;

            if (oldChild != null)
            {
                //((ISetLogicalParent)oldItem).SetParent(null);
                LogicalChildren.Remove(oldChild);
                //Visual Tree Already Managed
            }

            if (newChild != null)
            {
                ((ISetLogicalParent)newItem).SetParent(this);
                LogicalChildren.Add(newChild);
            }
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
                    InvalidateVisibilityChanges();
                    InvalidateMeasure();
                });
            }
        }

        private void InvalidateVisibilityChanges()
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

            bool hasChanged = false;

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

                hasChanged = true;
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

                hasChanged = true;
            }

            if (hasChanged)
            {
                if (firstItemVisible && secondItemVisible)
                {
                    var proportion = GetFirstProportion();

                    FirstItemLength = new GridLength(proportion, GridUnitType.Star);
                    SecondItemLength = new GridLength(1 - proportion, GridUnitType.Star);

                    GridSplitterVisible = true;
                }
                else
                {
                    GridSplitterVisible = false;
                }

                if (!firstItemVisible && !secondItemVisible)
                {
                    IsVisible = false;
                }
                else
                {
                    IsVisible = true;
                }
            }
        }
    }
}