using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Energy", menuName = "Config/Energy/Energy")]
public class EnergySO : ScriptableObject
{
	[SerializeField, HideInInspector] private int _energy;
	[SerializeField, HideInInspector] private int _maxEnergy;

	public int Energy => _energy;
	public int MaxEnergy => _maxEnergy;

	public void SetMaxEnergy(int i) {
		if (i <= 0) return;
		_maxEnergy = i;
	}
	public void SetEnergy(int i) {
		if (i < 0 || i > _maxEnergy) return;
		_energy = _maxEnergy;
	}
	public void LoseEnergy(int i) {
		if (i <= 0) return;
		_energy -= i;
		if (_energy < 0) _energy = 0;
	}
	public void GetEnergy(int i) {
		if (i <= 0) return;
		_energy += i;
		if (_energy > _maxEnergy) _energy = _maxEnergy;
	}
}
