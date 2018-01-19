using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    #region Variables
    public bool isHeavy;
    public Affector affector;


    private bool pressed;
    private Vector3 maxHeight, minHeight;
    #endregion

    private void Start()
    {
        maxHeight = transform.position;
        minHeight = maxHeight + (Vector3.down * 0.1f);
        pressed = false;
    }
    private void Update()
    {
        if (pressed)
        {
            transform.position = Vector3.Lerp(transform.position, minHeight, 5 * Time.deltaTime);
            affector.affected = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isHeavy)
        {
            if (other.GetComponent<Rigidbody>() != null && other.GetComponent<Rigidbody>().mass > 5)
                pressed = true;
        }
        else
        {
            if (other.GetComponent<Rigidbody>() != null)
                pressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
            pressed = false;
    }
}
