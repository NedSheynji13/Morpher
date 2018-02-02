using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainManager : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene(SaveAndLoad.LoadScene());
    }

    public void NewGame()
    {
        File.Delete(Path.Combine(Application.persistentDataPath, "forms.txt"));
        SceneManager.LoadScene(7);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
