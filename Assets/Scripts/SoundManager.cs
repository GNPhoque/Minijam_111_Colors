using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	[SerializeField] AudioSource audioSource;
	[SerializeField] AudioClip music;
	[SerializeField] AudioClip coin;
	[SerializeField] AudioClip trap;
	[SerializeField] AudioClip button;

	public static SoundManager instance;

	private void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
	}

	private void Start()
	{
		DontDestroyOnLoad(gameObject);
		GameManager.LoadNextScene();
	}

	public void PlayCoin()
	{
		audioSource.PlayOneShot(coin);
	}

	public void PlayTrap()
	{
		audioSource.PlayOneShot(trap);
	}

	public void PlayButton()
	{
		audioSource.PlayOneShot(button);
	}
}
