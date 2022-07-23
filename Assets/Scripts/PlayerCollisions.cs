using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.TryGetComponent<IInteractible>(out IInteractible interactible))
		{
			interactible.Use();
		}
	}
}