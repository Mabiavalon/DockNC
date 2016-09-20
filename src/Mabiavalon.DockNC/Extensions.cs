using System;
namespace Mabiavalon.DockNC
{
	public static class Extensions
	{
		public static BranchAccessor Visit<TContext>(
			this BranchAccessor branchAccessor,
			TContext context,
			BranchItem childItem,
			Action<TContext, BranchAccessor> branchVisitor = null,
			Action<TContext, object> contentVisitor = null)
		{
			if (branchAccessor == null) throw new ArgumentNullException("branchAccessor");

			branchAccessor.Visit(
				childItem,
				WrapVisitor(context, branchVisitor),
				WrapVisitor(context, contentVisitor)
				);

			return branchAccessor;
		}

		private static Action<TVisitArg> WrapVisitor<TContext, TVisitArg>(TContext context, Action<TContext, TVisitArg> visitor)
		{
			if (visitor == null) return null;

			return a => visitor(context, a);
		}
	}
}
