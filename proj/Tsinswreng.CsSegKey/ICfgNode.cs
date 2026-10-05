using Tsinswreng.CsCore;

namespace Tsinswreng.CsSegKey;


public partial interface ISegKey{
	
	[Doc(@$"Relative Path Segments.
	without path of `{nameof(Parent)}`
	")]
	public IList<str> RelaPathSegs{get;set;}
	
	[Doc(@$"Path Separator.
	default is `/`, NOT `.`
	Eg[`Foo/Bar/Baz`]
	")]
	public str PathSep{get;set;}
	public str? _FullPathCache{get;set;}
	public ISegKey? Parent{get;set;}
	public IList<ISegKey>? Children{get;set;}
	public obj? DataObj{get;set;}
	public Func<str?>? FnToString{get;set;}
}

public partial interface ISegKey<T>:ISegKey{
	
}
