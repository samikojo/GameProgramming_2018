using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TankGame.Localization;

namespace TankGame.UI
{
	public class LocalizedLabel : MonoBehaviour
	{
		[SerializeField]
		private Text _text;

		[SerializeField]
		private string _key;

		private void Awake()
		{
			Localization.Localization.LanguageLoaded += OnLanguageLoaded;
		}

		private void OnLanguageLoaded(LangCode currentLanguage)
		{
			_text.text = 
				Localization.Localization.CurrentLanguage.GetTranslation( _key );
		}

		void Start()
		{
			OnLanguageLoaded(Localization.Localization.CurrentLanguage.LanguageCode);
		}
	}
}
