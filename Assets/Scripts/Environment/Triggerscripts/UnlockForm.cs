using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockForm : MonoBehaviour
{
    #region Variables
    [Tooltip("0=None|1=Cube|2=Spring|4=Star|8=Bridge   Add numbers together to unlock multiple forms")]
    public int bitmask;

    public enum particleType { Cube, Spring, Star, Bridge};
    public particleType particleToRender;

    public bool DisplayParticles = true;
    public ParticleSystem[] Unlocktype;

    private bool isRunning = false;
    #endregion

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            Morphing.unlocked |= (Morphing.abilities)bitmask;
            SaveAndLoad.Save();
        }
    }

    private void Update()
    {
        if (DisplayParticles)
        {
            switch (particleToRender)
            {
                case particleType.Cube:
                    {
                        if(!isRunning) StartCoroutine(SpawnParticles(0));
                        break;
                    }
                case particleType.Spring:
                    {
                        if (!isRunning) StartCoroutine(SpawnParticles(1));
                        break;
                    }
                case particleType.Star:
                    {
                        if (!isRunning) StartCoroutine(SpawnParticles(2));
                        break;
                    }
                case particleType.Bridge:
                    {
                        if (!isRunning) StartCoroutine(SpawnParticles(3));
                        break;
                    }
            }

        }
    }

    private IEnumerator SpawnParticles(int _type)
    {
        isRunning = true;
        Instantiate(Unlocktype[_type], gameObject.transform);
        yield return new WaitForSeconds (0.8f);
        isRunning = false;
    }
}
