using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    #region Variables
    public bool isHeavy;
    public Affector affector;

    [HideInInspector]
    public bool pressed { get { return (pressed_actual && !pressed_previous); } }
    public bool pressedActual { get { return pressed_actual; } }

    private bool pressed_actual, pressed_previous, pressed_request, pressed_down;
    private Vector3 maxHeight, minHeight;
    #endregion

    private void Start()
    {
        maxHeight = transform.position;
        minHeight = maxHeight + (transform.rotation * Vector3.down * 0.1f);
        pressed_actual = pressed_previous = pressed_request = pressed_down = false;
    }
    private void Update()
    {
        SetPressed(pressed_request);
        if (pressed)
        {
            if (affector != null)
                affector.affected = true;
            else
                return;
        }
        
        transform.position = Vector3.Lerp(transform.position, (pressed_down ? minHeight : maxHeight), 5 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isHeavy)
        {
            if (other.GetComponent<Rigidbody>() != null && other.GetComponent<Rigidbody>().mass > 5)
                pressed_request = true;
        }
        else
        {
            if (other.GetComponent<Rigidbody>() != null)
                pressed_request = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
            pressed_request = false;
    }

    private void SetPressed(bool state_actual)
    {
        pressed_previous = pressed_actual;
        pressed_actual = state_actual;
        if (pressed) pressed_down = true;
    }

    public void Reset()
    {
        pressed_down = false;
    }
}