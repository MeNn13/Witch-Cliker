using System;
using UnityEngine;

public class AutoClick : MonoBehaviour
{
    public static Action OnAutoClick;

    private float _startTime = 1.0f;
    private float _currentTime = 0.0f;

    private void Update()
    {
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

}
