using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour, IInteractible
{
	public UnityEvent OnCollected;

	public void Use()
	{
		OnCollected?.Invoke();
		SoundManager.instance.PlayCoin();
		//GameManager.instance.CollectCoin();
		Destroy(gameObject);
	}
}
