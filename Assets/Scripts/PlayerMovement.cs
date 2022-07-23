using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Header("MOVEMENT")]
	[SerializeField] float maxSpeed;
	[SerializeField] float moveSpeed;
	[SerializeField] float jumpSpeed;
	[SerializeField] float smoothInputSpeed;
	[SerializeField] LayerMask groundLayer;

	[Header("COMPONENTS")]
	[SerializeField] Rigidbody2D rb;
	[SerializeField] InputReplaySystem replaySystem;
	[SerializeField] SpriteRenderer spriteRenderer;

	bool mustJump;
	bool stopJump;
	Vector2 movementInput;
	Vector2 smoothInputVelocity;
	Vector2 currentMovementVector;
	Vector2 lastDirection;

	private void Start()
	{
		replaySystem.OnNewInputToPlay += ReplaySystem_OnNewInputToPlay;
	}

	private void Update()
	{
		currentMovementVector = Vector2.SmoothDamp(currentMovementVector, movementInput, ref smoothInputVelocity, smoothInputSpeed);
		if (currentMovementVector != Vector2.zero) lastDirection = currentMovementVector.normalized;

		if (currentMovementVector.x < 0) spriteRenderer.flipX = true;
		else spriteRenderer.flipX = false;
	}

	private void FixedUpdate()
	{
		Vector3 newVelocity = rb.velocity;
		newVelocity.x = currentMovementVector.x * moveSpeed;
		//rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
		if (mustJump)
		{
			mustJump = false;
			stopJump = false;
			newVelocity.y = jumpSpeed;
		}
		else if (newVelocity.y > .5f && stopJump)
		{
			newVelocity.y *= .2f;
			stopJump = false;
		}
		rb.velocity = newVelocity;
	}

	private void ReplaySystem_OnNewInputToPlay(InputRecord input)
	{
		switch (input.recordType)
		{
			case InputRecordType.Movement:
				movementInput = input.movement;
				break;
			case InputRecordType.Jump:
				if (input.jump)
				{
					if (IsGrounded())
					{
						mustJump = true;
					}
				}
				else stopJump = true;
					
				break;
		}
	}

	bool IsGrounded()
	{
		return Physics2D.OverlapCircle(rb.position, .2f, groundLayer);
	}
}
