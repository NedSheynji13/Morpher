using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftThenDestroyAfter2Seconds : MonoBehaviour, IMove
{

    public void Move()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        transform.Translate(transform.rotation * Vector3.back * 0.1f);
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2);
        DestroyImmediate(gameObject);
    }
}
