// 本分片只測 KeyNode<T>.DfltValue 與 IKeyNode.DfltValueObj 這對雙通道。
using Tsinswreng.CsKeyNode;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsKeyNode.Test.Domains.KeyNode;

public partial class TestKeyNode {
	public void RegisterDfltValue(ITestNode Node) {
		var register = Node.MkTestFnRegister(
			typeof(TestKeyNode),
			[typeof(KeyNode<int>)],
			[nameof(KeyNode<int>.Data)],
			"DfltValue"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		// Data 只做強轉：值型別且 DataObj 為 null 時直接拋，不再回 default
		R("值型別取空值時拋", (FnTest)(async (o) => {
			var N = new KeyNode<int>();
			var Thrown = false;
			try{
				_ = N.Data;
			}catch(NullReferenceException){
				Thrown = true;
			}
			T(Thrown);
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