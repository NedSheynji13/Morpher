using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleChanger : MonoBehaviour
{
    public ParticleSystem[] portalEffects = new ParticleSystem[3];

    public void Start()
    {
        Instantiate(portalEffects[0], transform);
    }
}
