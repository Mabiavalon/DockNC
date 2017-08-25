using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace Mabiavalon.DockNC
{
    public class NewTabHost<TControl> : INewTabHost<TControl> where TControl : Control
    {
        private readonly TControl _container;
        private readonly DockControl _dockControl;

        public NewTabHost(TControl container, DockControl dockControl)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (dockControl == null) throw new ArgumentNullException(nameof(dockControl));

            _container = container;
            _dockControl = dockControl;
        }

        public TControl Container
        {
            get { return _container; }
        }

        public DockControl DockControl
        {
            get { return _dockControl; }
        }
    }
}