namespace Tsinswreng.CsSegKey;

[Doc(@$"分段鍵的路徑推導擴展。
兩個函數都只是由 {nameof(ISegKey.RelaPathSegs)}、{nameof(ISegKey.Parent)}、{nameof(ISegKey.PathSep)} 推導出來的簡便寫法,
沒有多態與重寫需求, 故放擴展而非成員。
")]
public static class ISegKeyExtn{

	[Doc($"""
	沿父鏈上溯, 收集全路徑的路徑段, 順序為父先子後。
	#Eg[
	```cs
	var Mkr = new MkrSegKey();
	var A = Mkr.MkNonLeaf(null, ["A"]);
	var XY = Mkr.MkNonLeaf(A, ["X", "Y"]);
	// XY.GetFullPathSegs() 是 ["A", "X", "Y"]
	```
	]
	""")]
	public static IList<str> GetFullPathSegs(
		this ISegKey z
	){
		// step 1: 自當前節點沿 Parent 逐層上溯, 收集每層自己的相對路徑段
		var List2D = new List<IList<str>>();
		ISegKey? Cur = z;
		while(Cur is not null){
			List2D.Add(Cur.RelaPathSegs);
			Cur = Cur.Parent;
		}
		// step 2: 上溯得到的是子先父後, 反轉成父先子後
		List2D.Reverse();
		// step 3: 逐層展平成一維的段列表
		var R = new List<str>();
		foreach(var Segs in List2D){
			R.AddRange(Segs);
		}
		return R;
	}

	[Doc($"""
	把全路徑的段以 {nameof(Sep)} 連接成一條字符串鍵。
	{nameof(Sep)} 省略時取當前節點的 {nameof(ISegKey.PathSep)}。
	#Eg[
	```cs
	var Mkr = new MkrSegKey();
	var A = Mkr.MkNonLeaf(null, ["A"]);
	var B = Mkr.MkNonLeaf(A, ["B"]);
	// B.GetFullPath() 是 "A/B"; B.GetFullPath(".") 是 "A.B"
	```
	]
	""")]
	public static str GetFullPath(
		this ISegKey z
		,str? Sep = null
	){
		return str.Join(Sep ?? z.PathSep, z.GetFullPathSegs());
	}
}