using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankGame.Persistence
{
	public class SaveSystem
	{
		private IPersistence _persistence;

		public SaveSystem( IPersistence persistence )
		{
			_persistence = persistence;
		}
	}
}
