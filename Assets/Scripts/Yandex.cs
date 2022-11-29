using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Yandex : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nickTMP;
    [SerializeField] private RawImage _avatar;

    [DllImport("__Internal")]
    private static extern void GetPlayerData();

    public void Start()
    {
        GetPlayerData();
    }

    public void SetName(string name) => _nickTMP.text = name;

    public void SetAvatar(string url)
    {
        StartCoroutine(DownloadImage(url));
    }

    IEnumerator DownloadImage(string mediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(mediaUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log(request.error);
        else
            _avatar.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }
}
