// 本分片只測 CfgNode<T>.DfltValue 與 ICfgNode.DfltValueObj 這對雙通道。
using Tsinswreng.CsSegKey;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsSegKey.Test.Domains.CfgNode;

public partial class TestCfgNode {
	public void RegisterDfltValue(ITestNode Node) {
		var register = Node.MkTestFnRegister(
			typeof(TestCfgNode),
			[typeof(CfgNode<int>)],
			[nameof(CfgNode<int>.DfltValue)],
			"DfltValue"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		// DfltValueObj 為 null 時，DfltValue 返回 default（值類型為 0）
		R("null 時返回 default", async (o) => {
			var N = new CfgNode<int>();
			T(N.DfltValue == 0);
			return null;
		});

		// DfltValue 寫入會同步到 DfltValueObj
		R("DfltValue 寫穿 DfltValueObj", async (o) => {
			var N = new CfgNode<int>();
			N.DfltValue = 5;
			T(N.DfltValueObj is 5);
			T(N.DfltValue == 5);
			return null;
		});

		// DfltValueObj 直接設值，DfltValue 讀出轉換結果
		R("DfltValueObj 直接設值", async (o) => {
			var N = new CfgNode<long>();
			N.DfltValueObj = 7L;
			T(N.DfltValue == 7L);
			return null;
		});

		// 引用類型：字符串默認值
		R("字符串默認值", async (o) => {
			var N = new CfgNode<string>();
			N.DfltValue = "en";
			T((string?)N.DfltValueObj == "en");
			T(N.DfltValue == "en");
			return null;
		});
	}
}