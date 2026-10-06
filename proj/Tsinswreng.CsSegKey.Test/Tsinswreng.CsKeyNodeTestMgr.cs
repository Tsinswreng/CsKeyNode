using Tsinswreng.CsTreeTest;
using Tsinswreng.CsKeyNode.Test.Domains.KeyNode;
using Tsinswreng.CsKeyNode.Test.Domains.KeyNodeApi;
namespace Tsinswreng.CsKeyNode.Test;

public class CsKeyNodeTestMgr:DiEtTestMgr{
	public static CsKeyNodeTestMgr Inst = new();
	public override ITestNode RegisterTestsInto(ITestNode? Node){
		Node = this.TestNode;
		this.RegisterTester<TestKeyNode>();
		this.RegisterTester<TestKeyNodeApi>();
		return Node;
	}
}
