using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Mabiavalon.DockNC
{
	public class DockControl : ContentControl
	{
        //TODO: Edge Case: Deal with empty Branches
	    public void Dock(object obj, DockTarget dockTarget)
	    {
	        if (Content == null)
	        {
                var newBranch = new Branch();

                SetOrientation(dockTarget, newBranch);

	            if (dockTarget == DockTarget.Top || dockTarget == DockTarget.Left)
	            {
	                newBranch.FirstItem = obj;
                    newBranch.FirstItemLength = new GridLength(1, GridUnitType.Star);
                    newBranch.SecondItemLength = new GridLength(0, GridUnitType.Star);
	            }
	            else
	            {
                    newBranch.SecondItem = obj;
                    newBranch.SecondItemLength = new GridLength(1, GridUnitType.Star);
                    newBranch.FirstItemLength = new GridLength(0, GridUnitType.Star);
                }

	            Content = newBranch;
                return;
            }

	        var currentBranch = Content as Branch;

	        if (currentBranch == null)
	            throw new NotImplementedException("Support for wrapping pure content not supported yet.");

	        if (currentBranch.BranchFilled)
			{
				var newBranch = new Branch();
				Branch(obj, dockTarget, currentBranch, newBranch);

				Content = newBranch;
			}
			else
			{
				var emptyItem = GetEmptyLeaf(currentBranch);

				var oldContent = emptyItem == BranchItem.First ? currentBranch.SecondItem : currentBranch.FirstItem;

				// Possible solution to issues with visual parent?
				currentBranch.FirstItem = null;
                currentBranch.FirstContentPresenter.UpdateChild();
				currentBranch.SecondItem = null;
                currentBranch.SecondContentPresenter.UpdateChild();

				Branch(obj, dockTarget, currentBranch, oldContent);

				currentBranch.FirstItemLength = new GridLength(0.49999, GridUnitType.Star);
				currentBranch.SecondItemLength = new GridLength(0.50001, GridUnitType.Star);
			}
		}

		public void Dock(object obj, DockTarget dockTarget, Branch branch, BranchItem branchItem)
		{
			object targetBranchContent = branchItem == BranchItem.First ? branch.FirstItem : branch.SecondItem;

			if (!(targetBranchContent is Branch))
			{
			    if (branchItem == BranchItem.First)
			    {
			        branch.FirstItem = null;
			        branch.FirstContentPresenter.UpdateChild();
			    }
			    else
			    {
                    branch.SecondItem = null;
                    branch.SecondContentPresenter.UpdateChild();
                }

				var newBranch = new Branch();

				SetOrientation(dockTarget, newBranch);

				if (dockTarget == DockTarget.Top || dockTarget == DockTarget.Left)
				{
					newBranch.FirstItem = obj;
					newBranch.FirstItemLength = new GridLength(1, GridUnitType.Star);
					newBranch.SecondItemLength = new GridLength(0, GridUnitType.Star);
				}
				else
				{
					newBranch.SecondItem = obj;
					newBranch.SecondItemLength = new GridLength(1, GridUnitType.Star);
					newBranch.FirstItemLength = new GridLength(0, GridUnitType.Star);
				}

				if (branchItem == BranchItem.First)
					branch.FirstItem = newBranch;
				else
					branch.SecondItem = newBranch;

				return;
			}

			var targetBranch = (Branch) targetBranchContent;

			if (targetBranch.BranchFilled)
			{
				if (branchItem == BranchItem.First)
					branch.FirstItem = null;
				else
					branch.SecondItem = null;

				var newBranch = new Branch();

				Branch(obj, dockTarget, targetBranch, newBranch);
			}
			else
			{
				var emptyItem = GetEmptyLeaf(targetBranch);

				var oldContent = emptyItem == BranchItem.First ? targetBranch.SecondItem : targetBranch.FirstItem;

				// Possible solution to issues with visual parent?
				targetBranch.FirstItem = null;
				targetBranch.SecondItem = null;

				Branch(obj, dockTarget, targetBranch, oldContent);

				targetBranch.FirstItemLength = new GridLength(0.49999, GridUnitType.Star);
				targetBranch.SecondItemLength = new GridLength(0.50001, GridUnitType.Star);
			}
		}

		private static void Branch(object obj, DockTarget dockTarget, Branch currentBranch, object oldContent)
		{
			SetOrientation(dockTarget, currentBranch);

			if (dockTarget == DockTarget.Top || dockTarget == DockTarget.Left)
			{
				currentBranch.FirstItem = obj;
				currentBranch.SecondItem = oldContent;
			}
			else
			{
				currentBranch.FirstItem = oldContent;
				currentBranch.SecondItem = obj;
			}
		}

		private static void Branch(object obj, DockTarget dockTarget, Branch currentBranch, Branch newBranch)
		{
			SetOrientation(dockTarget, newBranch);

			if (dockTarget == DockTarget.Top || dockTarget == DockTarget.Left)
			{
				newBranch.FirstItem = obj;
				newBranch.SecondItem = currentBranch;
			}
			else
			{
				newBranch.FirstItem = currentBranch;
				newBranch.SecondItem = obj;
			}
		}

		private static BranchItem GetEmptyLeaf(Branch branch)
	    {
	        if (branch.BranchFilled)
	            throw new ArgumentException("Branch is filled", nameof(branch));

	        return branch.FirstItem == null ? BranchItem.First : BranchItem.Second;
	    }

	    private static void SetOrientation(DockTarget dockTarget, Branch newBranch)
	    {
	        if (dockTarget == DockTarget.Top || dockTarget == DockTarget.Bottom)
	            newBranch.Orientation = Orientation.Vertical;
	        else
	            newBranch.Orientation = Orientation.Horizontal;
	    }

		private static Orientation GetOrientation(DockTarget dockTarget)
		{
			return dockTarget == DockTarget.Top || dockTarget == DockTarget.Bottom ? Orientation.Vertical : Orientation.Horizontal;
		}

	}
}
