using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Examples
{
	public class ExtensionTestRunner : MonoBehaviour
	{
		[SerializeField]
		private ExtensionTest _test;

		// Use this for initialization
		void Start()
		{
			_test.Run();
		}
	}
}
