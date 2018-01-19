using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consolecreator : MonoBehaviour
{
    #region Variables
    public GameObject Light, Heavy, Red, Green;
    public int first, second, third;
    private Vector3 xOffset = Vector3.right * 3;
    private Vector3 yOffset = Vector3.up * 3;
    private Vector3 zOffset = Vector3.forward * 3;
    private GameObject[] hint, console;
    #endregion

    public void BuildLight()
    {
        Instantiate(Light, transform.position - xOffset - zOffset, Quaternion.identity, transform.GetChild(0));
        Instantiate(Light, transform.position - xOffset, Quaternion.identity, transform.GetChild(0));
        Instantiate(Light, transform.position - xOffset + zOffset, Quaternion.identity, transform.GetChild(0));
        Instantiate(Light, transform.position - zOffset, Quaternion.identity, transform.GetChild(0));
        Instantiate(Light, transform.position, Quaternion.identity, transform.GetChild(0));
        Instantiate(Light, transform.position + zOffset, Quaternion.identity, transform.GetChild(0));
        Instantiate(Light, transform.position + xOffset - zOffset, Quaternion.identity, transform.GetChild(0));
        Instantiate(Light, transform.position + xOffset, Quaternion.identity, transform.GetChild(0));
        Instantiate(Light, transform.position + xOffset + zOffset, Quaternion.identity, transform.GetChild(0));

        Instantiate(Red, transform.position + yOffset - zOffset + Vector3.down + Vector3.back, Quaternion.identity, transform.GetChild(1));
        Instantiate(Red, transform.position + yOffset + Vector3.down + Vector3.back, Quaternion.identity, transform.GetChild(1));
        Instantiate(Red, transform.position + yOffset + zOffset + Vector3.down + Vector3.back, Quaternion.identity, transform.GetChild(1));
        Instantiate(Red, transform.position - zOffset + Vector3.down + Vector3.back, Quaternion.identity, transform.GetChild(1));
        Instantiate(Red, transform.position + Vector3.down + Vector3.back, Quaternion.identity, transform.GetChild(1));
        Instantiate(Red, transform.position + zOffset + Vector3.down + Vector3.back, Quaternion.identity, transform.GetChild(1));
        Instantiate(Red, transform.position - yOffset - zOffset + Vector3.down + Vector3.back, Quaternion.identity, transform.GetChild(1));
        Instantiate(Red, transform.position - yOffset + Vector3.down + Vector3.back, Quaternion.identity, transform.GetChild(1));
        Instantiate(Red, transform.position - yOffset + zOffset + Vector3.down + Vector3.back, Quaternion.identity, transform.GetChild(1));

        randomize();
        Vector3 tempposition = transform.GetChild(1).transform.GetChild(first).transform.position;
        GameObject.DestroyImmediate(transform.GetChild(1).transform.GetChild(first).gameObject);
        Instantiate(Green, tempposition, Quaternion.identity, transform.GetChild(1));
        tempposition = transform.GetChild(1).transform.GetChild(second).transform.position;
        GameObject.DestroyImmediate(transform.GetChild(1).transform.GetChild(second).gameObject);
        Instantiate(Green, tempposition, Quaternion.identity, transform.GetChild(1));
        tempposition = transform.GetChild(1).transform.GetChild(third).transform.position;
        GameObject.DestroyImmediate(transform.GetChild(1).transform.GetChild(third).gameObject);
        Instantiate(Green, tempposition, Quaternion.identity, transform.GetChild(1));
    }

    public void BuildHeavy()
    {
        Instantiate(Heavy, transform.position - xOffset - zOffset, Quaternion.identity, transform.GetChild(0));
        Instantiate(Heavy, transform.position - xOffset, Quaternion.identity, transform.GetChild(0));
        Instantiate(Heavy, transform.position - xOffset + zOffset, Quaternion.identity, transform.GetChild(0));
        Instantiate(Heavy, transform.position - zOffset, Quaternion.identity, transform.GetChild(0));
        Instantiate(Heavy, transform.position, Quaternion.identity, transform.GetChild(0));
        Instantiate(Heavy, transform.position + zOffset, Quaternion.identity, transform.GetChild(0));
        Instantiate(Heavy, transform.position + xOffset - zOffset, Quaternion.identity, transform.GetChild(0));
        Instantiate(Heavy, transform.position + xOffset, Quaternion.identity, transform.GetChild(0));
        Instantiate(Heavy, transform.position + xOffset + zOffset, Quaternion.identity, transform.GetChild(0));

        Instantiate(Red, transform.position + yOffset - zOffset + Vector3.down + Vector3.back, Quaternion.identity, transform.GetChild(1));
        Instantiate(Red, transform.position + yOffset + Vector3.down + Vector3.back, Quaternion.identity, transform.GetChild(1));
        Instantiate(Red, transform.position + yOffset + zOffset + Vector3.down + Vector3.back, Quaternion.identity, transform.GetChild(1));
        Instantiate(Red, transform.position - zOffset + Vector3.down + Vector3.back, Quaternion.identity, transform.GetChild(1));
        Instantiate(Red, transform.position + Vector3.down + Vector3.back, Quaternion.identity, transform.GetChild(1));
        Instantiate(Red, transform.position + zOffset + Vector3.down + Vector3.back, Quaternion.identity, transform.GetChild(1));
        Instantiate(Red, transform.position - yOffset - zOffset + Vector3.down + Vector3.back, Quaternion.identity, transform.GetChild(1));
        Instantiate(Red, transform.position - yOffset + Vector3.down + Vector3.back, Quaternion.identity, transform.GetChild(1));
        Instantiate(Red, transform.position - yOffset + zOffset + Vector3.down + Vector3.back, Quaternion.identity, transform.GetChild(1));

        randomize();
        Vector3 tempposition = transform.GetChild(1).transform.GetChild(first).transform.position;
        GameObject.DestroyImmediate(transform.GetChild(1).transform.GetChild(first).gameObject);
        Instantiate(Green, tempposition, Quaternion.identity, transform.GetChild(1));
        tempposition = transform.GetChild(1).transform.GetChild(second).transform.position;
        GameObject.DestroyImmediate(transform.GetChild(1).transform.GetChild(second).gameObject);
        Instantiate(Green, tempposition, Quaternion.identity, transform.GetChild(1));
        tempposition = transform.GetChild(1).transform.GetChild(third).transform.position;
        GameObject.DestroyImmediate(transform.GetChild(1).transform.GetChild(third).gameObject);
        Instantiate(Green, tempposition, Quaternion.identity, transform.GetChild(1));
    }

    public void Delete()
    {
        for (int i = transform.GetChild(0).transform.childCount - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(transform.GetChild(0).transform.GetChild(i).gameObject);
            GameObject.DestroyImmediate(transform.GetChild(1).transform.GetChild(i).gameObject);
        }
    }

    private void randomize()
    {
        first = (int)Mathf.Round(Random.value * 8);
        {
            second = (int)Mathf.Round(Random.value * 8);
        }
        while (second == first) ;
        {
            third = (int)Mathf.Round(Random.value * 8);
        }
        while (third == second || third == first) ;
    }

    private void Start()
    {
        if (transform.childCount == 0)
            DestroyImmediate(gameObject);
        else
        {
            
        }
    }
}
