// 一個 tester 對應一個 testee（此處是 KeyNode/IKeyNode 及 ExtnKeyNode 路徑擴展），放在獨立文件夾。
// 主文件以下劃線開頭：只負責組裝各分片的測試用例，不在主文件裏寫測試。
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsKeyNode.Test.Domains.KeyNodeApi;

public partial class TestKeyNodeApi : ITester {
	public TestKeyNodeApi() {
		// KeyNode 是普通具體類，測試直接 new / 用靜態 Mk，不依賴 DI。
	}

	public ITestNode RegisterTestsInto(ITestNode? Node) {
		Node ??= new TestNode();
		// 無外部依賴的純函數測試：有序輸出。
		Node.Ordered = true;
		Node.IsParallelRecursive = false;
		RegisterApi(Node);
		return Node;
	}
}
