using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static Action OnDying;

    [SerializeField] private Image _healthBar;
    
    [SerializeField]
    private int _maxHealth = 200; 

    private int _currentHealth;

    private void OnEnable()
    {
        Click.OnClickEnter += CheckHealthClick;
        AutoClick.OnAutoClick += CheckHealthAutoClick;
    }

    private void OnDisable()
    {
        Click.OnClickEnter -= CheckHealthClick;
        AutoClick.OnAutoClick -= CheckHealthAutoClick;
    }

    private void Start()
    {     
        _currentHealth = _maxHealth;
    }

    private void CheckHealthClick()
    {
        if (_currentHealth <= 0)
        {
            OnDying?.Invoke();

            _currentHealth = _maxHealth;
            _healthBar.fillAmount = (float)_currentHealth / _maxHealth;

#if UNITY_WEBGL
            Progress.Instance.Save();
#endif
        }
        else
        {
            _currentHealth -= Convert.ToInt32(Progress.Instance.GameInfo.SummClick);
            _healthBar.fillAmount = (float)_currentHealth / _maxHealth;
        }
    }
    private void CheckHealthAutoClick()
    {
        if (_currentHealth <= 0)
        {
            OnDying?.Invoke();

            _currentHealth = _maxHealth;
            _healthBar.fillAmount = (float)_currentHealth / _maxHealth;

#if UNITY_WEBGL
            Progress.Instance.Save();
#endif
        }
        else
        {
            _currentHealth -= Convert.ToInt32(Progress.Instance.GameInfo.SummAutoClick);

            _healthBar.fillAmount = (float)_currentHealth / _maxHealth;
        }
    }
}
