using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestArgs : EventArgs
{
	public string Arg { get; set; }
}

public class EventTrigger : MonoBehaviour
{
	public delegate void EventHandler(object sender, TestArgs args);

	public static event EventHandler Event;

	public void Trigger()
	{
		if ( Event != null )
		{
			var args = new TestArgs { Arg = "Testing" };
			Event(this, args);
		}
	}
}
