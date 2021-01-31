using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{
    public static Menus inst;

    public GameObject pauseMenu, howToPlayPage;
    public bool paused;
    public Image sign;

    public void Start()
    {
       inst = this;
       pauseMenu.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused == true)
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
        ClickerHandler.inst.canMove = true;
        paused = false;
    }

    public void pause()
    {
        pauseMenu.SetActive(true);
        ClickerHandler.inst.canMove = false;
        paused = true;
    }

    public void returnToTown()
    {
        ClickerHandler.inst.ReturnToTown();
    }

    public void OpenHowToPlay()
    {
        howToPlayPage.gameObject.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        howToPlayPage.gameObject.SetActive(false);
    }
}
