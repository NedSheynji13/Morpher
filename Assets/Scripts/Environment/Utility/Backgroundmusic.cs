using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgroundmusic : MonoBehaviour
{
    #region Variables
    public AudioSource[] BGM;
    #endregion

    private void Start()
    {
        AudioSource player = Instantiate(BGM[0]);
        player.transform.parent = gameObject.transform;
    }
}
