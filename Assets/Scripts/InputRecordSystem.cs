using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRecordSystem : MonoBehaviour
{
	public static event Action<InputRecord> OnAnyInputRecorded;

	PlayerInput inputs;
	float currentTime;
	bool started;

	private void Start()
	{
		OnAnyInputRecorded += InputRecordSystem_OnAnyInputRecorded;

		inputs = new PlayerInput();

		inputs.Player.Move.performed += Move_performed;
		inputs.Player.Move.canceled += Move_performed;
		inputs.Player.Jump.performed += Jump_performed;
		inputs.Player.Jump.canceled += Jump_canceled;
		inputs.Player.ColorLeft.performed += ColorLeft_performed;
		inputs.Player.ColorRight.performed += ColorRight_performed;

		inputs.Player.Enable();
	}

	private void InputRecordSystem_OnAnyInputRecorded(InputRecord obj)
	{
		started = true;
		OnAnyInputRecorded -= InputRecordSystem_OnAnyInputRecorded;
	}

	private void Update()
	{
		if (!started) return;
		currentTime += Time.deltaTime;
	}

	private void OnDestroy()
	{
		inputs.Player.Disable();
	}

	private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		InputRecord inputRecord = new InputRecord { recordType = InputRecordType.Movement, movement = obj.ReadValue<Vector2>(), time = currentTime };
		OnAnyInputRecorded?.Invoke(inputRecord);
	}

	private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		InputRecord inputRecord = new InputRecord { recordType = InputRecordType.Jump, jump = true, time = currentTime };
		OnAnyInputRecorded?.Invoke(inputRecord);
	}

	private void Jump_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		InputRecord inputRecord = new InputRecord { recordType = InputRecordType.Jump, jump = false, time = currentTime };
		OnAnyInputRecorded?.Invoke(inputRecord);
	}

	private void ColorLeft_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		ColorSystem.InputRecordSystem_OnColorChanged(false);
	}

	private void ColorRight_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		ColorSystem.InputRecordSystem_OnColorChanged(true);
	}
}
