using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSpawner : MonoBehaviour
{

    public GameObject[] prefabLibrary;

    public GameObject spawnedObject;

    public Transform spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnObject()
    {
        Destroy(spawnedObject);

        int randomToSpawn = 0;

        randomToSpawn = Random.Range(0, prefabLibrary.Length);

        if (prefabLibrary.Length != 0)
        {
            spawnedObject = Instantiate(prefabLibrary[randomToSpawn], spawnPosition.position, prefabLibrary[randomToSpawn].transform.rotation);
            spawnedObject.transform.SetParent(gameObject.transform);
        }

    }
}
