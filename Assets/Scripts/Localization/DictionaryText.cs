using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DictionaryText : MonoBehaviour
{
    Dictionary<byte, string> dictionary = new();

    [SerializeField] private string[] _translate;
    [SerializeField] private TMP_FontAsset _fontAssetDefault;
    [SerializeField] private TMP_FontAsset _fontAssetChina;
    [SerializeField] private TMP_FontAsset _fontAssetJapan;

    private TextMeshProUGUI _textMP;

    private void OnEnable()
    {
        ChangeLanguage.OnChangeLanguage += Change;
    }

    private void OnDisable()
    {
        ChangeLanguage.OnChangeLanguage -= Change;
    }

    private void Awake()
    {
        _textMP = GetComponent<TextMeshProUGUI>();

        for (byte i = 0; i < _translate.Length; i++)
        {
            dictionary[i] = _translate[i];
        }

        Change((Language)PlayerPrefs.GetInt("Language"));
    }

    private void Change(Language language)
    {
        if (language == Language.Ru || language == Language.En)
        {
            _textMP.font = _fontAssetDefault;
            _textMP.text = dictionary[(byte)language];
            _textMP.enableWordWrapping = true;
        }
        else if (language == Language.Cn)
        {
            _textMP.font = _fontAssetChina;          
            _textMP.text = dictionary[(byte)language];
            _textMP.enableWordWrapping = false;
        }
        else
        {
            _textMP.font = _fontAssetJapan;
            _textMP.text = dictionary[(byte)language];
            _textMP.enableWordWrapping = false;
        }
    }
}

public enum Language
{
    Ru = 0,
    En = 1,
    Cn = 2,
    Jp = 3
}