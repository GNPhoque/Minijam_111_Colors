using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
	public void LoadNextScene()
	{
		GameManager.LoadNextScene();
	}

	public void LoadLeaderboardsScene()
	{
		GameManager.LoadLeaderboardsScene();
	}
}
