using System;
using UnityEngine;

public class AutoClick : MonoBehaviour
{
    public static Action OnAutoClick;

    private bool _isGoing = true;
    private float _startTime = 1.0f;
    private float _currentTime = 0.0f;

    private void OnEnable()
    {
        EnemyHealth.OnDying += StopAutoClick;
    }

    private void OnDisable()
    {
        EnemyHealth.OnDying -= StopAutoClick;
    }

    private void Update()
    {
        if (_isGoing)
            if (_currentTime <= 0)
            {
                ClickPerSecond();
                _currentTime = _startTime;
            }
            else
                _currentTime -= Time.deltaTime;
    }

    private void ClickPerSecond()
    {
        OnAutoClick?.Invoke();
    }

    private void StopAutoClick()
    {
        _isGoing = false;
    }
}
