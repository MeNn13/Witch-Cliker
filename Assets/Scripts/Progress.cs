using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

[System.Serializable]
public class GameInfo
{
    public ulong Score;
    public ulong SummClick;
    public ulong PriceClick;
    public ulong SummAutoClick;
    public ulong PriceAutoClick;
}

public class Progress : MonoBehaviour
{
    public GameInfo GameInfo;

    [SerializeField] private TextMeshProUGUI _textMeshPro;

    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);

    [DllImport("__Internal")]
    private static extern void LoadExtern();

    public static Progress Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
            LoadExtern();
        }
        else
            Destroy(gameObject);
    }

    public void Save()
    {
        string jsonString = JsonUtility.ToJson(GameInfo);
        SaveExtern(jsonString);
    }

    public void SetDataInfo(string value)
    {
        GameInfo = JsonUtility.FromJson<GameInfo>(value);
        _textMeshPro.text = GameInfo.Score + "\n" + GameInfo.PriceClick + "\n" + GameInfo.SummClick + "\n" + GameInfo.PriceAutoClick + "\n" + GameInfo.SummAutoClick + "\n";
    }
}
