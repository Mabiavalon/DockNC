using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace Mabiavalon.DockNC
{
    public interface INewTabHost<out TControl> where TControl : Control
    {
        TControl Container { get; }
        DockControl DockControl { get; }
    }
}