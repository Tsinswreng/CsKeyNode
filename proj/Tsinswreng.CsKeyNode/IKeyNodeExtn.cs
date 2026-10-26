namespace Tsinswreng.CsKeyNode;

[Doc(@$"分段鍵的路徑推導擴展。
兩個函數都只是由 {nameof(IKeyNode.RelaPathSegs)}、{nameof(IKeyNode.Parent)}、{nameof(IKeyNode.PathSep)} 推導出來的簡便寫法,
沒有多態與重寫需求, 故放擴展而非成員。
")]
public static class IKeyNodeExtn{
	extension<T>(IKeyNode<T> z){
		[Doc(@$"本節點攜帶的數據, 型別按鍵宣告的 T 解釋。
		與 `KeyNode<T>.Data` 同義, 是同一份契約的兩個入口:
		實例屬性供 `new KeyNode<T>` 的物件初始化器使用, 本擴展供靜態型別為 `IKeyNode<T>` 時使用。
		兩處的型別與轉型規則必須保持一致, 改一處就要同改另一處。
		")]
		public T Data{
			get=>(T)z.DataObj!;
			set=>z.DataObj = value;
		}
	}
	
	extension(IKeyNode z){
		public str FullPath{
			get{
				return z.ToString()!;
			}
		}

		[Doc(@$"全路徑的段, 順序為父先子後, 走緩存。
		首次取用時計算並存入 {nameof(IKeyNode._FullPathSegsCache)}, 之後回傳同一份列表。
		回傳的是緩存本體, 調用方不得就地修改; 要改請自己複製一份。
		與 {nameof(BuildFullPathSegs)} 的差別只在緩存: 後者每次都重新推導。
		")]
		public IList<str> FullPathSegs{
			get{
				return z._FullPathSegsCache ??= z.BuildFullPathSegs();
			}
		}
	}

	[Doc($"""
	沿父鏈上溯, 收集全路徑的路徑段, 順序為父先子後。
	#Eg[
	```cs
	var Mkr = new MkrKeyNode();
	var A = Mkr.Mk(null, ["A"]);
	var XY = Mkr.Mk(A, ["X", "Y"]);
	// XY.GetFullPathSegs() 是 ["A", "X", "Y"]
	```
	]
	""")]
	public static IList<str> BuildFullPathSegs(
		this IKeyNode z
	){
		// step 1: 自當前節點沿 Parent 逐層上溯, 收集每層自己的相對路徑段
		var List2D = new List<IList<str>>();
		IKeyNode? Cur = z;
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
	{nameof(Sep)} 省略時取當前節點的 {nameof(IKeyNode.PathSep)}。
	#Eg[
	```cs
	var Mkr = new MkrKeyNode();
	var A = Mkr.Mk(null, ["A"]);
	var B = Mkr.Mk(A, ["B"]);
	// B.GetFullPath() 是 "A/B"; B.GetFullPath(".") 是 "A.B"
	```
	]
	""")]
	public static str BuildFullPath(
		this IKeyNode z
		,str? Sep = null
	){
		return str.Join(Sep ?? z.PathSep, z.BuildFullPathSegs());
	}
}
