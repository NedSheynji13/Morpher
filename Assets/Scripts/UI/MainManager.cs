using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene(SaveAndLoad.LoadScene());
    }

    public void NewGame()
    {
        SceneManager.LoadScene(7);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
