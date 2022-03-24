using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthConfig", menuName = "Config/Health/HealthConfig")]
public class HealthConfigSO : ScriptableObject
{
	public int initHealth = 5;
	public int maxHealth = 5;
}
