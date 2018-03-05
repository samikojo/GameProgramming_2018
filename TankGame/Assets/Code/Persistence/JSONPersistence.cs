using System;
using UnityEngine;
using System.IO;
using System.Text;

namespace TankGame.Persistence
{
	public class JSONPersistence: IPersistence
	{
		public string Extension { get { return ".json"; } }
		public string FilePath { get; private set; }

		public JSONPersistence( string path )
		{
			FilePath = path + Extension;
		}

		public void Save< T >( T data )
		{
			string jsonData = JsonUtility.ToJson( data, prettyPrint: true );
			File.WriteAllText( FilePath, jsonData, Encoding.UTF8 );
		}

		public T Load< T >()
		{
			string jsonData = File.ReadAllText( FilePath, Encoding.UTF8 );
			return JsonUtility.FromJson< T >( jsonData );
		}
	}
}
