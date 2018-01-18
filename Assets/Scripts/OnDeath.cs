using UnityEngine;

public class OnDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = Vector3.zero;
    }
}
