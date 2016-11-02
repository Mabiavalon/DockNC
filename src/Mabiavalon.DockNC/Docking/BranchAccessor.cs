using System;

namespace Mabiavalon.DockNC.Docking
{
    public class BranchAccessor
    {
        public BranchAccessor(Branch branch)
        {
            if (branch == null) throw new ArgumentNullException(nameof(branch));

            Branch = branch;

            var firstChildBranch = branch.FirstItem as Branch;
            if (firstChildBranch != null)
                FirstItemBranchAccessor = new BranchAccessor(firstChildBranch);
            else
                FirstItemContent = branch.FirstItem;

            var secondChildBranch = branch.SecondItem as Branch;
            if (secondChildBranch != null)
                SecondItemBranchAccessor = new BranchAccessor(secondChildBranch);
            else
                SecondItemContent = branch.SecondItem;
        }

        public Branch Branch { get; }

        public BranchAccessor FirstItemBranchAccessor { get; }

        public BranchAccessor SecondItemBranchAccessor { get; }

        public object FirstItemContent { get; }

        public object SecondItemContent { get; }

        public BranchAccessor Visit(BranchItem childItem, Action<BranchAccessor> childBranchVisitor = null,
            Action<object> childContentVisitor = null)
        {
            Func<BranchAccessor> branchGetter;
            Func<object> contentGetter;

            switch (childItem)
            {
                case BranchItem.First:
                    branchGetter = () => FirstItemBranchAccessor;
                    contentGetter = () => Branch.FirstItem;
                    break;
                case BranchItem.Second:
                    branchGetter = () => SecondItemBranchAccessor;
                    contentGetter = () => Branch.SecondItem;
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