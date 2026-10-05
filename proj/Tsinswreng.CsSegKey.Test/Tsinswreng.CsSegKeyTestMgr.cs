using Tsinswreng.CsTreeTest;
using Tsinswreng.CsKeyNode.Test.Domains.CfgNode;
using Tsinswreng.CsKeyNode.Test.Domains.SegKey;
namespace Tsinswreng.CsKeyNode.Test;

public class CsSegKeyTestMgr:DiEtTestMgr{
	public static CsSegKeyTestMgr Inst = new();
	public override ITestNode RegisterTestsInto(ITestNode? Node){
		Node = this.TestNode;
		this.RegisterTester<TestCfgNode>();
		this.RegisterTester<TestSegKey>();
		return Node;
	}
}
