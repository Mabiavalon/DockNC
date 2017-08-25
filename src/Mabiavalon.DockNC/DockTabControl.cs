using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;

namespace Mabiavalon.DockNC
{
    public class DockTabControl : SelectingItemsControl
    {
        /// <summary>
        /// Defines the <see cref="Transition"/> property.
        /// </summary>
        public static readonly StyledProperty<IPageTransition> TransitionProperty =
            Avalonia.Controls.Carousel.TransitionProperty.AddOwner<DockTabControl>();

        /// <summary>
        /// Defines an <see cref="IMemberSelector"/> that selects the content of a <see cref="TabItem"/>.
        /// </summary>
        public static readonly IMemberSelector ContentSelector =
            new FuncMemberSelector<object, object>(SelectContent);

        /// <summary>
        /// Defines an <see cref="IMemberSelector"/> that selects the header of a <see cref="TabItem"/>.
        /// </summary>
        public static readonly IMemberSelector HeaderSelector =
            new FuncMemberSelector<object, object>(SelectHeader);

        /// <summary>
        /// Defines the <see cref="TabStripPlacement"/> property.
        /// </summary>
        public static readonly StyledProperty<Dock> TabStripPlacementProperty =
            AvaloniaProperty.Register<DockTabControl, Dock>(nameof(TabStripPlacement), defaultValue: Dock.Top);

        /// <summary>
        /// Initializes static members of the <see cref="DockTabControl"/> class.
        /// </summary>
        static DockTabControl()
        {
            SelectionModeProperty.OverrideDefaultValue<DockTabControl>(SelectionMode.AlwaysSelected);
            FocusableProperty.OverrideDefaultValue<DockTabControl>(false);
            AffectsMeasure(TabStripPlacementProperty);
        }

        /// <summary>
        /// Gets the pages portion of the <see cref="DockTabControl"/>'s template.
        /// </summary>
        public IControl Pages
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the tab strip portion of the <see cref="TabControl"/>'s template.
        /// </summary>
        public IControl TabStrip
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the transition to use when switching tabs.
        /// </summary>
        public IPageTransition Transition
        {
            get { return GetValue(TransitionProperty); }
            set { SetValue(TransitionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the tabstrip placement of the tabcontrol.
        /// </summary>
        public Dock TabStripPlacement
        {
            get { return GetValue(TabStripPlacementProperty); }
            set { SetValue(TabStripPlacementProperty, value); }
        }

        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            // DockTabControl doesn't actually create items - instead its TabStrip and Carousel
            // children create the items. However we want it to be a SelectingItemsControl
            // so that it has the Items/SelectedItem etc properties. In this case, we can
            // return a null ItemContainerGenerator to disable the creation of item containers.
            return null;
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);

            TabStrip = e.NameScope.Find<IControl>("PART_TabStrip");
            Pages = e.NameScope.Find<IControl>("PART_Content");
        }

        /// <summary>
        /// Selects the content of a tab item.
        /// </summary>
        /// <param name="o">The tab item.</param>
        /// <returns>The content.</returns>
        private static object SelectContent(object o)
        {
            var content = o as IContentControl;

            if (content != null)
            {
                return content.Content;
            }
            else
            {
                return o;
            }
        }

        /// <summary>
        /// Selects the header of a tab item.
        /// </summary>
        /// <param name="o">The tab item.</param>
        /// <returns>The content.</returns>
        private static object SelectHeader(object o)
        {
            var headered = o as IHeadered;
            var control = o as IControl;

            if (headered != null)
            {
                return headered.Header ?? string.Empty;
            }
            else if (control != null)
            {
                // Non-headered control items should result in TabStripItems with empty content.
                // If a TabStrip is created with non IHeadered controls as its items, don't try to
                // display the control in the TabStripItem: the content portion will also try to 
                // display this control, resulting in dual-parentage breakage.
                return string.Empty;
            }
            else
            {
                return o;
            }
        }
    }
}
