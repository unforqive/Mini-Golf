using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParHandler : MonoBehaviour
{
    public int par;
    public int hole;
    public GameObject player;

    public GameObject parContainer;

    public DragPower dragPower;

    [Header("Spawns")]
    public GameObject hole1Spawn;
    public GameObject hole2Spawn;
    public GameObject hole3Spawn;
    public GameObject hole4Spawn;
    public GameObject hole5Spawn;
    public GameObject hole6Spawn;
    /*
    public GameObject hole7Spawn;
    public GameObject hole8Spawn;
    public GameObject hole9Spawn;
    */

    public TMPro.TMP_Text parNumber;

    void Start()
    {
        hole = 1;
        parNumber.GetComponent<TMPro.TMP_Text>().text = "PAR " + par;
    }

    void Update()
    {
        //hole 1, hole 4, hole 6
        if (hole == 1 || hole == 4 || hole == 6)
        {
            par = 2;
            UpdatePar(par);
        }

        //hole 2, hole 3, hole 5
        if (hole == 2 || hole == 3 || hole == 5)
        {
            par = 3;
            UpdatePar(par);
        }  
    }

    public void OnTriggerEnter(Collider col)
    {
        dragPower.ball.velocity = Vector3.zero;
        //reset strokes
        dragPower.strokes = 0;
        dragPower.score.GetComponent<TMPro.TMP_Text>().text = "STROKES " + dragPower.strokes;
        dragPower.UpdateScore(dragPower.strokes);

        if (col.CompareTag("Hole 1"))
        {
            hole = 2;
            player.transform.position = hole2Spawn.transform.position;
            transform.position = player.transform.position;
        }

        if (col.CompareTag("Hole 2"))
        {
            hole = 3;
            player.transform.position = hole3Spawn.transform.position;
            transform.position = player.transform.position;
        }

        if (col.CompareTag("Hole 3"))
        {
            hole = 4;
            player.transform.position = hole4Spawn.transform.position;
            transform.position = player.transform.position;
        }

        if (col.CompareTag("Hole 4"))
        {
            hole = 5;
            player.transform.position = hole5Spawn.transform.position;
            transform.position = player.transform.position;
        }

        if (col.CompareTag("Hole 5"))
        {
            hole = 6;
            player.transform.position = hole6Spawn.transform.position;
            transform.position = player.transform.position;
        }

        if (col.CompareTag("Hole 6"))
        {
            hole = 7;
            //player.transform.position = hole7Spawn.transform.position;
            transform.position = player.transform.position;
        }
    }

    public void UpdatePar(int par)
    {
        parNumber.GetComponent<TMPro.TMP_Text>().text = "PAR " + par;
    }
}
