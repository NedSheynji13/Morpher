using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingJumpAndRun : MonoBehaviour {

    public GameObject SphereWorldPartTwo;
    public GameObject SphereWorldPartThree;
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
            SphereWorldPartTwo.GetComponent<SphereWorldProgress>().done();
            SphereWorldPartThree.GetComponent<SphereWorldProgress>().done();
        }

     
    }

    void Update ()
    {
       

    }
}
