using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SphereWorld : MonoBehaviour {
    public float doneParts = 0;
    bool done = false;

	void Start ()
    {
		
	}
    void hauab()
    {
        SceneManager.LoadScene("HubWorld5000");

    }

    public void PartDone()
    {
        if (doneParts < 4)
        {
            doneParts += 1;
        }
        if (doneParts == 4)
        {
            done = true;
        }
    }
	void Update ()
    {
        if (done)
        {
            Invoke("hauab", 5);

        }
        
    }
}
