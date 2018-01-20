using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour, IMove
{
    #region Variables
    private float destroyAfter;
    #endregion

    public void Move(object o)
    {
        if (o != null && o.GetType() == typeof(float)) destroyAfter = (float)o;
        gameObject.GetComponent<Collider>().enabled = false;
        transform.Translate(transform.rotation * Vector3.back * 0.1f);
        StartCoroutine(Destroy(destroyAfter));
    }

    private IEnumerator Destroy(float _time)
    {
        yield return new WaitForSeconds(_time);
        DestroyImmediate(gameObject);
    }
}
