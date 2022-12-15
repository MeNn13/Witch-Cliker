using UnityEngine;

public class MuteMusic : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audioSource;
    [SerializeField] private GameObject _imgMute;
    [SerializeField] private GameObject _imgUnmute;
    public void Mute()
    {
        for (int i = 0; i < _audioSource.Length; i++)
            if (_audioSource[i].mute)
            {
                _imgMute.SetActive(false);
                _imgUnmute.SetActive(true);
                _audioSource[i].mute = false;
            }
            else
            {
                _imgMute.SetActive(true);
                _imgUnmute.SetActive(false);
                _audioSource[i].mute = true;
            }
    }
}
