using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using TankGame.Systems;

namespace TankGame.Testing
{
	public class FlagTests
	{

		[Test]
		public void FlagTestsCreateEnemyAndPlayerMask()
		{
			int playerLayer = LayerMask.NameToLayer( "Player" ); // 8
			int enemyLayer = LayerMask.NameToLayer( "Enemy" ); // 9

			// Mask created by our Flags class.
			int mask = Flags.CreateMask( playerLayer, enemyLayer );
			// Mask created by Unity's LayerMask class.
			int validMask = LayerMask.GetMask( "Player", "Enemy" );

			Assert.AreEqual( mask, validMask );
		}
	}
}
