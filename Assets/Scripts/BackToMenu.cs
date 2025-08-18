using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public void BackToMain()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
