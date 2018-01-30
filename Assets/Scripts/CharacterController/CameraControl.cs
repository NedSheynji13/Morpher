using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float sensivity;
    private Vector3 inputAngle;     //Used for communication with unity's input manager
    private float cubeTurn, MinFoV, MaxFoV, FoV;
    private Ray[] wallRays;

    void Start()
    {
        inputAngle = new Vector3(0, 0, 0);
        FoV = Camera.main.transform.localPosition.z;
        MaxFoV = FoV;
        MinFoV = MaxFoV / 2.5f;
        wallRays = new Ray[8];
    }

    void Update()
    {
        FollowMorpher(Morphing.currentForm); //Follows the current active object and also changes depending on the form
        Zoom();
    }

    /// <summary>
    /// Lets the camera follow a given Object. Keep in mind that this requires a camera root
    /// </summary>
    /// <param name="_currentForm">Put the game object the camera should follow here</param>
    void FollowMorpher(GameObject _currentForm)
    {
        switch (_currentForm.name)  //Depending which form is the active one, the camera control might change its behaviour
        {
            case "BasicForm":
                FollowBasicForm();
                break;
            case "CubeForm":
                FollowCubeForm();
                break;
            case "SpringForm":
                FollowBasicForm();
                break;
            case "StarForm":
                FollowStarForm();
                break;
        }

        transform.position = _currentForm.transform.position;   //If the object moves the camera keeps following
        if (_currentForm.name == "CubeForm")
            inputAngle.y = Mathf.Round(inputAngle.y / 90) * 90;
    }

    /// <summary>
    /// The camera behaviour used for the basic form
    /// </summary>
    void FollowBasicForm()  //The Camera follow behaviour for the basic form
    {
        //Takes informations from the mouse input, read by the input manager and recalculates them to allow smoother camera turns
        inputAngle.y += Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
        inputAngle.x += Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;

        //Snaps the rotation of the basic form between 0 and 360 degrees
        if (inputAngle.y > 360)
            inputAngle.y -= 360;
        else if (inputAngle.y < 0)
            inputAngle.y += 360;

        inputAngle.x = Mathf.Clamp(inputAngle.x, -10, 40);   //Clamps how high/low and the user can turn the camera. Prevents the camera from looking through the ground.
        transform.localRotation = Quaternion.Euler(inputAngle);            //Sets the rotation of the camera according to the users preference calculated earlier
    }

    /// <summary>
    /// The camera behaviour used for the cube form
    /// </summary>
    void FollowCubeForm()
    {
        cubeTurn += Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
        inputAngle.x += Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;

        if (cubeTurn < -20)
        {
            cubeTurn = 0;
            inputAngle.y -= 90f;
        }
        else if (cubeTurn > 20)
        {
            cubeTurn = 0;
            inputAngle.y += 90f;
        }

        inputAngle.x = Mathf.Clamp(inputAngle.x, -10, 40);   //Clamps how high/low and the user can turn the camera. Prevents the camera from looking through the ground.

        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(inputAngle), 10 * Time.deltaTime);
        //Using the Quaternion rotation to avoid overturning over or under +-360 degrees life with eulerAngles
        //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, inputAngle, 10 * Time.deltaTime);
    }

    void FollowStarForm()
    {
        //Takes informations from the mouse input, read by the input manager and recalculates them to allow smoother camera turns
        inputAngle.y += Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
        inputAngle.x += Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;

        //Snaps the rotation of the basic form between 0 and 360 degrees
        if (inputAngle.y > 360)
            inputAngle.y -= 360;
        else if (inputAngle.y < 0)
            inputAngle.y += 360;

        transform.localRotation = Quaternion.Euler(inputAngle);                         //Sets the rotation of the camera according to the users preference calculated earlier
    }

    void Zoom()
    {
        RaycastHit[] wallHit = new RaycastHit[wallRays.Length];
        Vector3 UR = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane)).normalized;
        Vector3 UL = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane)).normalized;
        Vector3 DR = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane)).normalized;
        Vector3 DL = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).normalized;
        wallRays[0] = new Ray(transform.position, DL);
        wallRays[1] = new Ray(transform.position, DR);
        wallRays[2] = new Ray(transform.position, UR);
        wallRays[3] = new Ray(transform.position, UL);
        for (int i = 0; i < wallRays.Length; i++) Physics.Raycast(wallRays[i], out wallHit[i]);

        for (int i = 0; i < wallRays.Length; i++)
        {
            if (-Vector3.Distance(wallHit[i].point, transform.position) > MaxFoV)
            {
                FoV = -Vector3.Distance(wallHit[i].point, transform.position);
                break;
            }
            else if (-Vector3.Distance(wallHit[i].point, transform.position) > MinFoV)
            {
                FoV = MinFoV;
                break;
            }
            else
                FoV = MaxFoV;
        }
        Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, new Vector3(0, 1, FoV), Time.deltaTime * 5);
    }
}
