using System.Collections;
using System.Collections.Generic;
using TankGame.Localization;
using UnityEngine;

namespace TankGame.UI
{
	public class LocalizationUI : MonoBehaviour
	{
		public void SetEnglish()
		{
			Localization.Localization.LoadLanguage(LangCode.EN);
		}

		public void SetFinnish()
		{
			Localization.Localization.LoadLanguage(LangCode.FI);
		}
	}
}
