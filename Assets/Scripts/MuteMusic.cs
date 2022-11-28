using UnityEngine;

public class MuteMusic : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _imgMute;
    [SerializeField] private GameObject _imgUnmute;
    public void Mute()
    {
        if (_audioSource.mute)
        {
            _imgMute.SetActive(false);
            _imgUnmute.SetActive(true);
            _audioSource.mute = false;
        }
        else
        {
            _imgMute.SetActive(true);
            _imgUnmute.SetActive(false);
            _audioSource.mute = true;
        }          
    }
}
