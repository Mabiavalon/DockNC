using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace Mabiavalon.DockNC
{
    public interface IInterLayoutClient
    {
        INewTabHost<Control> GetNewHost(object partition, DockControl source);
    }
}