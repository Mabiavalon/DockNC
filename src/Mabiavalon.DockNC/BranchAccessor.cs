using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.VisualTree;

namespace Mabiavalon.DockNC
{
    public class BranchAccessor
    {
		private readonly Branch _branch;
		private readonly BranchAccessor _firstItemBranchAccessor;
		private readonly BranchAccessor _secondItemBranchAccessor;
		private readonly Control _firstItemControl;
		private readonly Control _secondItemControl;

		public BranchAccessor(Branch branch)
		{
			if (branch == null) throw new ArgumentNullException(nameof(branch));

			_branch = branch;

			var firstChildBranch = branch.FirstItem as Branch;
			if (firstChildBranch != null)
				_firstItemBranchAccessor = new BranchAccessor(firstChildBranch);
			else
				_firstItemControl = FindControl(branch.FirstItem, branch.FirstContentPresenter);

			var secondChildBranch = branch.SecondItem as Branch;
			if (secondChildBranch != null)
				_secondItemBranchAccessor = new BranchAccessor(secondChildBranch);
			else
				_secondItemControl = FindControl(branch.SecondItem, branch.SecondContentPresenter);
		}

		// Probably don't neeeed.
		private static Control FindControl(object item, Control contentPresenter)
		{
			var result = item as Control;
			return result ?? contentPresenter.GetVisualDescendents().OfType<Control>().FirstOrDefault();
		}

		public Branch Branch
		{
			get { return _branch; }
		}

		public BranchAccessor FirstItemBranchAccessor
		{
			get { return _firstItemBranchAccessor; }
		}

		public BranchAccessor SecondItemBranchAccessor
		{
			get { return _secondItemBranchAccessor; }
		}

		public Control FirstItemControl
		{
			get { return _firstItemControl; }
		}

		public Control SecondItemControl
		{
			get { return _secondItemControl; }
		}
	}
}
