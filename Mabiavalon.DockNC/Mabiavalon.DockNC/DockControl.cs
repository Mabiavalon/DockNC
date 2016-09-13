using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Mabiavalon.DockNC
{
	public class DockControl : ContentControl
	{
	    public void Dock(object obj, Dock dock)
	    {
	        if (Content == null)
	        {
	            switch (dock)
	            {
	                case Avalonia.Controls.Dock.Left:
                        Content = new Branch { Orientation = Orientation.Horizontal, FirstItem = obj };
                        break;
	                case Avalonia.Controls.Dock.Bottom:
                        Content = new Branch { Orientation = Orientation.Vertical, SecondItem = obj };
                        break;
	                case Avalonia.Controls.Dock.Right:
                        Content = new Branch { Orientation = Orientation.Horizontal, SecondItem = obj };
	                    break;
                    case Avalonia.Controls.Dock.Top:
                        Content = new Branch { Orientation = Orientation.Vertical, FirstItem = obj };
	                    break;
                    default:
	                    throw new ArgumentOutOfRangeException(nameof(dock), dock, null);
	            }
	            return;             
	        }


        }
	}
}
