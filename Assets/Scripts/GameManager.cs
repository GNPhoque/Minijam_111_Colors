using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField] GameObject ghostPrefab;
	[SerializeField] InputReplaySystem player;

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
	}

	#region GHOST
	public void SummonGhost()
	{
		InputReplaySystem ghost = SpawnGhost();
		InputRecord playerLastMovementInput = player.GetComponent<PlayerMovement>().GetLastMovementInput();
		ghost.FixDelay(playerLastMovementInput.time);
		ghost.AddInputRecord(playerLastMovementInput);
		StartCoroutine(StartGhost(ghost));
	}

	InputReplaySystem SpawnGhost()
	{
		Vector3 spawnPos = player.transform.position;
		InputReplaySystem ghost = Instantiate(ghostPrefab, spawnPos, Quaternion.identity).GetComponent<InputReplaySystem>();
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
	}

	public void LoadNextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	} 
	#endregion
}
