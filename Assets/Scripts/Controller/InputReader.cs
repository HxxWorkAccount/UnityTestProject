using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
	[SerializeField] private InputSO input;
	[SerializeField] private InputConfigSO inputConfig;

	void Start() {
		if (input == null) Debug.LogError("No Input Data File");
		if (inputConfig == null) Debug.LogWarning("No InputConfig File");
	}

	void Update() {
		if (input == null || inputConfig == null) return;

		input.Reset();
		input.Attack1 = Input.GetKeyDown(inputConfig.attack1Key);
		input.Attack2 = Input.GetKeyDown(inputConfig.attack2Key);
		input.Dodge = Input.GetKeyDown(inputConfig.dodgeKey);
		input.MousePos = Input.mousePosition;

		Vector2 move = Vector2.zero;
		move.x += Input.GetKey(inputConfig.moveRightKey) ? 1 : 0;
		move.x += Input.GetKey(inputConfig.moveLeftKey) ? -1 : 0;
		move.y += Input.GetKey(inputConfig.moveUpKey) ? 1 : 0;
		move.y += Input.GetKey(inputConfig.moveDownKey) ? -1 : 0;
		input.Move = move.normalized;
	}
}
