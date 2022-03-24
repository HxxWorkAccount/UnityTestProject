using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health", menuName = "Config/Health/Health")]
public class HealthSO : ScriptableObject
{
	[SerializeField, HideInInspector] private int _health;
	[SerializeField, HideInInspector] private int _maxHealth;

	public int Health => _health;
	public int MaxHealth => _maxHealth;

	public void SetMaxHealth(int i) {
		if (i <= 0) return;
		_maxHealth = i;
	}
	public void SetHealth(int i) {
		if (i < 0 || i > _maxHealth) return;
		_health = _maxHealth;
	}
	public void Hurt(int i) {
		if (i <= 0) return;
		_health -= i;
		if (_health < 0) _health = 0;
	}
	public void Heal(int i) {
		if (i <= 0) return;
		_health += i;
		if (_health > _maxHealth) _health = _maxHealth;
	}
}
