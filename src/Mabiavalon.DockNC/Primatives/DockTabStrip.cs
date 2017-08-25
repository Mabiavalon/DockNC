using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Input;

namespace Mabiavalon.DockNC
{
    public class DockTabStrip : SelectingItemsControl
    {
        private static readonly FuncTemplate<IPanel> DefaultPanel =
           new FuncTemplate<IPanel>(() => new WrapPanel { Orientation = Orientation.Horizontal });

        private static IMemberSelector s_MemberSelector = new FuncMemberSelector<object, object>(SelectHeader);

        static DockTabStrip()
        {
            MemberSelectorProperty.OverrideDefaultValue<DockTabStrip>(s_MemberSelector);
            SelectionModeProperty.OverrideDefaultValue<DockTabStrip>(SelectionMode.AlwaysSelected);
            FocusableProperty.OverrideDefaultValue(typeof(DockTabStrip), false);
            ItemsPanelProperty.OverrideDefaultValue<DockTabStrip>(DefaultPanel);
        }

        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            return new ItemContainerGenerator<TabStripItem>(
                this,
                ContentControl.ContentProperty,
                ContentControl.ContentTemplateProperty);
        }

        /// <inheritdoc/>
        protected override void OnGotFocus(GotFocusEventArgs e)
        {
            base.OnGotFocus(e);

            if (e.NavigationMethod == NavigationMethod.Directional)
            {
                e.Handled = UpdateSelectionFromEventSource(e.Source);
            }
        }

        /// <inheritdoc/>
        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);

            if (e.MouseButton == MouseButton.Left)
            {
                e.Handled = UpdateSelectionFromEventSource(e.Source);
            }
        }

        private static object SelectHeader(object o)
        {
            var headered = o as IHeadered;
            return (headered != null) ? (headered.Header ?? string.Empty) : o;
        }
    }
}
