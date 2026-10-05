// // 本分片只測 ExtnCfgNode.GetFullPathSegs：沿父鏈收集路徑段。
// using Tsinswreng.CsSegKey;
// using Tsinswreng.CsTreeTest;

// namespace Tsinswreng.CsSegKey.Test.Domains.CfgNode;

// public partial class TestCfgNode {
// 	public void RegisterGetFullPathSegs(ITestNode Node) {
// 		var register = Node.MkTestFnRegister(
// 			typeof(TestCfgNode),
// 			[typeof(CfgNodeExtn)],
// 			[nameof(CfgNodeExtn.GetFullPathSegs)],
// 			"FullPathSegs"
// 		);
// 		var R = register.Register;
// 		var T = Assert.IsTrue;

// 		// 單節點：路徑段就是自身段列表
// 		R("單節點返回自身段", async (o) => {
// 			var N = SegKey<object>.Mk(null, ["Lang"]);
// 			T(N.GetFullPathSegs().SequenceEqual(["Lang"]));
// 			return null;
// 		});

// 		// 多級鏈：父先子後，與宣告順序一致
// 		R("多級鏈按父先子後", async (o) => {
// 			var A = SegKey<object>.Mk(null, ["A"]);
// 			var B = SegKey<object>.Mk(A, ["B"]);
// 			var C = SegKey<object>.Mk(B, ["C"]);
// 			T(C.GetFullPathSegs().SequenceEqual(["A", "B", "C"]));
// 			return null;
// 		});

// 		// 節點自身可以持有多段，展平後排在前綴之後
// 		R("多段節點展平", async (o) => {
// 			var A = SegKey<object>.Mk(null, ["A"]);
// 			var XY = SegKey<object>.Mk(A, ["X", "Y"]);
// 			T(XY.GetFullPathSegs().SequenceEqual(["A", "X", "Y"]));
// 			return null;
// 		});

// 		// 空段列表的節點返回空結果
// 		R("空段列表返回空", async (o) => {
// 			var N = SegKey<object>.Mk(null, []);
// 			T(N.GetFullPathSegs().Count == 0);
// 			return null;
// 		});
// 	}
// }
