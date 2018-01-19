using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlattform : MonoBehaviour
{
    #region Variables
    public Vector3 Distance;
    private Vector3 startPoint, currentposition;
    private float x, y, z;
    #endregion

    private void Start()
    {
        startPoint = transform.position;
        currentposition = startPoint;
    }


    void Update()
    {
        currentposition.x = startPoint.x + Distance.x * Mathf.Sin(Time.time);
        currentposition.y = startPoint.y + Distance.y * Mathf.Sin(Time.time);
        currentposition.z = startPoint.z + Distance.z * Mathf.Sin(Time.time);

        transform.position = currentposition;
    }
}
