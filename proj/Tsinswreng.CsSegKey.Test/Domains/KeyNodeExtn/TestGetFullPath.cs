// 本分片只測 IKeyNodeExtn.GetFullPath：把全路徑的段拼成一條字符串鍵。
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsKeyNode.Test.Domains.KeyNodeExtn;

public partial class TestIKeyNodeExtn {
	public void RegisterGetFullPath(ITestNode Node) {
		var register = Node.MkTestFnRegister(
			typeof(TestIKeyNodeExtn),
			[typeof(IKeyNodeExtn)],
			[nameof(IKeyNodeExtn.BuildFullPath)],
			"FullPath"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		// 不傳 Sep 時用當前節點的 PathSep；MkrKeyNode 的默認是 "/"
		R("默認分隔符拼兩級", async (o) => {
			var Mkr = new MkrKeyNode();
			var A = Mkr.Mk(null, ["A"]);
			var B = Mkr.Mk(A, ["B"]);
			T(B.BuildFullPath() == "A/B");
			return null;
		});

		// 顯式傳 Sep 時覆蓋節點自己的分隔符
		R("顯式 Sep 覆蓋節點分隔符", async (o) => {
			var Mkr = new MkrKeyNode();
			var A = Mkr.Mk(null, ["A"]);
			var B = Mkr.Mk(A, ["B"]);
			T(B.BuildFullPath(".") == "A.B");
			return null;
		});

		// 族自己配的分隔符由 Mkr 寫進節點，拼鍵時就用它
		R("用節點自己的 PathSep", async (o) => {
			var Mkr = new MkrKeyNode{PathSep = "."};
			var Word = Mkr.Mk(null, ["Word"]);
			var FilterLanguage = Mkr.Mk(Word, ["FilterLanguage"]);
			T(FilterLanguage.BuildFullPath() == "Word.FilterLanguage");
			return null;
		});

		// 只有空段的節點拼出空串
		R("空段拼出空串", async (o) => {
			var Mkr = new MkrKeyNode();
			T(Mkr.Mk(null, []).BuildFullPath() == "");
			return null;
		});

		// 根節點帶一個空串段時，結果以分隔符開頭（Url 那類要前導 "/" 的用法）
		R("空串段令鍵以分隔符開頭", async (o) => {
			var Mkr = new MkrKeyNode();
			var Root = Mkr.Mk(null, [""]);
			var Leaf = Mkr.Mk(Root, ["a"]);
			T(Leaf.BuildFullPath() == "/a");
			return null;
		});

		// 節點自帶多段時照樣展平後拼接
		R("多段節點拼接", async (o) => {
			var Mkr = new MkrKeyNode();
			var A = Mkr.Mk(null, ["A"]);
			var XY = Mkr.Mk(A, ["X", "Y"]);
			T(XY.BuildFullPath() == "A/X/Y");
			return null;
		});

		// GetFullPath 是純推導：它不讀也不寫 _FullPathCache
		R("不填緩存", async (o) => {
			var Mkr = new MkrKeyNode();
			var N = Mkr.Mk(null, ["A"]);
			T(N.BuildFullPath() == "A");
			T(N._FullPathCache is null);
			return null;
		});
	}
}
