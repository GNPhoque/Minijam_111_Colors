using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trap : MonoBehaviour, IInteractible
{
	public UnityEvent OnTriggered;

	public void Use()
	{
		OnTriggered?.Invoke();
	}
}
