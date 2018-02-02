using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levelchanger : MonoBehaviour
{
    #region Scenechanging
    public int SceneIndex;
    public Texture2D sceneChangeScreen;
    public float fadeSpeed = 0.8f;

    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;
    #endregion

    private void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), sceneChangeScreen);
    }

    private float BeginFade(int direction)
    {
        fadeDir = direction;
        return (fadeSpeed);
    }

    private void OnLevelWasLoaded()
    {
        BeginFade(-1);
    }

    private void Change()
    {
        StartCoroutine(ChangeWithFading());
    }

    private IEnumerator ChangeWithFading()
    {
        float fadeTime = BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(SceneIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial")) SaveAndLoad.SaveScene();
        if (other.gameObject.GetComponent<Rigidbody>() != null) Invoke("Change", 0.5f);
    }
}
