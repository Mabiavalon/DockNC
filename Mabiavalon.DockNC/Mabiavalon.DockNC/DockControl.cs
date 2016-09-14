using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Mabiavalon.DockNC
{
	public class DockControl : ContentControl
	{
	    public void Dock(object obj, DockTarget dockTarget)
	    {
	        if (Content == null)
	        {
	            switch (dockTarget)
	            {
	                case DockTarget.Left:
                        Content = new Branch { Orientation = Orientation.Horizontal, FirstItem = obj };
                        break;
	                case DockTarget.Bottom:
                        Content = new Branch { Orientation = Orientation.Vertical, SecondItem = obj };
                        break;
	                case DockTarget.Right:
                        Content = new Branch { Orientation = Orientation.Horizontal, SecondItem = obj };
	                    break;
                    case DockTarget.Top:
                        Content = new Branch { Orientation = Orientation.Vertical, FirstItem = obj };
	                    break;
                    default:
	                    throw new ArgumentOutOfRangeException(nameof(dockTarget), dockTarget, null);
	            }
	            return;             
	        }


        }
	}
}
