// 一個 tester 對應一個 testee（此處是 KeyNode/IKeyNode 本體），放在獨立文件夾。
// 路徑推導（IKeyNodeExtn）與造鍵器（MkrKeyNode）各有自己的 tester，不在本文件。
// 主文件以下劃線開頭：只負責組裝各分片的測試用例，不在主文件裏寫測試。
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.CsKeyNode.Test.Domains.KeyNode;

public partial class TestKeyNode : ITester {
	public TestKeyNode() {
		// KeyNode 是普通具體類，測試直接 new，不依賴 DI。
	}

	public ITestNode RegisterTestsInto(ITestNode? Node) {
		Node ??= new TestNode();
		// 無外部依賴的純函數測試：有序輸出。
		Node.Ordered = true;
		Node.IsParallelRecursive = false;
		RegisterDfltValue(Node);
		RegisterChildren(Node);
		return Node;
	}
}
