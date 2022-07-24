using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
	[SerializeField] Animator animator;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.TryGetComponent<IInteractible>(out IInteractible interactible))
		{
			interactible.Use();
		}
	}
}
