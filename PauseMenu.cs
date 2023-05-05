using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool paused = false;
    public GameObject canvasObject;
    public GameObject self;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0.0f;
        canvasObject.SetActive(true);
    }

    public void Resume()
    {
        paused = false;
        Time.timeScale = 1.0f;
        canvasObject.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1.0f;
        paused = false;
        Destroy(self);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1.0f;
        paused = false;
        Destroy(self);
    }

    public static void Quit()
    {
        Application.Quit();
    }
}
