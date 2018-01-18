using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartFourScript : MonoBehaviour
{

    public GameObject SphereWorldPartFour;
    public float partsDone = 0;
    public bool done = false;


    public void PartDone()
    {
        if (partsDone < 2)
        {
            partsDone = partsDone + 1;
        }
        if (partsDone == 2)
        {
            done = true;
            SphereWorldPartFour.GetComponent<SphereWorldProgress>().done();
        }


    }
}
