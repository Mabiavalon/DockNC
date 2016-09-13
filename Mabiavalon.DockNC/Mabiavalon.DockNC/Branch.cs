using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Styling;

namespace Mabiavalon.DockNC
{
    public class Branch : TemplatedControl
    {
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
            PseudoClass(OrientationProperty, o => o == Avalonia.Controls.Orientation.Vertical, ":vertical");
            PseudoClass(OrientationProperty, o => o == Avalonia.Controls.Orientation.Horizontal, ":horizontal");
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
    }
}
