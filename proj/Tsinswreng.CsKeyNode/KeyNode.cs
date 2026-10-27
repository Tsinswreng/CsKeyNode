using Tsinswreng.CsCore;

namespace Tsinswreng.CsKeyNode;

public partial class KeyNode: IKeyNode{
	public IReadOnlyList<str> RelaPathSegs{get;init;} = [];
	public str PathSep{get;init;} = "/";
	public str? _FullPathCache{get;set;}
	public IReadOnlyList<str>? _FullPathSegsCache{get;set;}
	public obj? DataObj{get;set;}

	public IKeyNode? Parent{get;init;}
	public IList<IKeyNode>? Children{get;set;}
	public Func<str?>? FnToString{get;set;}
	
	public KeyNode(){
		FnToString = ()=>{
			if(_FullPathCache is not null){
				return _FullPathCache;
			}
			// 全路徑要沿 Parent 上溯, 不能只 Join 自己這一段
			_FullPathCache = this.BuildFullPath();
			return _FullPathCache;
		};
	}

	public static implicit operator string?(KeyNode z){
		return z.ToString();
	}

	/// 非泛型 KeyNode 也要能轉成字符串鍵, 故在此重寫 ToString。
	public override string? ToString(){
		if(FnToString is not null){
			return FnToString();
		}
		return base.ToString();
	}
}

public partial class KeyNode<T>:KeyNode,IKeyNode<T>{
	public KeyNode(){}
	
	[Doc(@$"本節點攜帶的數據, 型別按鍵宣告的 T 解釋。
	只做強轉, 不做容錯: 型別不符拋 InvalidCastException,
	值型別且 {nameof(DataObj)} 為 null 時拋 NullReferenceException, 兩者皆屬鍵的宣告違約。
	")]
	public T Data{
		get=>(T)DataObj!;
		set=>DataObj = value;
	}

	public static implicit operator string?(KeyNode<T> z){
		return z.ToString();
	}
	public override string? ToString() {
		if(FnToString is not null){
			return FnToString();
		}
		return base.ToString();
	}

}
