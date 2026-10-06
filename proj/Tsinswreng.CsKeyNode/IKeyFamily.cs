namespace Tsinswreng.CsKeyNode;

[Doc($$"""
鍵族的契約: 每個族自己帶一個 {{nameof(Mkr)}}。
{{nameof(KeysDeclBase<>)}} 只把它拿來造節點, 不看它的任何配置字段;
所以 {{nameof(MkrKeyNode)}} 以後增字段, 族在自己的初始化器裏加上就行, 基類不用跟着改。
""")]
public interface IKeyFamily{

	/// 本族共用的 Mkr。由族自己宣告、自己配置。
	/// 族類必須把它寫在鍵聲明之前——靜態初始化按書寫順序進行,
	/// 後面的鍵聲明要用到它。
	public static abstract MkrKeyNode Mkr{get;}
}
