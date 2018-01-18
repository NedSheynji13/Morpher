using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlattformSpinningBall : MonoBehaviour
{
    private GameObject Player;

    private void Start()
    {
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.Rotate(0, 0, 1);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            Morphing.forced = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            Player.transform.parent = gameObject.transform;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            Player.transform.rotation = Quaternion.identity;
            Player.transform.localScale = Vector3.one;
            Player.transform.parent = null;
            Morphing.forced = false;
        }
    }
}
