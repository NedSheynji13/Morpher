using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    #region Variables
    private int loadedSceneIndex;
    #endregion

    void Start()
    {
        loadedSceneIndex = SaveAndLoad.LoadScene();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(loadedSceneIndex);
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
