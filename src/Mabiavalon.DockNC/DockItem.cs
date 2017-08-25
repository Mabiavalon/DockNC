using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Mixins;
using Avalonia.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mabiavalon.DockNC
{
    public class DockTabItem : HeaderedContentControl, ISelectable
    {
        /// <summary>
        /// Defines the <see cref="IsSelected"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsSelectedProperty =
            ListBoxItem.IsSelectedProperty.AddOwner<DockTabItem>();

        /// <summary>
        /// Initializes static members of the <see cref="TabItem"/> class.
        /// </summary>
        static DockTabItem()
        {
            SelectableMixin.Attach<DockTabItem>(IsSelectedProperty);
            FocusableProperty.OverrideDefaultValue(typeof(DockTabItem), true);
        }

        /// <summary>
        /// Gets or sets the selection state of the item.
        /// </summary>
        public bool IsSelected
        {
            get { return GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
    }
}
