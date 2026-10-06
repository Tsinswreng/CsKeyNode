// 一個 tester 對應一個 testee（此處是 IKeyNodeExtn，由節點推導路徑的擴展），放在獨立文件夾。
// 主文件以下劃線開頭：只負責組裝各分片的測試用例，不在主文件裏寫測試。
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsKeyNode.Test.Domains.KeyNodeExtn;

public partial class TestIKeyNodeExtn : ITester {
	public TestIKeyNodeExtn() {
		// IKeyNodeExtn 是純靜態擴展：測試直接造 KeyNode，不依賴 DI。
	}

	public ITestNode RegisterTestsInto(ITestNode? Node) {
		Node ??= new TestNode();
		// 無外部依賴的純函數測試：有序輸出。
		Node.Ordered = true;
		Node.IsParallelRecursive = false;
		RegisterGetFullPathSegs(Node);
		RegisterGetFullPath(Node);
		return Node;
	}
}
