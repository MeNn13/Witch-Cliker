using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class ADS : MonoBehaviour
{
    public static Action OnAdsMultiplier;
    public static Action UnAdsMultiplier;

    [DllImport("__Internal")]
    private static extern void ShowAdsExtern();

    [SerializeField] private GameObject _ads;

    private float _currentPlayingTime = 30.0f;
    [SerializeField]
    private float _currentActiveTime;
    private bool _isPlaying = false;
    private bool _isActive = true;
    private System.Random random = new System.Random();

    private void Start()
    {       
        _currentActiveTime = random.Next(60, 120);
    }

    private void Update()
    {
        if (_isActive)
            if (_currentActiveTime <= 0)
            {
                _ads.SetActive(true);
                _isActive = false;
            }
            else
                _currentActiveTime -= Time.deltaTime;

        if (_isPlaying)
            if (_currentPlayingTime <= 0)
            {
                UnAdsMultiplier?.Invoke();
                _isPlaying = false;
                _currentActiveTime = random.Next(60, 120);
                _currentPlayingTime = 30.0f;
                _isActive = true;
            }
            else
                _currentPlayingTime -= Time.deltaTime;
    }

    public void ShowAds()
    {
        ShowAdsExtern();
    }

    public void MultiplierClicks()
    {
        OnAdsMultiplier?.Invoke();
        _ads.SetActive(false);
        _isPlaying = true;
    }
}
