using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TankGame.Localization
{
	public class LocalizationNotFoundException : FileNotFoundException
	{
		public LangCode Language { get; private set; }

		public LocalizationNotFoundException( LangCode language )
		{
			Language = language;
		}

		public override string Message
		{
			get { return "Localization can not be found for language " +
					Language; }
		}
	}
}
