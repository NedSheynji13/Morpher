using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBasicForm : MonoBehaviour
{
    private Rigidbody physix;       //Used for saving the rigidbody component of this game object
    private Vector3 Physix2Move;    //Used for rewriting the game objects velocities
    private Quaternion lookRot;     //Used for rotating the player according to the mouse input
    private float speed = 10;            //Used to determine how fast the morpher is able to move
    private int colCount = 0;
    private float gravityStrength = 10;
    private float gravityStrengthMP = 1;
    private bool jumpBlock, CRunning;

    #region Variables used for the jumping animation
    private bool jumpAnimation;     //Used to initiate the jumping animation
    #endregion

    void Start()
    {
        //Upon start saves the rigidbody of this game object, the speed with which the object shall move
        physix = GetComponent<Rigidbody>();
        jumpBlock = CRunning = false;
    }

    void FixedUpdate()
    {
        //Using GetAxis results in some kind of acceleration. The object needs some time to get on speed
        //using GetAxisRaw allows instant movement into the given direction with infinite acceleration
        
        Physix2Move.x = Input.GetAxis("Horizontal") * speed;
        Physix2Move.z = Input.GetAxis("Vertical") * speed;
        Physix2Move.y = physix.velocity.y;

        lookRot = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0); //Reads the rotation arround the Y Axis of the main camera

        if (Input.GetButtonDown("Jump") && !jumpBlock) //Allows the user to jump unless he is aired
            Jump();
        //else if (!Morphing.grounded)  //Strengthens the gravity to make jumping more realistic
        //    GravityEqualizer();

        if (!Morphing.grounded)
        {
            physix.AddForce(Vector3.down * gravityStrength * gravityStrengthMP);
            physix.velocity = new Vector3(physix.velocity.x, physix.velocity.y, physix.velocity.z);
        }
        physix.velocity = lookRot * Physix2Move;

        //Asking if, so that the user cant move while morphing
        //Taking information from the user input WASD in order to give the object a velocity to the given direction
        //Matrix multiplied with the rotation of the camera to allow movement with the directions given by the camera rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, 10 * Time.deltaTime); //Turns the morphers face to whereever to camera faces currently with a slight delay

        if (Morphing.grounded)
        {
            Morphing.wantedscale.y = Mathf.Sin(Time.time * 5) * 0.02f + 1;
            Morphing.wantedscale.x = Mathf.Sin(Time.time * 5) * -0.02f + 1;
            Morphing.wantedscale.z = Mathf.Sin(Time.time * 5) * -0.02f + 1;
        }

        if (jumpAnimation)
        {
            Morphing.wantedscale.y = Mathf.Sin(Mathf.PI / 10 * physix.velocity.y / 5) + 1;
            Morphing.wantedscale.x = (-Mathf.Sin(Mathf.PI / 10 * physix.velocity.y / 50) + 1) / 1.5f;
            Morphing.wantedscale.z = (-Mathf.Sin(Mathf.PI / 10 * physix.velocity.y / 50) + 1) / 1.5f;
            if (physix.velocity.y <= 0)
            {
                jumpAnimation = false;
                Morphing.wantedscale = Vector3.one;
            }
        }
    }

    void Jump()
    {
        jumpBlock = jumpAnimation = true;
        Physix2Move.y = 10;
    }

    private void OnCollisionEnter(Collision col)
    {
        colCount++;
        for (int i = 0; i < col.contacts.Length; i++)
        {
            if (col.contacts[i].point.y - transform.position.y < 0.3f && col.contacts[i].point.y - transform.position.y > -0.3f)
            {
                gravityStrengthMP = 2;
                break;
            }
        }
    }

    private void OnTriggerExit()
    {
        colCount--;
        Morphing.grounded = false;
        if (colCount == 0)
            StartCoroutine(EdgeJump());
    }

    private void OnTriggerStay(Collider other)
    {
        Morphing.grounded = true;
        jumpBlock = false;
        gravityStrengthMP = 1;
    }

    private IEnumerator EdgeJump()
    {
        CRunning = true;
        yield return new WaitForSeconds(0.1f);
        if (!Morphing.grounded)
            jumpBlock = true;
        else
            StopCoroutine(EdgeJump());
        CRunning = false;
    }

    private void GravityEqualizer()
    {
        if (physix.velocity.y == 0)
            return;
        else if (CRunning)
            physix.AddForce(Vector3.down * ((gravityStrength * gravityStrengthMP) / physix.velocity.y));
    }
}
