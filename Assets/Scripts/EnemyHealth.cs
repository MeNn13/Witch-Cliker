using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static Action OnDying;
    public static Action OnDyingBoss;

    [SerializeField] private Image _healthBar;
    [SerializeField] private TextMeshProUGUI _healthLabel;

    [SerializeField] private List<int> _healthList;

    private int _maxHealth;
    private int _currentHealth;
    private bool _isActiveBoss = false;

    private void OnEnable()
    {
        Click.OnClickEnter += CheckHealthClick;
        AutoClick.OnAutoClick += CheckHealthAuto;
        Level.OnLevelUp += UpdateHealth;
        ChangeOnBoss.OnChangeBoss += ChangeBoss;
    }

    private void OnDisable()
    {
        Click.OnClickEnter -= CheckHealthClick;
        AutoClick.OnAutoClick -= CheckHealthAuto;
        Level.OnLevelUp -= UpdateHealth;
        ChangeOnBoss.OnChangeBoss -= ChangeBoss;
    }
    private void Start()
    {
        _maxHealth = _healthList[Progress.Instance.GameInfo.Level - 1];
        _currentHealth = _maxHealth;
        _healthLabel.text = _currentHealth.ToString() + "/" + _maxHealth;
    }

    private void CheckHealthClick()
    {
        if (_currentHealth <= 0)
        {
            if (!_isActiveBoss)
            {
                OnDying?.Invoke();

                _currentHealth = _maxHealth;
                _healthBar.fillAmount = (float)_currentHealth / _maxHealth;

                _healthLabel.text = _currentHealth.ToString() + "/" + _maxHealth;

#if UNITY_WEBGL
                Progress.Instance.Save();
#endif
            }
            else
            {
                OnDyingBoss?.Invoke();

                _maxHealth = _healthList[Progress.Instance.GameInfo.Level - 1];
                _currentHealth = _maxHealth;
                _healthLabel.text = _currentHealth.ToString() + "/" + _maxHealth;

                _currentHealth = _maxHealth;
                _healthBar.fillAmount = (float)_currentHealth / _maxHealth;

                _healthLabel.text = _currentHealth.ToString() + "/" + _maxHealth;

                _isActiveBoss = false;
#if UNITY_WEBGL
                Progress.Instance.Save();
#endif
            }

        }
        else
        {
            _currentHealth -= Convert.ToInt32(Progress.Instance.GameInfo.SummClick * ((4 * (Progress.Instance.GameInfo.Level - 1)) + 1));
            _healthBar.fillAmount = (float)_currentHealth / _maxHealth;
            _healthLabel.text = _currentHealth.ToString() + "/" + _maxHealth;
        }
    }
    private void CheckHealthAuto()
    {
        if (_currentHealth <= 0)
        {
            if (!_isActiveBoss)
            {
                OnDying?.Invoke();

                _currentHealth = _maxHealth;
                _healthBar.fillAmount = (float)_currentHealth / _maxHealth;

                _healthLabel.text = _currentHealth.ToString() + "/" + _maxHealth;

#if UNITY_WEBGL
                Progress.Instance.Save();
#endif
            }
            else
            {
                OnDyingBoss?.Invoke();

                _maxHealth = _healthList[Progress.Instance.GameInfo.Level - 1];
                _currentHealth = _maxHealth;
                _healthLabel.text = _currentHealth.ToString() + "/" + _maxHealth;

                _currentHealth = _maxHealth;
                _healthBar.fillAmount = (float)_currentHealth / _maxHealth;

                _healthLabel.text = _currentHealth.ToString() + "/" + _maxHealth;

                _isActiveBoss = false;
#if UNITY_WEBGL
                Progress.Instance.Save();
#endif
            }

        }
        else
        {
            _currentHealth -= Convert.ToInt32(Progress.Instance.GameInfo.SummAutoClick * ((4 * (Progress.Instance.GameInfo.Level - 1)) + 1));
            _healthBar.fillAmount = (float)_currentHealth / _maxHealth;
            _healthLabel.text = _currentHealth.ToString() + "/" + _maxHealth;
        }
    }

    private void UpdateHealth()
    {
        _maxHealth = _healthList[Progress.Instance.GameInfo.Level - 1];
    }

    private void ChangeBoss()
    {
        _maxHealth = _healthList[Progress.Instance.GameInfo.Level - 1] * 2;
        _currentHealth = _maxHealth;
        _healthLabel.text = _currentHealth.ToString() + "/" + _maxHealth;

        _isActiveBoss = true;
    }
}
