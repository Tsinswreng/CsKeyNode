// 一個 tester 對應一個 testee（此處是 MkrKeyNode，造鍵器兼族配置），放在獨立文件夾。
// 文件夾／命名空間不叫 MkrKeyNode，是為了不讓命名空間遮蔽同名的型別。
// 主文件以下劃線開頭：只負責組裝各分片的測試用例，不在主文件裏寫測試。
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsKeyNode.Test.Domains.KeyNodeMkr;

public partial class TestMkrKeyNode : ITester {
	public TestMkrKeyNode() {
		// MkrKeyNode 是普通具體類：測試直接 new，不依賴 DI。
	}

	public ITestNode RegisterTestsInto(ITestNode? Node) {
		Node ??= new TestNode();
		// 無外部依賴的純函數測試：有序輸出。
		Node.Ordered = true;
		Node.IsParallelRecursive = false;
		RegisterMk(Node);
		return Node;
	}
}
