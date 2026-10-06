namespace Tsinswreng.CsKeyNode;

[Doc($$"""
鍵族的 Mk 入口。
族類繼承本類之後, 類體內就能用裸的 `Mk` 聲明鍵, 不必自己再寫一遍轉發方法。
裸 `Mk` 不是 using static 導入來的, 而是本類的靜態方法被子類繼承,
類體內的簡單名查找看得到繼承來的成員。

#Eg[
```cs
public class KeysClientCfg : KeyNodeFamily<KeysClientCfg>, IKeyFamily{
	// 本族的 Mkr, 由族自己配置; 要寫在鍵聲明之前
	public static MkrKeyNode Mkr{get;} = new(){ PathSep = "." };
	public static KeyNode<str> Lang = Mk(null, [nameof(Lang)], "en");
}
```
]

本類不持狀態, 也不知道 {{nameof(MkrKeyNode)}} 有哪些配置字段,
只把 {{nameof(IKeyFamily.Mkr)}} 拿來用。所以 Mkr 增字段時本類不用改。
""")]
public abstract class KeysDeclBase<TSelf> where TSelf : KeysDeclBase<TSelf>, IKeyFamily{

	/// 造一個不帶型別信息的節點, 數據型別為 obj?。
	public static KeyNode<obj?> Mk(
		IKeyNode? Parent
		,IList<str> Path
		,obj? Data = null
	){
		return TSelf.Mkr.Mk(Parent, Path, Data);
	}

	/// 造一個帶型別信息的節點, 數據型別由調用處推斷。
	public static KeyNode<T2> Mk<T2>(
		IKeyNode? Parent
		,IList<str> Path
		,T2 Data = default!
	){
		return TSelf.Mkr.Mk(Parent, Path, Data);
	}
}
