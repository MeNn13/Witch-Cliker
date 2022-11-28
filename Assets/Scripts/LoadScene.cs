using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LevelLoad(int level)
    {
        SceneManager.LoadSceneAsync(level);
    }
}
