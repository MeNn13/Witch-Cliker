using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
public class GameInfo
{
    private ulong score = 0;
    public ulong Score
    {
        get
        {
            return this.score;
        }
        set
        {
            if (value < 0)
                score = 0;
            else
                score = value;
        }
    }

    private byte summClick = 1;
    public byte SummClick
    {
        get
        {
            return this.summClick;
        }
        set
        {
            if (value < 1)
                summClick = 1;
            else
                summClick = value;
        }
    }

    private int priceClick = 40;
    public int PriceClick
    {
        get
        {
            return this.priceClick;
        }
        set
        {
            if (value < 40)
                priceClick = 40;
            else
                priceClick = value;
        }
    }

    private byte summAutoClick = 1;
    public byte SummAutoClick
    {
        get
        {
            return this.summAutoClick;
        }
        set
        {
            if (value < 1)
                summAutoClick = 1;
            else
                summAutoClick = value;
        }
    }

    private int priceAutoClick = 200;
    public int PriceAutoClick
    {
        get
        {
            return this.priceAutoClick;
        }
        set
        {
            if (value < 200)
                priceAutoClick = 200;
            else
                priceAutoClick = value;
        }
    }

    private byte level = 1;
    public byte Level
    {
        get
        {
            return this.level;
        }
        set
        {
            if (value < 1 || value > 10)
                level = 1;
            else
                level = value;
        }
    }

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
            //Load();
            LoadExtern();
        }
        else
            Destroy(gameObject);
    }

    public void Save()
    {
        string jsonString1 = JsonConvert.SerializeObject(GameInfo);
        //PlayerPrefs.SetString("GameData", jsonString1);
        SaveExtern(jsonString1);
    }

    public void Load()
    {
        //GameInfo = JsonUtility.FromJson<GameInfo>(PlayerPrefs.GetString("GameData"));
        GameInfo = JsonConvert.DeserializeObject<GameInfo>(PlayerPrefs.GetString("GameData"));
    }

    public void SetDataInfo(string value)
    {
        GameInfo = JsonConvert.DeserializeObject<GameInfo>(value);
    }
}
