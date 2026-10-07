// 本分片只測 KeysDeclBase<TSelf>.Mk：族類用繼承來的裸 Mk 聲明鍵，實際造鍵的是本族的 MkrKeyNode。
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsKeyNode.Test.Domains.KeysDeclBase;

public partial class TestKeysDeclBase {
	public void RegisterMk(ITestNode Node) {
		var register = Node.MkTestFnRegister(
			typeof(TestKeysDeclBase), // tester type
			[typeof(KeysDeclBase<>)], // testee types
			[nameof(KeysDeclBase<KeysTestDot>.Mk)], // testee fn names, 必須用 nameof()
			"Mk" // optional
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		// 裸 Mk 造出的節點，分隔符來自本族的 Mkr，而不是庫的默認值
		R("裸 Mk 用本族分隔符", async (o) => {
			T(KeysTestDot.Word.Name.PathSep == ".");
			T(KeysTestDot.Word.Name.BuildFullPath() == "Word.Name");
			return null;
		});

		// 另一族的鍵不受族一影響：同一段聲明寫法，拼出的鍵不同
		R("另一族各記各的分隔符", async (o) => {
			T(KeysTestSlash.Word.Name.PathSep == "/");
			T(KeysTestSlash.Word.Name.BuildFullPath() == "Word/Name");
			return null;
		});

		// 節點上的分隔符就是本族 Mkr 上的那個值
		R("節點分隔符取自本族 Mkr", async (o) => {
			T(KeysTestDot.Lang.PathSep == KeysTestDot._MkrKeyNode.PathSep);
			T(KeysTestSlash.Lang.PathSep == KeysTestSlash._MkrKeyNode.PathSep);
			return null;
		});

		// 泛型入口：型別由調用處推斷，數據留在節點上
		R("泛型入口推斷型別並保留數據", async (o) => {
			T(KeysTestDot.Lang is KeyNode<str>);
			T(KeysTestDot.Lang.Data == "en");
			T(KeysTestSlash.Lang.Data == "en");
			return null;
		});

		// obj 入口：不傳數據時，唯一的數據通道 DataObj 是 null
		R("obj 入口數據為 null", async (o) => {
			T(KeysTestDot.Word._R.DataObj is null);
			return null;
		});

		// 同族的鍵可以串成父鏈：父節點是本族另一個鍵聲明
		R("父子鍵串成路徑", async (o) => {
			T(KeysTestDot.Word.Name.Parent == KeysTestDot.Word._R);
			T(KeysTestDot.Word.Name.BuildFullPathSegs().SequenceEqual(["Word", "Name"]));
			return null;
		});
	}
}
