// 本分片只測 CfgNode 的靜態工廠 Mk（obj 重載與泛型重載）。
using Tsinswreng.CsSegKey;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsSegKey.Test.Domains.CfgNode;

public partial class TestCfgNode {
	public void RegisterMk(ITestNode Node) {
		var register = Node.MkTestFnRegister(
			typeof(TestCfgNode), // tester type
			[typeof(CfgNode<>)], // testee types
			[nameof(CfgNode<object>.Mk)], // testee fn names, 必須用 nameof()
			"Mk" // optional
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		// obj 重載：第三參數傳 null 時 T2 無法推斷，命中 obj 重載，返回 CfgNode<object?>
		R("obj 重載返回 object 節點", async (o) => {
			var N = CfgNode<object>.Mk(null, ["A"], null);
			T(N is CfgNode<object>);
			T(N.RelaPathSegs.SequenceEqual(["A"]));
			T(N.Parent is null);
			T(N.DfltValueObj is null);
			return null;
		});

		// 泛型重載：型別化默認值寫入 DfltValueObj
		R("泛型重載保存型別化默認值", async (o) => {
			var N = CfgNode<object>.Mk<string>(null, ["Lang"], "en");
			T(N is CfgNode<string>);
			T(N.DfltValue == "en");
			T((string?)N.DfltValueObj == "en");
			return null;
		});

		// 泛型重載缺省參數：T2 為引用類型時 default! 即 null
		R("泛型重載缺省默認值為 null", async (o) => {
			var N = CfgNode<object>.Mk<string>(null, ["Prompt"]);
			T(N.DfltValue is null);
			T(N.DfltValueObj is null);
			return null;
		});

		// 泛型重載缺省參數：T2 為值類型時 default! 即 0
		R("泛型重載缺省默認值為 0", async (o) => {
			var N = CfgNode<object>.Mk<int>(null, ["Count"]);
			T(N.DfltValue == 0);
			return null;
		});

		// Mk 只設父鏈，不掛 Children（照搬行為）
		R("Mk 不填充 Children", async (o) => {
			var Parent = CfgNode<object>.Mk(null, ["Word"]);
			var Child = CfgNode<object>.Mk<string>(Parent, ["Name"], "x");
			T(Child.Parent == Parent);
			T(Child.Children is null);
			T(Parent.Children is null);
			return null;
		});
	}
}