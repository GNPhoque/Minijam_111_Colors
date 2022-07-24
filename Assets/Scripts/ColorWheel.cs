using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ColorWheel : MonoBehaviour
{
	[SerializeField] Image image;
	[SerializeField] Sprite[] wheels;

	static int currentIndex;

	#region SINGLETON
	public static ColorWheel instance;
	private void Awake()
	{
		instance = this;
		image.sprite = wheels[currentIndex];
	}
	#endregion

	public void ChangeWheel(bool positive)
	{
		if (positive)
		{
			if (currentIndex == wheels.Length - 1)
			{
				currentIndex = 0;
			}
			else
			{
				currentIndex++;
			}
		}
		else
		{
			if (currentIndex == 0)
			{
				currentIndex = wheels.Length - 1;
			}
			else currentIndex--;
		}
		image.sprite = wheels[currentIndex];
	}
}
