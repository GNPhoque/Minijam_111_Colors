using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelDoor : MonoBehaviour, IInteractible
{
	public void Use()
	{
		Destroy(gameObject);
	}
}
