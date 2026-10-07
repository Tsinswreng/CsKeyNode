namespace Tsinswreng.CsKeyNode.Test;
using System.Collections;
//TODO 異常處理 勿緣用戶配置ʹ謬而致整程序崩

/// 客戶端配置鍵族。
/// 繼承 KeyNodeFamily 之後, 裸的 Mk 由基類提供, 本類不必再寫轉發方法。
public class KeysClientCfg : KeysDeclBase<KeysClientCfg>, IStaticKeysDecl{
	/// 本族共用的 Mkr。分隔符之類的配置都寫在它身上, 且必須在鍵聲明之前。
	public static MkrKeyNode _MkrKeyNode{get;} = new(){ PathSep = "." };

	// public static KeyNode Mk(){
		
	// }
	//public static FnMkKeyNode Mk = Mkr.Mk;
	
	public static KeyNode<str> Lang = Mk(null, [nameof(Lang)], "en");

	public static KeyNode<str> SqlitePath = Mk(null, [nameof(SqlitePath)], "./Ngan.Dict.sqlite");

	public static KeyNode<str> RwCfgPath = Mk(null, [nameof(RwCfgPath)], "Ngan.Dict.Rw.jsonc");

	public class Background{
		public static KeyNode _R = Mk(null, [nameof(Background)]);
		public static KeyNode<str> Mode = Mk(_R, [nameof(Mode)], "");
		public static KeyNode<IList<object?>> GalleryDirs = Mk(_R, [nameof(GalleryDirs)], (IList<object?>)[]);
	}
	public static KeyNode<str> ServerBaseUrl = Mk(null, [nameof(ServerBaseUrl)], "");
	//public static IKeyNode Ui = Mk(null, [nameof(Ui)], null);
	public static class Ui{
		public static KeyNode _R = Mk(null, [nameof(Ui)]);
		public static KeyNode<f64> BaseFontSize = Mk(_R, [nameof(BaseFontSize)], 16.0);
	}
		

	public static class User{
		public static KeyNode _R = Mk(null, [nameof(User)]);
		public static KeyNode<str> CurrentUserId = Mk(_R, [nameof(CurrentUserId)], "0");
	}

	public class Word{
		public static KeyNode _R = Mk(null, [nameof(Word)]);
		
		public static KeyNode<str> WordsPackExportPath = Mk(_R, [nameof(WordsPackExportPath)], "");
		
		public static KeyNode<str> WordsPackImportPath = Mk(_R, [nameof(WordsPackImportPath)], "");
		public static KeyNode<u64> MaxDisplayedWordCount = Mk(_R, [nameof(MaxDisplayedWordCount)], 500ul);
		
		public static KeyNode<IList<obj?>?> FilterLanguage = Mk(_R, [nameof(FilterLanguage)], (IList<obj?>?)null);
		public static KeyNode<bool> EnableRandomBackground = Mk(_R, [nameof(EnableRandomBackground)], false);
		
		public static KeyNode<bool> EnableAutoPronounce = Mk(_R, [nameof(EnableAutoPronounce)], false);
	}

	public class LlmDictionary{
		public static KeyNode _R = Mk(null, [nameof(LlmDictionary)]);
		public static KeyNode<str> ApiUrl = Mk(_R, [nameof(ApiUrl)], "");
		public static KeyNode<str> ApiKey = Mk(_R, [nameof(ApiKey)], "");
		public static KeyNode<str> Model = Mk(_R, [nameof(Model)], "");
		public static KeyNode<str> Prompt = Mk(_R, [nameof(Prompt)], "");
		/// 透傳到 LLM 請求體頂層的額外 JSON 對象。
		/// 用於承載 provider-specific 參數；空字符串表示不附加。
		public static KeyNode<str> ExtraBodyJson = Mk(_R, [nameof(ExtraBodyJson)], "");
	}

	/// 詞典頁的本地界面偏好。
	public class Dictionary{
		public static KeyNode _R = Mk(null, [nameof(Dictionary)]);
		/// 源語言快捷標籤配置。
		/// 配置文件保存為對象陣列；陣列順序即快捷欄顯示順序。
		public static KeyNode<IList<obj?>> SrcLangTags = Mk(
			_R,
			[nameof(SrcLangTags)],
			(IList<obj?>)[]
		);
	}

	/// 快捷鍵配置。
	public class Hotkey{
		public static KeyNode _R = Mk(null, [nameof(Hotkey)]);

		/// 查詞快捷鍵：觸發後讀剪貼板並打開字典查詢。
		public class DictionaryLookup{
			public static KeyNode _R = Mk(Hotkey._R, [nameof(DictionaryLookup)]);
			/// 修飾鍵，支持多值，使用 "|" 分隔，如 Ctrl|Shift。
			public static KeyNode<str> Modifiers = Mk(_R, [nameof(Modifiers)], "Alt");
			/// 主鍵，值為 EHotkeyKey 枚舉名稱，如 E/F1/Enter。
			public static KeyNode<str> Key = Mk(_R, [nameof(Key)], "E");
		}
	}
}


public class KeysRedis : KeysDeclBase<KeysClientCfg>, IStaticKeysDecl{
	public const str S = ":";
	public static MkrKeyNode _MkrKeyNode{get;} = new(){ PathSep = S };
	
	public class User{
		public static KeyNode _R = Mk(null, [nameof(User)]);
		public static str History(str IdUser){
			return _R+S+IdUser;
		}
		public static str Friend(str IdUser){
			return _R+S+IdUser+S+"friend";
		}
	}
	
}
