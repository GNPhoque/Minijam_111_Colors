using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour, IInteractible
{
	public void Use()
	{
		GameManager.instance.LoadNextScene();
	}
}
