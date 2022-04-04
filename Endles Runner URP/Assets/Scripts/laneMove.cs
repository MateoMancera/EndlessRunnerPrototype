using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laneMove : MonoBehaviour
{

    public Transform[] laneFloor;

    public float floorSpeed;

    public Transform posDespawn, posSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < laneFloor.Length; i++)
        {
            laneFloor[i].position = Vector3.MoveTowards(laneFloor[i].position, posDespawn.position, floorSpeed);
            if (laneFloor[i].position == posDespawn.position)
            {
                laneFloor[i].position = posSpawn.position;
                if (laneFloor[i].GetComponent<randomSpawner>().enabled)
                {
                    laneFloor[i].GetComponent<randomSpawner>().spawnObject();
                }
            }
        }
    }
}
