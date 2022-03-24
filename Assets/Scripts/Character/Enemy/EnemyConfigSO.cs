using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Config/EnemyCOnfig")]
public class EnemyConfigSO : ScriptableObject
{
	[Header("Basic")]
	[SerializeField, Range(1, 5)] public int initHealth;
	[SerializeField, Range(0, 50)] public float maxSpeed;

	[Header("Attack")]
	[SerializeField, Range(1, 3)] public int attackDamage;
	[SerializeField, Range(0.1f, 2)] public float attackInterval;
}
