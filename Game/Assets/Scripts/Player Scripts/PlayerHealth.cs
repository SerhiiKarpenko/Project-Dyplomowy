using UnityEngine;

[RequireComponent(typeof(PlayerLevel))]
public class PlayerHealth : HealthAbstract
{
	[SerializeField] private GameObject _playerUI;
	[SerializeField] private float _currentHealth;
	[SerializeField] private float _maxHealth;
	private PlayerLevel _currentLevel;
	public delegate void ChangeHealthBarValue(float currentHealth);
	public static event ChangeHealthBarValue onChangeHp;


	public float MaxHealth => _maxHealth;

	private void Start()
	{
		_currentLevel = gameObject.GetComponent<PlayerLevel>();
		_currentHealth = _maxHealth;
	}

	public override void ApplyDamage(float amount)
	{
		if(amount < 0)
		{
			throw new System.ArgumentOutOfRangeException("Cannot apply negative Damage");
		}

		if (_currentHealth <= 0)
		{
			Death();
		}
		_currentHealth -= amount;
		onChangeHp?.Invoke(_currentHealth);
	}

	public override void Death()
	{
		gameObject.SetActive(false);
		_playerUI.SetActive(false);
		PlayerPrefs.SetInt("Player_level", _currentLevel.CurrentLevel);
		Events.OnPlayerDeath();
	}


	// ������� ? 
	public void Heal()
	{
		// ���� ����� ����� �� ������� �� ���������� 2 ������, �������� �������� ����������� 
	}


}
