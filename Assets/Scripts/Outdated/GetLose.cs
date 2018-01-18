using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLose : MonoBehaviour {

    public Component[] Rigs;
    
    void Start ()
    {
        Rigs = GetComponentsInChildren<Rigidbody>();
        
    }

    void Abwarten ()
    {
        foreach (Rigidbody rig in Rigs)
        {
            if (rig.isKinematic)
            {
                rig.AddForce(new Vector3(10, 10, 10));
                rig.isKinematic = false;

            }
        }
    }
    void JetzAberRausHier ()
    {
        foreach (Rigidbody rig in Rigs)
        {
            if (!rig.isKinematic)
            {
                rig.gameObject.SetActive(false);
               
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Basic":
                break;
            case "Player":
                foreach (Rigidbody rig in Rigs)
                {
                    if (rig.isKinematic)
                    {
                        Invoke("Abwarten", 0.25f);

                        Invoke("JetzAberRausHier", 2);
                     
                    }
                }

                break;
        }
    }
}
