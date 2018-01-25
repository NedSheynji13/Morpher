using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBridgeForm : MonoBehaviour
{
    #region Variables
    public GameObject connectedBridge, hover, lilsphere, movingParticle;
    public Transform LinePointGroup;
    public AudioSource movingSound;
    private LineRenderer connection;
    private Vector3[] LinePoints;
    private Vector3 SpawnPoint, basePosition, basePositionPlayer;
    private GameObject player, lilspheretemp, temp;
    private Rigidbody physix;
    private bool move, playing = false;
    private int nextposition = 0;
    private float time;
    #endregion

    private void Start()
    {
        connection = GetComponentInChildren<LineRenderer>();
        basePosition = hover.transform.position;
        player = null;
        move = false;
        LinePoints = new Vector3[LinePointGroup.childCount];
        for (int i = 0; i < LinePoints.Length; i++) LinePoints[i] = LinePointGroup.GetChild(i).position;

        connection.positionCount = LinePoints.Length + 2;
        connection.SetPosition(0, transform.position + new Vector3(0, 1.531444f, 0)); //Set the first line segment always to the position of the bridge
        for (int i = 0; i < LinePoints.Length; i++) connection.SetPosition(i + 1, LinePoints[i]);
        connection.SetPosition(connection.positionCount - 1, connectedBridge.transform.position + new Vector3(0, 1.531444f, 0)); //Set the last line segment always to the position of the connected bridge
        SpawnPoint = connectedBridge.transform.position + new Vector3(0, 2, 0);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!Morphing.teleportSickness)
        {
            if (Input.GetKey(KeyCode.F) && (Morphing.abilities.Bridge == (Morphing.unlocked & Morphing.abilities.Bridge)))
            {
                physix = other.GetComponent<Rigidbody>();
                player = other.gameObject;
                basePositionPlayer = player.transform.position;
                move = true;
                if (!playing)
                {
                    Instantiate(movingSound);
                    temp = Instantiate(movingParticle);
                    lilspheretemp = Instantiate(lilsphere, other.transform.position, Quaternion.identity);
                }
                playing = true;
            }
        }
    }

    private IEnumerator WaitforTeleport()
    {
        yield return new WaitForSeconds(2);
        Morphing.teleportSickness = false;
    }

    private void Update()
    {
        Hovering();
        if (move)
        {
            Morphing.teleportSickness = true;
            player.transform.position = transform.position;
            Morphing.wantedscale = Vector3.zero;

            Moving();

            if (player.transform.position == connection.GetPosition(connection.positionCount - 1))
            {
                Morphing.forced = true;
                physix.velocity = Vector3.zero;
                player.transform.position = SpawnPoint;
                player.GetComponent<Rigidbody>().useGravity = true;
                player = null;
                Morphing.wantedscale = Vector3.one;
                StartCoroutine(WaitforTeleport());
                nextposition = 0;
                move = playing = false;
                DestroyAudiosource();
                Destroy(temp);
                Destroy(lilspheretemp);
            }
        }
    }

    private void Moving()
    {
        player.transform.position = lilspheretemp.transform.position = temp.transform.position = Vector3.Lerp(basePositionPlayer, connection.GetPosition(nextposition), (time / (Vector3.Distance(basePositionPlayer, connection.GetPosition(nextposition)))) * 20);
        if (player.transform.position == connection.GetPosition(nextposition))
        {
            nextposition++;
            basePositionPlayer = player.transform.position;
            time = 0;
        }
        else
            time += Time.deltaTime;
    }

    private void Hovering()
    {
        hover.transform.Rotate(0, 2, 0);
        hover.transform.position = new Vector3(hover.transform.position.x, Mathf.Sin(Time.time) * 0.1f + basePosition.y, hover.transform.position.z);
    }

    private void DestroyAudiosource()
    {
        foreach (GameObject Sound in GameObject.FindGameObjectsWithTag("Sound")) Destroy(Sound);
    }
}
