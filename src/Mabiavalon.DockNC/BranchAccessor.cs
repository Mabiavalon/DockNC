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
		private readonly object _firstItemContent;
		private readonly object _secondItemContent;

		public BranchAccessor(Branch branch)
		{
			if (branch == null) throw new ArgumentNullException(nameof(branch));

			_branch = branch;

			var firstChildBranch = branch.FirstItem as Branch;
			if (firstChildBranch != null)
				_firstItemBranchAccessor = new BranchAccessor(firstChildBranch);
			else
				_firstItemContent = branch.FirstItem;

			var secondChildBranch = branch.SecondItem as Branch;
			if (secondChildBranch != null)
				_secondItemBranchAccessor = new BranchAccessor(secondChildBranch);
			else
				_secondItemContent = branch.SecondItem;
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

		public object FirstItemContent
		{
			get { return _firstItemContent; }
		}

		public object SecondItemContent
		{
			get { return _secondItemContent; }
		}

		public BranchAccessor Visit(BranchItem childItem, Action<BranchAccessor> childBranchVisitor = null, Action<object> childContentVisitor = null)
		{
			Func<BranchAccessor> branchGetter;
			Func<object> contentGetter;

			switch (childItem)
			{
				case BranchItem.First:
					branchGetter = () => _firstItemBranchAccessor;
					contentGetter = () => _branch.FirstItem;
					break;
				case BranchItem.Second:
					branchGetter = () => _secondItemBranchAccessor;
					contentGetter = () => _branch.SecondItem;
					break;
				default:
					throw new ArgumentOutOfRangeException("childItem");
			}

			var branchDescription = branchGetter();
			if (branchDescription != null)
			{
				if (childBranchVisitor != null)
					childBranchVisitor(branchDescription);
				return this;
			}

			if (childContentVisitor == null) return this;

			var content = contentGetter();
			if (content != null)
				childContentVisitor(content);

			return this;
		}
	}
}
