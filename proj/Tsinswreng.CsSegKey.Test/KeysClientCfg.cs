namespace Tsinswreng.CsSegKey.Test;
using System.Collections;
//TODO 異常處理 勿緣用戶配置ʹ謬而致整程序崩

/// 客戶端配置鍵族。
/// 繼承 MkrSegKeyFamily 之後, 裸的 Mk 由基類提供, 本類不必再寫轉發方法。
public class KeysClientCfg : MkrSegKeyFamily<KeysClientCfg>{
	/// 本族的分隔符。整族只寫這一處。
	protected override str FamilyPathSep => ".";

	// public static SegKey Mk(){
		
	// }
	//public static FnMkSegKey Mk = Mkr.Mk;
	
	public static SegKey<str> Lang = Mk(null, [nameof(Lang)], "en");

	public static SegKey<str> SqlitePath = Mk(null, [nameof(SqlitePath)], "./Ngan.Dict.sqlite");

	public static SegKey<str> RwCfgPath = Mk(null, [nameof(RwCfgPath)], "Ngan.Dict.Rw.jsonc");

	public class Background{
		public static SegKey _R = Mk(null, [nameof(Background)]);
		public static SegKey<str> Mode = Mk(_R, [nameof(Mode)], "");
		public static SegKey<IList<object?>> GalleryDirs = Mk(_R, [nameof(GalleryDirs)], (IList<object?>)[]);
	}
	public static SegKey<str> ServerBaseUrl = Mk(null, [nameof(ServerBaseUrl)], "");
	//public static ICfgNode Ui = Mk(null, [nameof(Ui)], null);
	public static class Ui{
		public static SegKey _R = Mk(null, [nameof(Ui)]);
		public static SegKey<f64> BaseFontSize = Mk(_R, [nameof(BaseFontSize)], 16.0);
	}
		

	public static class User{
		public static SegKey _R = Mk(null, [nameof(User)]);
		public static SegKey<str> CurrentUserId = Mk(_R, [nameof(CurrentUserId)], "0");
	}

	public class Word{
		public static SegKey _R = Mk(null, [nameof(Word)]);
		
		public static SegKey<str> WordsPackExportPath = Mk(_R, [nameof(WordsPackExportPath)], "");
		
		public static SegKey<str> WordsPackImportPath = Mk(_R, [nameof(WordsPackImportPath)], "");
		public static SegKey<u64> MaxDisplayedWordCount = Mk(_R, [nameof(MaxDisplayedWordCount)], 500ul);
		
		public static SegKey<IList<obj?>?> FilterLanguage = Mk(_R, [nameof(FilterLanguage)], (IList<obj?>?)null);
		public static SegKey<bool> EnableRandomBackground = Mk(_R, [nameof(EnableRandomBackground)], false);
		
		public static SegKey<bool> EnableAutoPronounce = Mk(_R, [nameof(EnableAutoPronounce)], false);
	}

	public class LlmDictionary{
		public static SegKey _R = Mk(null, [nameof(LlmDictionary)]);
		public static SegKey<str> ApiUrl = Mk(_R, [nameof(ApiUrl)], "");
		public static SegKey<str> ApiKey = Mk(_R, [nameof(ApiKey)], "");
		public static SegKey<str> Model = Mk(_R, [nameof(Model)], "");
		public static SegKey<str> Prompt = Mk(_R, [nameof(Prompt)], "");
		/// 透傳到 LLM 請求體頂層的額外 JSON 對象。
		/// 用於承載 provider-specific 參數；空字符串表示不附加。
		public static SegKey<str> ExtraBodyJson = Mk(_R, [nameof(ExtraBodyJson)], "");
	}

	/// 詞典頁的本地界面偏好。
	public class Dictionary{
		public static SegKey _R = Mk(null, [nameof(Dictionary)]);
		/// 源語言快捷標籤配置。
		/// 配置文件保存為對象陣列；陣列順序即快捷欄顯示順序。
		public static SegKey<IList<obj?>> SrcLangTags = Mk(
			_R,
			[nameof(SrcLangTags)],
			(IList<obj?>)[]
		);
	}

	/// 快捷鍵配置。
	public class Hotkey{
		public static SegKey _R = Mk(null, [nameof(Hotkey)]);

		/// 查詞快捷鍵：觸發後讀剪貼板並打開字典查詢。
		public class DictionaryLookup{
			public static SegKey _R = Mk(Hotkey._R, [nameof(DictionaryLookup)]);
			/// 修飾鍵，支持多值，使用 "|" 分隔，如 Ctrl|Shift。
			public static SegKey<str> Modifiers = Mk(_R, [nameof(Modifiers)], "Alt");
			/// 主鍵，值為 EHotkeyKey 枚舉名稱，如 E/F1/Enter。
			public static SegKey<str> Key = Mk(_R, [nameof(Key)], "E");
		}
	}
}