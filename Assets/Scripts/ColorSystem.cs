using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColorSystem : MonoBehaviour
{
	#region STATIC
	static Color _currentColor = Color.red;
	static List<Color> colors = new List<Color>() {Color.red, Color.blue, Color.green };

	public static Color currentColor
	{
		get => _currentColor; set
		{
			_currentColor = value;
			OnColorChanged?.Invoke(value);
		}
	}

	static event Action<Color> OnColorChanged;

	public static void InputRecordSystem_OnColorChanged(bool positive)
	{
		int currentIndex = colors.IndexOf(currentColor);
		if (positive)
		{
			if (currentIndex == colors.Count - 1)
			{
				currentColor = colors[0];
			}
			else currentColor = colors[++currentIndex];
		}
		else
		{
			if (currentIndex == 0)
			{
				currentColor = colors.Last();
			}
			else currentColor = colors[--currentIndex];
		}
	} 
	#endregion

	[SerializeField] SpriteRenderer spriteRenderer;
	[SerializeField] bool changeColor;
	[SerializeField] bool disableCollider;
	[SerializeField] Color baseColor;
	[SerializeField] Collider2D col;

	private void Start()
	{
		OnColorChanged += ColorChanger_OnColorChanged;
		ColorChanger_OnColorChanged(currentColor);
	}

	private void ColorChanger_OnColorChanged(Color color)
	{
		if (changeColor) ChangeColor(color);
		if (disableCollider) CheckDisableCollider();
	}

	private void ChangeColor(Color color)
	{
		Color newColor = color;
		newColor.a = spriteRenderer.color.a;
		spriteRenderer.color = newColor;
	}

	private void CheckDisableCollider()
	{
		col.enabled = baseColor != currentColor;
	}
}
