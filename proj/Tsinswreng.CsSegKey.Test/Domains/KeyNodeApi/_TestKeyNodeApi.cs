// 一個 tester 對應一個 testee（此處是 KeyNode 的對外使用面：鍵族聲明與字符串鍵出口），放在獨立文件夾。
// 主文件以下劃線開頭：只負責組裝各分片的測試用例，不在主文件裏寫測試。
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsKeyNode.Test.Domains.KeyNodeApi;

public partial class TestKeyNodeApi : ITester {
	public TestKeyNodeApi() {
		// 鍵族 KeysClientCfg 全是靜態聲明：測試直接讀，不依賴 DI。
	}

	public ITestNode RegisterTestsInto(ITestNode? Node) {
		Node ??= new TestNode();
		// 無外部依賴的純函數測試：有序輸出。
		Node.Ordered = true;
		Node.IsParallelRecursive = false;
		RegisterToString(Node);
		return Node;
	}
}
