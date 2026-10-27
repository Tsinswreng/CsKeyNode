namespace Tsinswreng.CsKeyNode;

public interface IOptKeyNode{
	
}

public class MkrKeyNode{
	public str PathSep{get;set;} = "/";
	public MkrKeyNode(){}
	
	[Doc("""
	#Eg[
	```cs
	
	```
	]
	""")]
	public KeyNode<obj?> Mk(
		IKeyNode? Parent
		,IReadOnlyList<str> Path
		,obj? Data = null
	){
		return new KeyNode<obj?>{
			PathSep = PathSep,
			RelaPathSegs = Path,
			Data = Data,
			Parent = Parent,
		};
	}
	
	public KeyNode<T2> Mk<T2>(
		IKeyNode? Parent
		,IReadOnlyList<str> Path
		,T2 Data = default!
	){
		return new KeyNode<T2>{
			PathSep = PathSep,
			RelaPathSegs = Path,
			Data = Data,
			Parent = Parent,
		};
	}
}
