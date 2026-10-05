// 本分片只測 CfgNode<T>.Children：默認值與手動掛接（照搬行為：不與 Parent 自動同步）。
using Tsinswreng.CsSegKey;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsSegKey.Test.Domains.CfgNode;

public partial class TestCfgNode {
	public void RegisterChildren(ITestNode Node) {
		var register = Node.MkTestFnRegister(
			typeof(TestCfgNode),
			[typeof(SegKey<object>)],
			[nameof(SegKey<object>.Children)],
			"Children"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		// 默認為 null
		R("默認是 null", async (o) => {
			var N = new SegKey<string>();
			T(N.Children is null);
			return null;
		});

		// 可手動填，但不會自動回設孩子的 Parent（照搬行為）
		R("手動掛接不動 Parent", async (o) => {
			var Parent = new SegKey<string>();
			var Child = new SegKey<string>();
			Parent.Children = [Child];
			T(Parent.Children!.Contains(Child));
			T(Child.Parent is null);
			return null;
		});
	}
}