using Tsinswreng.CsCore;

namespace Tsinswreng.CsKeyNode;

[Doc($@"分段鍵節點。
把一串字符串片段當作路徑段, 沿 {nameof(Parent)} 上溯可還原出完整路徑;
葉子節點可經 {nameof(DataObj)} 攜帶數據。

路徑相關成員({nameof(RelaPathSegs)}、{nameof(PathSep)}、{nameof(Parent)}) 只能初始化一次,
因為全路徑會被緩存(見 {nameof(_FullPathCache)}), 事後改寫會讓緩存與實際路徑不一致。
")]
public partial interface IKeyNode{
	
	[Doc(@$"Relative Path Segments.
	without path of `{nameof(Parent)}`
	")]
	public IList<str> RelaPathSegs{get;init;}
	
	[Doc(@$"Path Separator.
	default is `/`, NOT `.`
	Eg[`Foo/Bar/Baz`]
	")]
	public str PathSep{get;init;}
	public str? _FullPathCache{get;set;}
	[Doc(@$"父節點。為 null 表示本節點是鍵樹的根; 全路徑由此鏈上溯得到。
	")]
	public IKeyNode? Parent{get;init;}
	[Doc(@$"子節點。可選, 不影響自身路徑, 建立時不會自動填充。
	")]
	public IList<IKeyNode>? Children{get;set;}
	[Doc(@$"節點攜帶的數據。
	")]
	public obj? DataObj{get;set;}
	[Doc(@$"自訂的字符串表示法。為 null 時用默認的「以 {nameof(PathSep)} 連接全路徑」。
	")]
	public Func<str?>? FnToString{get;set;}
}

public partial interface IKeyNode<T>:IKeyNode{
	
}
