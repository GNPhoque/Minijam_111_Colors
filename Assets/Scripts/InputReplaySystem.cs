using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReplaySystem : MonoBehaviour
{
	public List<InputRecord> inputRecords;

	float currentTime;
	bool started;

	public event Action<InputRecord> OnNewInputToPlay;

	private void Start()
	{
		inputRecords = new List<InputRecord>();
		InputRecordSystem.OnAnyInputRecorded += InputRecordSystem_OnAnyInputRecorded;
	}

	private void Update()
	{
		if (!started) return;

		currentTime += Time.deltaTime;

		if (!inputRecords.Any()) return;

		while (inputRecords.First().time <= currentTime)
		{
			OnNewInputToPlay?.Invoke(inputRecords[0]);
			inputRecords.RemoveAt(0);
			if (!inputRecords.Any()) return;
		}
	}

	private void InputRecordSystem_OnAnyInputRecorded(InputRecord obj)
	{
		inputRecords.Add(obj);
	}

	public void StartPlayback()
	{
		started = true;
	}
}