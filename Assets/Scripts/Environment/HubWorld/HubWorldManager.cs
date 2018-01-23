using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubWorldManager : MonoBehaviour
{

    #region Variables
    public ParticleSystem Locked, Accessable;
    public GameObject Changer;
    [HideInInspector] public int unlockedWorlds;

    private Vector3[] portalpositions;
    private Quaternion rot;
    #endregion

    private void Awake()
    {
        try
        {
            unlockedWorlds = SaveAndLoad.LoadScene();
        }
        catch
        {
            unlockedWorlds = 1;
        }
        rot = Quaternion.Euler(-90f, 0, 0);
        portalpositions = new Vector3[]
        {
            transform.Find("Portal1").transform.position,
            transform.Find("Portal2").transform.position,
            transform.Find("Portal3").transform.position,
            transform.Find("Portal4").transform.position,
            transform.Find("Portal5").transform.position
        };
    }

    void Start()
    {
        for (int i = 1; i <= portalpositions.Length; i++)
        {
            if (unlockedWorlds >= i)
            {
                GameObject temp = Instantiate(Changer, portalpositions[i - 1], Quaternion.identity);
                temp.GetComponent<Levelchanger>().SceneIndex = i;
                Instantiate(Accessable, portalpositions[i - 1], rot);
            }
            else
                Instantiate(Locked, portalpositions[i - 1], rot);
        }
    }
}
