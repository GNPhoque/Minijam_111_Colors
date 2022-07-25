using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Linq;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour
{
	[SerializeField] GameObject namePanel;
	[SerializeField] TMPro.TMP_InputField nameField;
	[SerializeField] TMPro.TextMeshProUGUI resultTime;

	[SerializeField] GameObject leaderboardRowPrefab;
	[SerializeField] Transform leaderboardRowParent;

	[SerializeField] GameObject bufferring;

	int playerTime;

	private void Start()
	{
		playerTime = TimerSystem.GetTimeScore();
		TimerSystem.ResetTimer();

		Login();
	}

	void Login()
	{
		var request = new LoginWithCustomIDRequest
		{
			CustomId = SystemInfo.deviceUniqueIdentifier,
			CreateAccount = true,
			InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
			{
				GetPlayerProfile = true
			}
		};
		PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
	}

	public void SendLeaderboard()
	{
		var request = new UpdatePlayerStatisticsRequest
		{
			Statistics = new List<StatisticUpdate>
			{
				new StatisticUpdate
				{
					StatisticName="GameScore",
					Value = -playerTime
				}
			}
		};
		PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
	}

	public void GetLeaderboard()
	{
		bufferring.SetActive(true);
		var request = new GetLeaderboardRequest
		{
			StatisticName = "GameScore",
			StartPosition = 0,
			MaxResultsCount = 10,
		};
		StartCoroutine(WaitBeforePull(request, OnLeaderboardGet, OnError));
	}

	private static IEnumerator WaitBeforePull(GetLeaderboardRequest request, Action<GetLeaderboardResult> OnLeaderboardGet, Action<PlayFabError> OnError)
	{
		yield return new WaitForSeconds(1f);
		PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
	}

	public void SubmitNameButton()
	{
		var request = new UpdateUserTitleDisplayNameRequest
		{
			DisplayName = nameField.text
		};
		PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
	}



	private void OnLoginSuccess(LoginResult obj)
	{
		Debug.Log("Successful login/acount creation!");

		//From main menu button
		if (playerTime == 0)
		{
			GetLeaderboard();
			return;
		}

		string name = null;
		//Get player name
		if (obj.InfoResultPayload.PlayerProfile != null)
			name = obj.InfoResultPayload.PlayerProfile.DisplayName;

		if (name == null)
		{
			namePanel.SetActive(true);
		}
		else
		{
			SendLeaderboard();
		}
	}

	private void OnError(PlayFabError error)
	{
		Debug.LogError("Database connection error");
		Debug.LogError(error.GenerateErrorReport());
	}

	private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult obj)
	{
		Debug.Log("Succesfull leaderboard update");
		GetLeaderboard();
	}

	private void OnLeaderboardGet(GetLeaderboardResult obj)
	{
		Debug.Log($"Leaderboard retrieved with {obj.Leaderboard.Count} rows");
		foreach (Transform item in leaderboardRowParent)
		{
			Destroy(item.gameObject);
		}
		foreach (var item in obj.Leaderboard)
		{
			Debug.Log($"{++item.Position} : {item.DisplayName} {item.StatValue}");
			GameObject newRow = Instantiate(leaderboardRowPrefab, leaderboardRowParent);
			TMPro.TextMeshProUGUI[] texts = newRow.GetComponentsInChildren<TMPro.TextMeshProUGUI>();
			texts[0].text = item.Position.ToString();
			texts[1].text = item.DisplayName;
			texts[2].text = (-item.StatValue).ToString("00:00:00");
		}
		bufferring.SetActive(false);
	}

	private void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult obj)
	{
		Debug.Log("Updated user display name");
		namePanel.SetActive(false);
		SendLeaderboard();
	}

	public void LoadTitleScene()
	{
		GameManager.LoadTitleScene();
	}
}
