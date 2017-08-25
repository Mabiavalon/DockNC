using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;

namespace Mabiavalon.DockNC.Docking
{
    public class Layout : ContentControl
    {
        private static readonly HashSet<Layout> LoadedLayouts = new HashSet<Layout>();

        public static readonly StyledProperty<IInterLayoutClient> InterLayoutClientProperty =
            AvaloniaProperty.Register<Layout, IInterLayoutClient>("InterLayoutClient");

        public IInterLayoutClient InterLayoutClient
        {
            get { return GetValue(InterLayoutClientProperty); }
            set { SetValue(InterLayoutClientProperty, value); }
        }

        public Layout()
        {

        }

        protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            LoadedLayouts.Add(this);
            base.OnAttachedToLogicalTree(e);
        }

        protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            LoadedLayouts.Remove(this);
            base.OnDetachedFromLogicalTree(e);
        }

        public static IEnumerable<Layout> GetLoadedInstances()
        {
            return LoadedLayouts.ToList();
        }

        public string Partition { get; set; }

        internal static bool IsContainedWithinBranch(Control control)
        {
            do
            {
                control = control.GetLogicalParent<Control>();
                if (control is Branch)
                    return true;
            } while (control != null);
            return false;
        }
    }
}