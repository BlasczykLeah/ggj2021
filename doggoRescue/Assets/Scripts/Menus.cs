using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool paused;

    public void Start()
    {
       pauseMenu.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            pause();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && paused == true)
        {
            resume();
        }
    }
    public void play()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }

    public void resume()
    {
        pauseMenu.SetActive(false);
        paused = false;
    }

    public void pause()
    {
        pauseMenu.SetActive(true);
        paused = true;
    }
}
