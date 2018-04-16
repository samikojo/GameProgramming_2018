using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener : MonoBehaviour
{
	private void OnEnable ()
	{
		EventTrigger.Event += OnEvent;
	}

	private void OnDisable()
	{
		EventTrigger.Event -= OnEvent;
	}

	private void OnEvent(object senter, TestArgs args)
	{
		Debug.Log( args.Arg );
	}
}
