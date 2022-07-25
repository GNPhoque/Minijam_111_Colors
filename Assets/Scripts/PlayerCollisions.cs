using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
	[SerializeField] Animator animator;
	[SerializeField] InputReplaySystem player;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ghost"))
		{
			animator.SetTrigger("Death");
			if(player) player.enabled = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.TryGetComponent<IInteractible>(out IInteractible interactible))
		{
			interactible.Use();
		}
	}
}
