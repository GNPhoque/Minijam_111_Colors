using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSign : MonoBehaviour
{
	public static int deaths = 0;

	[SerializeField] SpriteRenderer spriteRenderer;
	[SerializeField] Sprite[] sprites;
	
	private void Start()
	{
		if (deaths >= sprites.Length - 1) deaths = sprites.Length - 1;
		spriteRenderer.sprite = sprites[deaths];
	}
}
