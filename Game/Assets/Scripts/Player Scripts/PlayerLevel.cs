using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private SkillCanvasManager _skillCanvasManager;
    [SerializeField] private Slider _experienceSlider;
    [SerializeField] private float _amoutToNextLevel;
    private float _currentAmount = 0;
    private int _currentLevel = 1;

    public int CurrentLevel => _currentLevel;
    public float AmountToNextLevel => _amoutToNextLevel;
    public float CurrentAmountOfExperience => _currentAmount;

    private void Start()
    {
        Events.PlayerLevelUp += PlayerLevelUp;
        //Events.OnPlayerExperienceChanged += UpdateCurrentExperienceValue;
        _experienceSlider.maxValue = _amoutToNextLevel;
        _experienceSlider.value = _currentAmount;
    }


    public void PlayerLevelUp()
    {
        if (_currentAmount  >= _amoutToNextLevel)
        {
            _currentAmount = 0;
            _amoutToNextLevel = _amoutToNextLevel + (_amoutToNextLevel / 2);
            _experienceSlider.maxValue = _amoutToNextLevel;
            _currentLevel++;
            Events.PlayerLevelChange(_currentLevel);
            _skillCanvasManager.ShowSkills();
        }
    }

    public void UpdateCurrentExperienceValue(float amount)
    {
        _currentAmount += amount;
        Events.OnPlayerExperienceAmountChanged(_currentAmount);
    }

    private void OnDestroy()
    {
        Events.PlayerLevelUp -= PlayerLevelUp;
    }
}
