using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject OptionsMenu;

    private void Start()
    {
        OptionsMenu.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        OptionsMenu.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    public void PauseGame()
    {
        OptionsMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif

    }
}
