// 本分片只測 IKeyNodeExtn.GetFullPathSegs：沿父鏈上溯，收集全路徑的路徑段。
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsKeyNode.Test.Domains.KeyNodeExtn;

public partial class TestIKeyNodeExtn {
	public void RegisterGetFullPathSegs(ITestNode Node) {
		var register = Node.MkTestFnRegister(
			typeof(TestIKeyNodeExtn), // tester type
			[typeof(IKeyNodeExtn)], // testee types
			[nameof(IKeyNodeExtn.GetFullPathSegs)], // testee fn names, 必須用 nameof()
			"FullPathSegs" // optional
		);
		var R = register.Register;
		var T = Assert.IsTrue;
		// 樹一律用 MkrKeyNode 造。
		var Mkr = new MkrKeyNode();

		// 單節點：全路徑段就是自己那一段
		R("單節點返回自身段", async (o) => {
			var N = Mkr.Mk(null, ["Lang"]);
			T(N.GetFullPathSegs().SequenceEqual(["Lang"]));
			return null;
		});

		// 多級鏈：上溯得到的是子先父後，要反轉成父先子後
		R("多級鏈按父先子後", async (o) => {
			var A = Mkr.Mk(null, ["A"]);
			var B = Mkr.Mk(A, ["B"]);
			var C = Mkr.Mk(B, ["C"]);
			T(C.GetFullPathSegs().SequenceEqual(["A", "B", "C"]));
			return null;
		});

		// 一個節點可自帶多段，展平後接在父鏈之後
		R("多段節點展平", async (o) => {
			var A = Mkr.Mk(null, ["A"]);
			var XY = Mkr.Mk(A, ["X", "Y"]);
			T(XY.GetFullPathSegs().SequenceEqual(["A", "X", "Y"]));
			return null;
		});

		// 根節點的空段列表不貢獻任何段
		R("空段列表不貢獻段", async (o) => {
			var N = Mkr.Mk(null, []);
			T(N.GetFullPathSegs().Count == 0);
			return null;
		});

		// 葉子的空段不貢獻段，但父層照樣保留
		R("葉子空段仍保留父層", async (o) => {
			var A = Mkr.Mk(null, ["A"]);
			var Leaf = Mkr.Mk(A, []);
			T(Leaf.GetFullPathSegs().SequenceEqual(["A"]));
			return null;
		});

		// 返回的是新列表：改動它不影響節點本身
		R("返回新列表", async (o) => {
			var A = Mkr.Mk(null, ["A"]);
			A.GetFullPathSegs().Add("Z");
			T(A.GetFullPathSegs().Count == 1);
			return null;
		});

		// 上溯只讀 Parent，不會順手回寫 Children
		R("上溯不回寫 Children", async (o) => {
			var A = Mkr.Mk(null, ["A"]);
			var B = Mkr.Mk(A, ["B"]);
			T(B.GetFullPathSegs().SequenceEqual(["A", "B"]));
			T(A.Children is null);
			return null;
		});
	}
}
