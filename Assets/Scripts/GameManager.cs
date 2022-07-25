using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField] GameObject ghostPrefab;
	[SerializeField] InputReplaySystem player;
	[SerializeField] TextMeshProUGUI timerText;

	static int totalDeaths;
	static bool isTimerStarted;

	#region SINGLETON
	public static GameManager instance;
	private void Awake()
	{
		instance = this;
	} 
	#endregion

	private void Start()
	{
		player.StartPlayback();
		if (TimerSystem.sw == null)
		{
			TimerSystem.StartTimer();
			isTimerStarted = true; 
		}
	}

	private void Update()
	{
		timerText.text = TimerSystem.GetTime();	
	}

	#region GHOST
	public void SummonGhostWithXOffset(float offset)
	{
		SummonGhost(player.transform.position + Vector3.right * offset);
	}
	public void SummonGhostWithYOffset(float offset)
	{
		SummonGhost(player.transform.position + Vector3.up * offset);
	}

	public void SummonGhostAtPosition(Vector3 spawnPos)
	{
		SummonGhost(spawnPos);
	}

	public void SummonGhost(Vector3? spawnPos = null)
	{
		InputReplaySystem ghost = SpawnGhost(spawnPos);
		InputRecord playerLastMovementInput = player.GetComponent<PlayerMovement>().GetLastMovementInput();
		ghost.FixDelay(playerLastMovementInput.time);
		ghost.AddInputRecord(playerLastMovementInput);
		StartCoroutine(StartGhost(ghost));
	}

	InputReplaySystem SpawnGhost(Vector3? spawnPos=null)
	{
		if(!spawnPos.HasValue) spawnPos = player.transform.position;
		InputReplaySystem ghost = Instantiate(ghostPrefab, spawnPos.Value, Quaternion.identity).GetComponent<InputReplaySystem>();
		return ghost;
	}

	IEnumerator StartGhost(InputReplaySystem ghost)
	{
		yield return new WaitForSeconds(.3f);
		ghost.gameObject.SetActive(true);
		yield return new WaitForSeconds(.2f);
		ghost.StartPlayback();
	}
	#endregion

	#region SCENE
	public void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		ExitSign.deaths++;
		totalDeaths++;
	}

	public static void LoadNextScene()
	{
		if(SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings - 1)
		{
			TimerSystem.StopTimer();
		}
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		ExitSign.deaths = 0;
	} 

	public static void LoadTitleScene()
	{
		SceneManager.LoadScene(1);
	}

	public static void LoadLeaderboardsScene()
	{
		SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
	}
	#endregion
}
