using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IInteractible
{
	public void Use()
	{
		GameManager.instance.CollectCoin();
		Destroy(gameObject);
	}
}
