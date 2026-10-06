// 本分片只測 KeyNode<T>.Children：默認值與手動掛接（照搬行為：不與 Parent 自動同步）。
using Tsinswreng.CsKeyNode;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsKeyNode.Test.Domains.KeyNode;

public partial class TestKeyNode {
	public void RegisterChildren(ITestNode Node) {
		var register = Node.MkTestFnRegister(
			typeof(TestKeyNode),
			[typeof(KeyNode<object>)],
			[nameof(KeyNode<object>.Children)],
			"Children"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		// 默認為 null
		R("默認是 null", async (o) => {
			var N = new KeyNode<string>();
			T(N.Children is null);
			return null;
		});

		// 可手動填，但不會自動回設孩子的 Parent（照搬行為）
		R("手動掛接不動 Parent", async (o) => {
			var Parent = new KeyNode<string>();
			var Child = new KeyNode<string>();
			Parent.Children = [Child];
			T(Parent.Children!.Contains(Child));
			T(Child.Parent is null);
			return null;
		});
	}
}