// 本分片只測 CfgNode<T>.DfltValue 與 ICfgNode.DfltValueObj 這對雙通道。
using Tsinswreng.CsKeyNode;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsKeyNode.Test.Domains.CfgNode;

public partial class TestCfgNode {
	public void RegisterDfltValue(ITestNode Node) {
		var register = Node.MkTestFnRegister(
			typeof(TestCfgNode),
			[typeof(KeyNode<int>)],
			[nameof(KeyNode<int>.Data)],
			"DfltValue"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		// DfltValueObj 為 null 時，DfltValue 返回 default（值類型為 0）
		R("null 時返回 default", (FnTest)(async (o) => {
			var N = new KeyNode<int>();
			T(N.Data == 0);
			return null;
		}));

		// DfltValue 寫入會同步到 DfltValueObj
		R("DfltValue 寫穿 DfltValueObj", (FnTest)(async (o) => {
			var N = new KeyNode<int>();
			N.Data = 5;
			T(N.Data is 5);
			T(N.Data == 5);
			return null;
		}));

		// DfltValueObj 直接設值，DfltValue 讀出轉換結果
		R("DfltValueObj 直接設值", (FnTest)(async (o) => {
			var N = new KeyNode<long>();
			N.Data = 7L;
			T(N.Data == 7L);
			return null;
		}));

		// 引用類型：字符串默認值
		R("字符串默認值", (FnTest)(async (o) => {
			var N = new KeyNode<string>();
			N.Data = "en";
			T((string?)N.Data == "en");
			T(N.Data == "en");
			return null;
		}));
	}
}