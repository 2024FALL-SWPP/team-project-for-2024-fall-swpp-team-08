using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float stageLength = 700.0f;
    private float mapLength = 50.0f;

    public GameObject[] mapModulePrefabs;

    // Start is called before the first frame update
    void Start()
    {
        SpawnMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnMap()
    {
        for (int i = 0; i < (stageLength / mapLength - 1); i++)
        {
            int index = Random.Range(0, mapModulePrefabs.Length);
            Vector3 spawnPos = new Vector3(mapLength * (i + 1), 0, 0);
            Instantiate(mapModulePrefabs[index], spawnPos, mapModulePrefabs[index].transform.rotation);
        }
    }
}
