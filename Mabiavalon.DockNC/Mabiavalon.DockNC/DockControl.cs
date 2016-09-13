using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Mabiavalon.DockNC
{
	public class DockControl : ContentControl
	{
	    public void Dock(Control control, Dock dock)
	    {
	        if (Content == null)
	        {
	            switch (dock)
	            {
	                case Avalonia.Controls.Dock.Left:
                        Content = new Branch { Orientation = Orientation.Horizontal, FirstItem = control };
                        break;
	                case Avalonia.Controls.Dock.Bottom:
                        Content = new Branch { Orientation = Orientation.Vertical, SecondItem = control };
                        break;
	                case Avalonia.Controls.Dock.Right:
                        Content = new Branch { Orientation = Orientation.Horizontal, SecondItem = control };
	                    break;
                    case Avalonia.Controls.Dock.Top:
                        Content = new Branch { Orientation = Orientation.Vertical, FirstItem = control };
	                    break;
                    default:
	                    throw new ArgumentOutOfRangeException(nameof(dock), dock, null);
	            }
	            return;             
	        }


        }
	}
}
