using Tsinswreng.CsTreeTest;
using Tsinswreng.CsKeyNode.Test.Domains.KeyNode;
using Tsinswreng.CsKeyNode.Test.Domains.KeyNodeApi;
using Tsinswreng.CsKeyNode.Test.Domains.KeyNodeExtn;
using Tsinswreng.CsKeyNode.Test.Domains.KeyNodeMkr;
using Tsinswreng.CsKeyNode.Test.Domains.KeysDeclBase;
namespace Tsinswreng.CsKeyNode.Test;

public class CsKeyNodeTestMgr:DiEtTestMgr{
	public static CsKeyNodeTestMgr Inst = new();
	public override ITestNode RegisterTestsInto(ITestNode? Node){
		Node = this.TestNode;
		// 節點本體（KeyNode/IKeyNode）
		this.RegisterTester<TestKeyNode>();
		// 鍵族的對外使用面（KeysClientCfg 與字符串鍵出口）
		this.RegisterTester<TestKeyNodeApi>();
		// 造鍵器兼族配置
		this.RegisterTester<TestMkrKeyNode>();
		// 族的裸 Mk 入口
		this.RegisterTester<TestKeysDeclBase>();
		// 路徑推導擴展
		this.RegisterTester<TestIKeyNodeExtn>();
		return Node;
	}
}
