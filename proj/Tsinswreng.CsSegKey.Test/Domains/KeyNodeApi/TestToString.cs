// 本分片只測 KeyNode.ToString 與隱式轉字符串：鍵聲明到字符串鍵的出口。
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsKeyNode.Test.Domains.KeyNodeApi;

public partial class TestKeyNodeApi {
	public void RegisterToString(ITestNode Node) {
		var register = Node.MkTestFnRegister(
			typeof(TestKeyNodeApi), // tester type
			[typeof(KeyNode<object>)], // testee types
			[nameof(KeyNode<object>.ToString)], // testee fn names, 必須用 nameof()
			"ToString" // optional
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		// 族裏的鍵：全路徑沿父鏈拼，分隔符用族 Mkr 配好的 "."
		R("族鍵拼出全路徑", async (o) => {
			T(KeysClientCfg.Word.FilterLanguage.ToString() == "Word.FilterLanguage");
			return null;
		});

		// 隱式轉字符串與 ToString 同源
		R("隱式轉字符串", async (o) => {
			str? S = KeysClientCfg.Lang;
			T(S == "Lang");
			T(KeysClientCfg.Lang.ToString() == "Lang");
			return null;
		});

		// 第二次轉字符串命中 _FullPathCache，返回同一實例
		R("第二次命中緩存", async (o) => {
			var Mkr = new MkrKeyNode();
			var N = Mkr.Mk(null, ["Cached"]);
			var S1 = N.ToString();
			var S2 = N.ToString();
			T(ReferenceEquals(S1, S2));
			T(N._FullPathCache == S1);
			return null;
		});

		// 自訂 FnToString 時走自訂那份，不再用默認的全路徑
		R("自訂 FnToString 優先", async (o) => {
			var Mkr = new MkrKeyNode();
			var N = Mkr.Mk(null, ["A"]);
			N.FnToString = () => "custom";
			T(N.ToString() == "custom");
			return null;
		});

		// 非泛型 KeyNode 也走同一條出口
		// 註：...Test.Domains 底下有同名的 KeyNode 子命名空間，非泛型的型別名會被它遮蔽，故寫全。
		R("非泛型節點轉字符串", async (o) => {
			global::Tsinswreng.CsKeyNode.KeyNode N = new(){RelaPathSegs = ["A"], PathSep = "/"};
			T(N.ToString() == "A");
			str? S = N;
			T(S == "A");
			return null;
		});

		// 多段與多級混合時，全路徑仍是一條以族分隔符連接的鍵
		R("多段多級拼接", async (o) => {
			var Mkr = new MkrKeyNode{PathSep = "."};
			var A = Mkr.Mk(null, ["A", "B"]);
			var C = Mkr.Mk(A, ["C"]);
			T(C.ToString() == "A.B.C");
			return null;
		});
	}
}
