using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Input", menuName = "Input/Input")]
public class InputSO : ScriptableObject
{
	public Vector2 Move { get; set; }
	public Vector2 MousePos { get; set; }
	public bool Dodge { get; set; }
	public bool Attack1 { get; set; }
	public bool Attack2 { get; set; }

	public void Reset() {
		Move = Vector2.zero;
		MousePos = Vector2.zero;
		Dodge = false;
		Attack1 = false;
		Attack2 = false;
	}
}
