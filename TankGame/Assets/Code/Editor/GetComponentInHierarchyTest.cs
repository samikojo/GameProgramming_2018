using NUnit.Framework;
using UnityEngine;

namespace TankGame.Testing
{
	public class GetComponentInHierarchyTest
	{
		private GameObject _parent;
		private GameObject _child;
		private GameObject _grandChild;

		private GetComponentInHierarchyTester Setup
			( bool includeInactive, bool componentInParent, bool setActive )
		{
			_parent = new GameObject();
			_child = new GameObject();
			_grandChild = new GameObject();

			_child.transform.parent = _parent.transform;
			_grandChild.transform.parent = _child.transform;

			GetComponentInHierarchyTester tester =
				_child.AddComponent< GetComponentInHierarchyTester >();

			tester.Setup( includeInactive, componentInParent, setActive );

			return tester;
		}

		[Test]
		public void GetComponentInHierarchyTest_ComponentInChild_IncludeInactive_SetActive()
		{
			GetComponentInHierarchyTester tester = Setup(
				includeInactive: true,
				componentInParent: false,
				setActive: true );
			TestComponent result = tester.Run();
			Assert.NotNull( result );
		}

		[Test]
		public void GetComponentInHierarchyTest_ComponentInChild_DonIncludeInactive_SetActive()
		{
			GetComponentInHierarchyTester tester = Setup(
				includeInactive: false,
				componentInParent: false,
				setActive: true );
			TestComponent result = tester.Run();
			Assert.NotNull( result );
		}

		[Test]
		public void GetComponentInHierarchyTest_ComponentInChild_IncludeInactive_SetInactive()
		{
			GetComponentInHierarchyTester tester = Setup(
				includeInactive: true,
				componentInParent: false,
				setActive: false );
			TestComponent result = tester.Run();
			Assert.NotNull( result );
		}

		[Test]
		public void GetComponentInHierarchyTest_ComponentInChild_DontIncludeInactive_SetInactive()
		{
			GetComponentInHierarchyTester tester = Setup(
				includeInactive: false,
				componentInParent: false,
				setActive: false );
			TestComponent result = tester.Run();
			Assert.IsNull( result );
		}

		[Test]
		public void GetComponentInHierarchyTest_ComponentInParent_IncludeInactive_SetActive()
		{
			var tester = Setup( includeInactive: true, componentInParent: true, setActive: true );
			var result = tester.Run();
			Assert.NotNull( result );
		}

		[Test]
		public void GetComponentInHierarchyTest_ComponentInParent_DontIncludeInactive_SetActive()
		{
			var tester = Setup( includeInactive: false, componentInParent: true, setActive: true );
			var result = tester.Run();
			Assert.NotNull( result );
		}

		[Test]
		public void GetComponentInHierarchyTest_ComponentInParent_IncludeInactive_SetInactive()
		{
			var tester = Setup( includeInactive: true, componentInParent: true, setActive: false );
			var result = tester.Run();
			Assert.NotNull( result );
		}

		[Test]
		public void GetComponentInHierarchyTest_ComponentInParent_DontIncludeInactive_SetInactive()
		{
			var tester = Setup( includeInactive: false, componentInParent: true, setActive: false );
			var result = tester.Run();
			Assert.IsNull( result );
		}
	}
}
