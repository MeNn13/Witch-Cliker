using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
public class GameInfo
{
    public ulong Score;
    public ulong SummClick;
    public ulong PriceClick;
    public ulong SummAutoClick;
    public ulong PriceAutoClick;
    public byte Level;
    public List<string> ProductName;
}

public class Progress : MonoBehaviour
{
    public GameInfo GameInfo;

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
    }
}
