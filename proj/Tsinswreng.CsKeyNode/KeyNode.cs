using Tsinswreng.CsCore;

namespace Tsinswreng.CsKeyNode;

public partial class KeyNode: IKeyNode{
	public IList<str> RelaPathSegs{get;init;} = [];
	public str PathSep{get;init;} = "/";
	public str? _FullPathCache{get;set;}
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
			_FullPathCache = this.GetFullPath();
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
	
	public T? Data{
		get=>DataObj is null?default:(T?)DataObj;
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
