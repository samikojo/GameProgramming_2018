using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace TankGame.Localization
{
	public enum LangCode
	{
		NA = 0,
		EN = 1,
		FI = 2
	}

	public static class Localization
	{
		public const string LocalizationFolderName = "Localization";
		public const string FileExtension = ".json";

		public static event Action<LangCode> LanguageLoaded;

		public static string LocalizationPath
		{
			get
			{
				return Path.Combine( Application.streamingAssetsPath,
					LocalizationFolderName );
			}
		}

		// Currently loaded language object.
		public static Language CurrentLanguage { get; private set; }

		public static string GetLocalizationFilePath( LangCode langCode )
		{
			return Path.Combine( LocalizationPath, langCode.ToString() ) +
			       FileExtension;
		}

		public static void SaveCurrentLanguage()
		{
			if ( CurrentLanguage == null ||
			     CurrentLanguage.LanguageCode == LangCode.NA )
			{
				// There is no language loaded!
				return;
			}

			if ( !Directory.Exists( LocalizationPath ) )
			{
				// Localization directory does not exist. Let's create one.
				Directory.CreateDirectory( LocalizationPath );
			}

			// Serialize the language file and write the serialized content
			// to the file.
			string path = GetLocalizationFilePath( CurrentLanguage.LanguageCode );
			string serializedLanguage = JsonUtility.ToJson( CurrentLanguage );
			File.WriteAllText( path, serializedLanguage, Encoding.UTF8 );
		}

		public static void LoadLanguage( LangCode langCode )
		{
			var path = GetLocalizationFilePath( langCode );
			if ( File.Exists( path ) )
			{
				// Language exists
				string jsonLanguage = File.ReadAllText( path );
				CurrentLanguage = JsonUtility.FromJson< Language >( jsonLanguage );
			}
			else
			{
				CreateLanguage(langCode);
			}

			if (LanguageLoaded != null)
			{
				LanguageLoaded(CurrentLanguage.LanguageCode);
			}
		}

		public static void CreateLanguage( LangCode langCode )
		{
			CurrentLanguage = new Language( langCode );
		}
	}
}
