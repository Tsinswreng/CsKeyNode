using Tsinswreng.CsCore;

namespace Tsinswreng.CsSegKey;

public partial class SegKey: ISegKey{
	public IList<str> RelaPathSegs{get;init;} = [];
	public str PathSep{get;init;} = "/";
	public str? _FullPathCache{get;set;}
	public obj? DataObj{get;set;}

	public ISegKey? Parent{get;init;}
	public IList<ISegKey>? Children{get;set;}
	public Func<str?>? FnToString{get;set;}
	
	public SegKey(){
		FnToString = ()=>{
			if(_FullPathCache is not null){
				return _FullPathCache;
			}
			// 全路徑要沿 Parent 上溯, 不能只 Join 自己這一段
			_FullPathCache = this.GetFullPath();
			return _FullPathCache;
		};
	}

	public static implicit operator string?(SegKey z){
		return z.ToString();
	}

	/// 非泛型 SegKey 也要能轉成字符串鍵, 故在此重寫 ToString。
	public override string? ToString(){
		if(FnToString is not null){
			return FnToString();
		}
		return base.ToString();
	}
}

public partial class SegKey<T>:SegKey,ISegKey<T>{
	public SegKey(){}
	
	public T? Data{
		get=>DataObj is null?default:(T?)DataObj;
		set=>DataObj = value;
	}

	public static implicit operator string?(SegKey<T> z){
		return z.ToString();
	}
	public override string? ToString() {
		if(FnToString is not null){
			return FnToString();
		}
		return base.ToString();
	}

	
	[Doc("""
	#Eg[
	```cs
	
	```
	]
	""")]
	public static SegKey<obj?> Mk(
		ISegKey? Parent
		,IList<str> Path
		,obj? Data = null
	){
		return new SegKey<obj?>{
			RelaPathSegs=Path, Data=Data, Parent=Parent
		};
	}
	
	public static SegKey<T2> Mk<T2>(
		ISegKey? Parent
		,IList<str> Path
		,T2 Data = default!
	){
		return new SegKey<T2>{
			RelaPathSegs=Path, Data=Data, Parent=Parent
		};
	}
}