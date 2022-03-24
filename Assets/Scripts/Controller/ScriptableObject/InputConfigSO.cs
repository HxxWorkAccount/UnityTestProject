using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputConfig", menuName = "Input/InputConfig")]
public class InputConfigSO : ScriptableObject
{
	[Header("PC Input Config")]
	[SerializeField] public KeyCode moveUpKey = KeyCode.W;
	[SerializeField] public KeyCode moveDownKey = KeyCode.S;
	[SerializeField] public KeyCode moveLeftKey = KeyCode.A;
	[SerializeField] public KeyCode moveRightKey = KeyCode.D;
	[SerializeField] public KeyCode attack1Key = KeyCode.Mouse0;
	[SerializeField] public KeyCode attack2Key = KeyCode.Mouse2;
	[SerializeField] public KeyCode dodgeKey = KeyCode.Mouse1;
}
