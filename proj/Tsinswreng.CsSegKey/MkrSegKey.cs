namespace Tsinswreng.CsSegKey;


public delegate SegKey<obj?> FnMkSegKey(
	ISegKey? Parent
	,IList<str> Path
	,obj? Data = null
);
public delegate SegKey<T> FnMkSegKey<T>(
	ISegKey? Parent
	,IList<str> Path
	,T Data = default!
);

public class MkrSegKey{
	public str PathSep{get;set;} = "/";
	public MkrSegKey(){}
	
	[Doc("""
	#Eg[
	```cs
	
	```
	]
	""")]
	public SegKey<obj?> Mk(
		ISegKey? Parent
		,IList<str> Path
		,obj? Data = null
	){
		return new SegKey<obj?>{
			PathSep = PathSep,
			RelaPathSegs = Path,
			Data = Data,
			Parent = Parent,
		};
	}
	
	public SegKey<T2> Mk<T2>(
		ISegKey? Parent
		,IList<str> Path
		,T2 Data = default!
	){
		return new SegKey<T2>{
			PathSep = PathSep,
			RelaPathSegs = Path,
			Data = Data,
			Parent = Parent,
		};
	}
}