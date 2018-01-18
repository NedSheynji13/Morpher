using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchPress : MonoBehaviour
{
    public bool pressed;
    public Material defaultColour;
    public Material pressedColour;
    private Vector3 pressedPosition;

    private void Start()
    {
        pressedPosition = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
            pressed = true;
    }
    private void Update()
    {
        if (pressed)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, pressedPosition, 10 * Time.deltaTime);
            GetComponent<Renderer>().material = pressedColour;
        }
    }
}
