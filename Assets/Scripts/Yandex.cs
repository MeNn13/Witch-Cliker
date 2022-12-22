using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{
    public static Action OnAuth;
    public static Action OnRejection;

    [DllImport("__Internal")]
    private static extern void ShowFullAd();

    [DllImport("__Internal")]
    private static extern void AuthExtern();

    public void Start()
    {
        ShowFullAd();
    }

    public void Auth()
    {
        AuthExtern();    
    }

    public void AuthEnter()
    {
       // OnAuth?.Invoke();
    }

    public void Rejection()
    {
        OnRejection?.Invoke();
    }

}
