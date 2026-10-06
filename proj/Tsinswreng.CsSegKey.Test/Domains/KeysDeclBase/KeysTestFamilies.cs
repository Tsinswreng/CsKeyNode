// 測試用的鍵族樣本（fixture）：每個族自己宣告、自己配置一個 MkrKeyNode，
// 鍵聲明裹用 KeysDeclBase 提供的裸 Mk。
// 兩個族的分隔符故意不同，用來驗證「族與族各記各的 Mkr」。
// 註：本命名空間的祖先（...Test.Domains）底下有同名的 KeyNode／MkrKeyNode 子命名空間，
// 非泛型的型別名會被那兩個命名空間遮蔽，故這裡的非泛型引用一律用 global:: 寫全。
namespace Tsinswreng.CsKeyNode.Test.Domains.KeysDeclBase;

/// 樣本族一：分隔符是 "."。
public class KeysTestDot : KeysDeclBase<KeysTestDot>, IStaticKeysDecl{
	/// 本族共用的 Mkr。必須寫在鍵聲明之前——靜態初始化按書寫順序進行。
	public static global::Tsinswreng.CsKeyNode.MkrKeyNode _MkrKeyNode{get;} = new(){PathSep = "."};

	public static global::Tsinswreng.CsKeyNode.KeyNode<str> Lang = Mk(null, [nameof(Lang)], "en");

	public class Word{
		public static global::Tsinswreng.CsKeyNode.KeyNode _R = Mk(null, [nameof(Word)]);
		public static global::Tsinswreng.CsKeyNode.KeyNode<str> Name = Mk(_R, [nameof(Name)], "");
	}
}

/// 樣本族二：分隔符是 "/"，與族一並存。
public class KeysTestSlash : KeysDeclBase<KeysTestSlash>, IStaticKeysDecl{
	/// 本族共用的 Mkr。必須寫在鍵聲明之前——靜態初始化按書寫順序進行。
	public static global::Tsinswreng.CsKeyNode.MkrKeyNode _MkrKeyNode{get;} = new(){PathSep = "/"};

	public static global::Tsinswreng.CsKeyNode.KeyNode<str> Lang = Mk(null, [nameof(Lang)], "en");

	public class Word{
		public static global::Tsinswreng.CsKeyNode.KeyNode _R = Mk(null, [nameof(Word)]);
		public static global::Tsinswreng.CsKeyNode.KeyNode<str> Name = Mk(_R, [nameof(Name)], "");
	}
}
