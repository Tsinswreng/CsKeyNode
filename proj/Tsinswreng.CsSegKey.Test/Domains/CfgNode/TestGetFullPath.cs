// 本分片只測 ExtnCfgNode.GetFullPath：拼接完整路徑與緩存行為。
using Tsinswreng.CsSegKey;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsSegKey.Test.Domains.CfgNode;

public partial class TestCfgNode {
	public void RegisterGetFullPath(ITestNode Node) {
		var register = Node.MkTestFnRegister(
			typeof(TestCfgNode),
			[typeof(ExtnCfgNode)],
			[nameof(ExtnCfgNode.GetFullPath)],
			"FullPath"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		// 單節點：完整路徑就是自身段
		R("單節點", async (o) => {
			var N = CfgNode<object>.Mk(null, ["Lang"]);
			T(N.GetFullPath() == "Lang");
			return null;
		});

		// 多級鏈：默認分隔符 "/"
		R("多級鏈默認分隔符", async (o) => {
			var A = CfgNode<object>.Mk(null, ["A"]);
			var B = CfgNode<object>.Mk(A, ["B"]);
			var C = CfgNode<object>.Mk(B, ["C"]);
			T(C.GetFullPath() == "A/B/C");
			return null;
		});

		// 自定義分隔符（用未緩存過的路徑，避免受緩存影響）
		R("自定義分隔符", async (o) => {
			var A = CfgNode<object>.Mk(null, ["A"]);
			var B = CfgNode<object>.Mk(A, ["B"]);
			T(B.GetFullPath(".") == "A.B");
			return null;
		});

		// 第二次調用命中 _FullPathCache，返回同一字符串實例
		R("第二次返回緩存", async (o) => {
			var N = CfgNode<object>.Mk(null, ["Cached"]);
			var S1 = N.GetFullPath();
			var S2 = N.GetFullPath();
			T(ReferenceEquals(S1, S2));
			T(N._FullPathCache == S1);
			return null;
		});

		// PathSep 常量為 "/"
		R("PathSep 常量", async (o) => {
			T(CfgNode<object>.PathSep == "/");
			return null;
		});
	}
}