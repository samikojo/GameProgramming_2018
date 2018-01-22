using NUnit.Framework;
using UnityEngine;

namespace TankGame.Testing
{
	public class GetOrAddComponentTest
	{
		[Test]
		public void NewEditModeTestSimplePasses()
		{
			GameObject go = new GameObject();
			TestComponent component = go.GetOrAddComponent< TestComponent >();
			Assert.NotNull( component );
		}

		[Test]
		public void GetOrAddComponentTest_OnlyOneComponent()
		{
			GameObject go = new GameObject();
			go.GetOrAddComponent< TestComponent >();
			go.GetOrAddComponent< TestComponent >();
			go.GetOrAddComponent< TestComponent >();
			go.GetOrAddComponent< TestComponent >();
			TestComponent[] components = go.GetComponents< TestComponent >();
			Assert.IsTrue( components.Length == 1 );
		}
	}
}
