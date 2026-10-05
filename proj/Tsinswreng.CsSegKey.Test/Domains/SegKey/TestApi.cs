// 本分片只測 CfgNode<T>.Children：默認值與手動掛接（照搬行為：不與 Parent 自動同步）。
using Tsinswreng.CsSegKey;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsSegKey.Test.Domains.SegKey;



public partial class TestSegKey {
	public void RegisterApi(ITestNode Node) {
		var register = Node.MkTestFnRegister(
			typeof(TestSegKey),
			[typeof(SegKey<object>)],
			[nameof(SegKey<object>.Children)],
			"Api"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		str? s = KeysClientCfg.Word.FilterLanguage;
		System.Console.WriteLine(s);
		//T(s == "KeysClientCfg.Word.FilterLanguage");
		
	}
}
