using System;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.Localization
{
	[Serializable]
	public class Language
	{
		[SerializeField]
		private List< string > _keys = new List< string >();

		[SerializeField]
		private List< string > _values = new List< string >();

		[SerializeField]
		private LangCode _langCode;

		public LangCode LanguageCode
		{
			get { return _langCode; }
			set { _langCode = value; }
		}

		public Language()
		{
			LanguageCode = LangCode.NA;
			Debug.Log( "Language created but not initialized" );
		}

		public Language( LangCode language )
		{
			LanguageCode = language;
			Debug.Log( "Language created and initialized" );
		}

		public string GetTranslation( string key )
		{
			string result = null;
			int index = _keys.IndexOf( key );
			if ( index >= 0 )
			{
				result = _values[ index ];
			}

			return result;
		}

		public Dictionary< string, string > GetValues()
		{
			var result = new Dictionary< string, string >();
			for ( int i = 0; i < _keys.Count; ++i )
			{
				result.Add( _keys[ i ], _values[ i ] );
			}

			return result;
		}

#if UNITY_EDITOR
		public void SetValues( Dictionary< string, string > translations )
		{
			// Clear the lists before adding new values
			_keys.Clear();
			_values.Clear();

			foreach ( var translation in translations )
			{
				_keys.Add( translation.Key );
				_values.Add( translation.Value );
			}
		}
#endif
	}
}
