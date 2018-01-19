using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStarForm : MonoBehaviour
{
    private Rigidbody physix;       //Used for saving the rigidbody component of this game object
    private Vector3 dir, torque, equalizer;    //Used for rewriting the game objects velocities
    private Vector3 lastForce = new Vector3(0, 0, 0);
    private int colCount = 0;
    private bool floating = true;

    public void Start()
    {
        physix = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        if (Vector3.Angle(equalizer, Camera.main.transform.forward) >= 90)
        {
            dir = Input.GetAxisRaw("Horizontal") * -Camera.main.transform.up + Input.GetAxisRaw("Vertical") * Camera.main.transform.right;
            torque = Vector3.ProjectOnPlane(dir, equalizer).normalized * 100;
            physix.AddTorque(torque);
        }
        else
        {
            dir = Input.GetAxisRaw("Horizontal") * Camera.main.transform.up + Input.GetAxisRaw("Vertical") * Camera.main.transform.right;
            torque = Vector3.ProjectOnPlane(dir, equalizer).normalized * 100;
            physix.AddTorque(torque);
        }

        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.X))
            physix.useGravity = true;

        Morphing.grounded = !floating;
        if (!floating)
            physix.useGravity = false;

        SurroundingGravity();
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.contacts[0].point != null)
        {
            equalizer = other.contacts[0].normal;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        colCount++;
        floating = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        colCount--;
        if (colCount == 0)
        {
            floating = true;
            lastForce = physix.velocity;
        }
    }

    private void SurroundingGravity()
    {
        physix.AddForce(-equalizer * 10);

        if (floating)
            physix.AddForce(-10 * (equalizer + lastForce));
    }
}
