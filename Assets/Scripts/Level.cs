using System;
using TMPro;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Action OnLevelUp;

    [SerializeField] private TextMeshProUGUI _levelText;

    private void Start()
    {
        _levelText.text = "Level " + Progress.Instance.GameInfo.Level;
    }

    public void LevelUp()
    {
        Progress.Instance.GameInfo.Level++;

        _levelText.text = "Level " + Progress.Instance.GameInfo.Level;

        OnLevelUp?.Invoke();

        Progress.Instance.Save();
    }
}
