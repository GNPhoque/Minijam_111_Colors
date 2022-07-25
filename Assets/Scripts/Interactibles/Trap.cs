using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trap : MonoBehaviour, IInteractible
{
	public UnityEvent OnTriggered;
	[SerializeField] bool DestroyOnUse;

	public void Use()
	{
		OnTriggered?.Invoke();
		if (DestroyOnUse) Destroy(gameObject);
	}
}
