using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Mabiavalon.DockNC
{
	public class DockControl : DockPanel
	{
		public static readonly AttachedProperty<Dock> DockAtProperty =
			AvaloniaProperty.RegisterAttached<DockControl, DockItem, Dock>("DockAt");

		public static readonly StyledProperty<bool> LastChildFillProperty =
			DockPanel.LastChildFillProperty.AddOwner<DockControl>();

		public DockControl()
		{
			AffectsArrange(DockAtProperty);
		}

		public static Dock GetDockAt(DockItem dockItem)
		{
			return dockItem.GetValue(DockAtProperty);
		}

		public static void SetDockAt(DockItem dockItem, Dock value)
		{
			dockItem.SetValue(DockAtProperty, value);
		}
	}
}
