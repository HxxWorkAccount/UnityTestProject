using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnergyConfig", menuName = "Config/Energy/EnergyConfig")]
public class EnergyConfigSO : ScriptableObject
{
	public int initEnergy = 7;
	public int maxEnergy = 7;
}
