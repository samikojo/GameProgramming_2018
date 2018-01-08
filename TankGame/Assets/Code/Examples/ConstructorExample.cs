using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Examples
{
	public class ConstructorExample : MonoBehaviour
	{
		void Start()
		{
			var example = new GrandChildClass();
		}

	}

	public class BaseClass
	{
		private int _number = 1;

		public BaseClass()
		{
			Init();
			Debug.Log( 1 );
		}

		public virtual void Init()
		{
			
		}
	}

	public class ChildClass : BaseClass
	{
		private int _number = 2;

		public ChildClass()
		{
			Debug.Log( 2 );
		}

		public ChildClass( int number )
		{
			_number = number;
		}
	}

	public class GrandChildClass : ChildClass
	{
		private int _number;

		public GrandChildClass()
		{
			_number = 3;
			Debug.Log( 3 );
		}

		public GrandChildClass( int number ) : this()
		{
			_number = number;
		}

		public override void Init()
		{
			Debug.Log( "GrandChild:" + _number );
		}
	}
}
