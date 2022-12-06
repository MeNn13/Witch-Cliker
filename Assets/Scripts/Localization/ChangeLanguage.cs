using UnityEngine;
using System;

public class ChangeLanguage : MonoBehaviour
{
    public static Action<Language> OnChangeLanguage;

    [SerializeField] private Language _language;

    public void Change()
    {
        PlayerPrefs.SetInt("Language", (byte)_language);
        OnChangeLanguage?.Invoke(_language);
    }
}
