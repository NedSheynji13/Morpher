using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakScript : MonoBehaviour
{
    #region Variables
    private bool crashed = false;
    private PhysicMaterial bounce;
    private GameObject[] pieces;
    private Vector3 scaling = Vector3.one;
	#endregion
	
	void Start ()
	{
        bounce = new PhysicMaterial();
        bounce.bounciness = 0.5f;
        pieces = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            pieces[i] = transform.GetChild(i).gameObject;
            pieces[i].layer = 10;
        }
	}

    private void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            pieces[i].transform.localScale = Vector3.Lerp(pieces[i].transform.localScale, scaling, 5 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!crashed)
        {
            if (other.GetComponent<Rigidbody>() != null && other.GetComponent<Rigidbody>().mass > 5)
            {
                Destroy(transform.GetComponent<BoxCollider>());
                Destroy(transform.GetComponent<BoxCollider>());
                for (int i = 0; i < transform.childCount; i++)
                {
                    pieces[i].AddComponent<BoxCollider>();
                    pieces[i].AddComponent<Rigidbody>();
                    pieces[i].GetComponent<BoxCollider>().material = bounce;
                    pieces[i].GetComponent<Rigidbody>().velocity = (new Vector3(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f) * 100);
                    StartCoroutine(ScaleDown());
                }
                crashed = true;
            }
        }
    }

    private IEnumerator ScaleDown()
    {
        yield return new WaitForSeconds(3);
        scaling = Vector3.zero;
        yield return new WaitForSeconds(6);
        DestroyImmediate(gameObject);
    }
}
