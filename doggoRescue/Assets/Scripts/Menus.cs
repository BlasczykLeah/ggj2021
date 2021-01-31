using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
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
}
