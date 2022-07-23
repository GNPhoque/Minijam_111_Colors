using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	[SerializeField] GameObject ghostPrefab;

	[SerializeField] InputReplaySystem player;
	[SerializeField] InputReplaySystem[] ghosts;

	bool started;
	EndLevelDoor endLevelDoor;

	public int coins;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		InputRecordSystem.OnAnyInputRecorded += InputRecordSystem_OnAnyInputRecorded;
		player.StartPlayback();
		coins = FindObjectsOfType<Coin>().Length;
		endLevelDoor = FindObjectOfType<EndLevelDoor>();
	}

	private void InputRecordSystem_OnAnyInputRecorded(InputRecord obj)
	{
		if (started) return;
		started = true;
		StartCoroutine(StartGhosts());
	}

	IEnumerator StartGhosts()
	{
		foreach (var ghost in ghosts)
		{
			yield return new WaitForSeconds(.5f);
			ghost.StartPlayback();
		}
	}

	public void CollectCoin()
	{
		coins--;
		if (coins == 0)
		{
			endLevelDoor.Use();
		}
	}
}
