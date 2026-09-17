using Tsinswreng.CsTreeTest;
using Tsinswreng.CsSegKey.Test.Domains.CfgNode;
namespace Tsinswreng.CsSegKey.Test;

public class CsSegKeyTestMgr:DiEtTestMgr{
	public static CsSegKeyTestMgr Inst = new();
	public override ITestNode RegisterTestsInto(ITestNode? Node){
		Node = this.TestNode;
		this.RegisterTester<TestCfgNode>();
		return Node;
	}
}