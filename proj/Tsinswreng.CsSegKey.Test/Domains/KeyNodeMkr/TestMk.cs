// 本分片只測 MkrKeyNode.Mk：造節點，並把本族的配置（分隔符）寫進節點。
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsKeyNode.Test.Domains.KeyNodeMkr;

public partial class TestMkrKeyNode {
	public void RegisterMk(ITestNode Node) {
		var register = Node.MkTestFnRegister(
			typeof(TestMkrKeyNode), // tester type
			[typeof(MkrKeyNode)], // testee types
			[nameof(MkrKeyNode.Mk)], // testee fn names, 必須用 nameof()
			"Mk" // optional
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		// 默認分隔符是 "/"，會被寫進造出來的節點
		R("默認分隔符寫進節點", async (o) => {
			var Mkr = new MkrKeyNode();
			var N = Mkr.Mk(null, ["A"]);
			T(N.PathSep == "/");
			return null;
		});

		// 族自配的分隔符會被寫進造出來的節點
		R("自配分隔符寫進節點", async (o) => {
			var Mkr = new MkrKeyNode{PathSep = "."};
			var N = Mkr.Mk(null, ["A"]);
			T(N.PathSep == ".");
			return null;
		});

		// obj 重載：段、父、數據的初始值
		R("obj 重載的初始值", async (o) => {
			var Mkr = new MkrKeyNode();
			var N = Mkr.Mk(null, ["A"]);
			T(N.RelaPathSegs.SequenceEqual(["A"]));
			T(N.Parent is null);
			T(N.Data is null);
			T(N.Children is null);
			return null;
		});

		// 只掛父鏈，不回填父的 Children
		R("造子節點不回填 Children", async (o) => {
			var Mkr = new MkrKeyNode();
			var A = Mkr.Mk(null, ["A"]);
			var B = Mkr.Mk(A, ["B"]);
			T(B.Parent == A);
			T(A.Children is null);
			return null;
		});

		// 泛型重載：型別化默認值直接放進節點
		R("泛型重載保留型別化默認值", async (o) => {
			var Mkr = new MkrKeyNode();
			var N = Mkr.Mk<str>(null, ["Lang"], "en");
			T(N is KeyNode<str>);
			T(N.Data == "en");
			return null;
		});

		// 泛型重載缺省參數：引用型別 default! 即 null
		R("泛型重載缺省默認為 null", async (o) => {
			var Mkr = new MkrKeyNode();
			var N = Mkr.Mk<str>(null, ["Prompt"]);
			T(N.Data is null);
			return null;
		});

		// 泛型重載缺省參數：值型別 default! 即 0
		R("泛型重載缺省值型別為 0", async (o) => {
			var Mkr = new MkrKeyNode();
			var N = Mkr.Mk<i32>(null, ["Count"]);
			T(N.Data == 0);
			return null;
		});

		// 兩個 Mkr 各自記自己的配置，互不影響
		R("兩個 Mkr 互不影響", async (o) => {
			var MkrDot = new MkrKeyNode{PathSep = "."};
			var MkrSlash = new MkrKeyNode{PathSep = "/"};
			var A = MkrDot.Mk(null, ["A"]);
			var B = MkrSlash.Mk(null, ["A"]);
			T(A.PathSep == ".");
			T(B.PathSep == "/");
			return null;
		});

		// 造出來的節點已經有默認的字符串表示法，且緩存還沒被填
		R("新節點有默認表示法且緩存為空", async (o) => {
			var Mkr = new MkrKeyNode();
			var N = Mkr.Mk(null, ["A"]);
			T(N.FnToString is not null);
			T(N._FullPathCache is null);
			return null;
		});
	}
}
