using Tsinswreng.CsCore;

namespace Tsinswreng.CsSegKey;

public partial class SegKey: ISegKey{
	public IList<str> RelaPathSegs{get;set;} = [];
	public str PathSep{get;set;} = "/";
	public str? _FullPathCache{get;set;}
	public obj? DataObj{get;set;}

	public ISegKey? Parent{get;set;}
	public IList<ISegKey>? Children{get;set;}
	public Func<str?>? FnToString{get;set;}
	
	public SegKey(){
		FnToString = ()=>{
			if(_FullPathCache is not null){
				return _FullPathCache;
			}
			_FullPathCache = str.Join(PathSep, RelaPathSegs);
			return _FullPathCache;
		};
	}

	public static implicit operator string?(SegKey z){
		return z.ToString();
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
