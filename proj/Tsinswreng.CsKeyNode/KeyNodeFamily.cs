namespace Tsinswreng.CsKeyNode;

[Doc($$"""
一個鍵族的基類。
同一族(如一份配置、一組 Url)的鍵共用一個 {{nameof(Mkr)}} 決定分隔符,
並可用裸的 `Mk` 聲明鍵, 不必每個族自己再寫一遍轉發方法。

#Eg[
```cs
public class KeysClientCfg : KeyNodeFamily<KeysClientCfg>{
	// 本族的分隔符, 只寫這一處
	protected override str FamilyPathSep => ".";
	public static KeyNode<str> Lang = Mk(null, [nameof(Lang)], "en");
}
```
]

用法要點:
- 裸 `Mk` 不是 using static 導入來的, 而是本基類的靜態方法被子類繼承,
  類體內的簡單名查找看得到繼承來的成員。
- `Mk` 讀的是 {{nameof(I)}}(本族的單例)身上的 {{nameof(Mkr)}},
  所以每個族各有自己的分隔符, 互不影響。
""")]
public abstract class KeyNodeFamily<TSelf> where TSelf : KeyNodeFamily<TSelf>, new(){

	/// 本族的單例。
	/// 每個族(即每個封閉的 TSelf)各有一份, 記着本族的分隔符。
	public static TSelf I{get;} = new();

	/// 本族用的路徑分隔符。子類覆寫它, 例如 `"."`。默認是 `/`。
	protected virtual str FamilyPathSep => "/";

	/// 本族的 Mkr。分隔符在單例建立時寫入, 之後各族互不影響。
	public MkrKeyNode Mkr{get;}

	protected KeyNodeFamily(){
		Mkr = new MkrKeyNode{ PathSep = FamilyPathSep };
	}

	/// 造一個不帶型別信息的節點, 數據型別為 obj?。
	public static KeyNode<obj?> Mk(
		IKeyNode? Parent
		,IList<str> Path
		,obj? Data = null
	){
		return I.Mkr.Mk(Parent, Path, Data);
	}

	/// 造一個帶型別信息的節點, 數據型別由調用處推斷。
	public static KeyNode<T2> Mk<T2>(
		IKeyNode? Parent
		,IList<str> Path
		,T2 Data = default!
	){
		return I.Mkr.Mk(Parent, Path, Data);
	}
}
