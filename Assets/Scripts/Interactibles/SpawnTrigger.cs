using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour, IInteractible
{
	public void Use()
	{
		GameManager.instance.SummonGhost();
		Destroy(gameObject);
	}
}
