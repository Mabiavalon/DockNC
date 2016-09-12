﻿using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Mabiavalon.DockNC
{
	public class DockControl : DockPanel
	{
		public DockControl()
		{
            AffectsArrange(DockProperty);
            LastChildFillProperty.OverrideDefaultValue<DockControl>(true);
		}
	}
}
