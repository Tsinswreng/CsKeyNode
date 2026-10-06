// // 本分片只測 ExtnKeyNode.GetFullPathSegs：沿父鏈收集路徑段。
// using Tsinswreng.CsKeyNode;
// using Tsinswreng.CsTreeTest;

// namespace Tsinswreng.CsKeyNode.Test.Domains.KeyNode;

// public partial class TestKeyNode {
// 	public void RegisterGetFullPathSegs(ITestNode Node) {
// 		var register = Node.MkTestFnRegister(
// 			typeof(TestKeyNode),
// 			[typeof(KeyNodeExtn)],
// 			[nameof(KeyNodeExtn.GetFullPathSegs)],
// 			"FullPathSegs"
// 		);
// 		var R = register.Register;
// 		var T = Assert.IsTrue;

// 		// 單節點：路徑段就是自身段列表
// 		R("單節點返回自身段", async (o) => {
// 			var N = KeyNode<object>.Mk(null, ["Lang"]);
// 			T(N.GetFullPathSegs().SequenceEqual(["Lang"]));
// 			return null;
// 		});

// 		// 多級鏈：父先子後，與宣告順序一致
// 		R("多級鏈按父先子後", async (o) => {
// 			var A = KeyNode<object>.Mk(null, ["A"]);
// 			var B = KeyNode<object>.Mk(A, ["B"]);
// 			var C = KeyNode<object>.Mk(B, ["C"]);
// 			T(C.GetFullPathSegs().SequenceEqual(["A", "B", "C"]));
// 			return null;
// 		});

// 		// 節點自身可以持有多段，展平後排在前綴之後
// 		R("多段節點展平", async (o) => {
// 			var A = KeyNode<object>.Mk(null, ["A"]);
// 			var XY = KeyNode<object>.Mk(A, ["X", "Y"]);
// 			T(XY.GetFullPathSegs().SequenceEqual(["A", "X", "Y"]));
// 			return null;
// 		});

// 		// 空段列表的節點返回空結果
// 		R("空段列表返回空", async (o) => {
// 			var N = KeyNode<object>.Mk(null, []);
// 			T(N.GetFullPathSegs().Count == 0);
// 			return null;
// 		});
// 	}
// }
