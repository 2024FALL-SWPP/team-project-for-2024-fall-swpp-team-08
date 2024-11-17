using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager_Stage1 : MonoBehaviour
{
    public GameObject[] stage1ModulePrefabs;
    public GameObject[] stage2ModulePrefabs;
    public GameObject[] stage3ModulePrefabs;
    private Scene currentScene;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        switch(currentScene.name)
        {
            case "Stage1Scene":
                SpawnStage1();
                break;
            case "Stage2Scene":
                SpawnStage2();
                break;
            case "Stage3Scene":
                SpawnStage3();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnStage1()
    {
        // Stage Length : 920f (첫 20f는 장애물 X)
        // Obstacle Interval : 10f (avg 2sec per obstacle)
        // Item Interval : ??? (avg ???sec per item)
        // 9 Modules * 10 Obstacles (Module Length = 100f)

        for(int i = 0; i < 9; i++)
        {
            GameObject randomModule = stage1ModulePrefabs[Random.Range(0, stage1ModulePrefabs.Length)];
            Instantiate(randomModule, new Vector3(i*100, 0, 0), randomModule.transform.rotation);
            Debug.Log("Spawn Module " + (i+1));
        }
    }

    private void SpawnStage2()
    {
        // Stage Length : 920f
        // Obstacle Interval : 9f (avg 1.8sec per obstacle)
        // Item Interval : ??? (avg ???sec per item)
        // 10 Modules * 10 Obstacles (Module Length = 90f)

        for(int i = 0; i < 10; i++)
        {
            GameObject randomModule = stage2ModulePrefabs[Random.Range(0, stage2ModulePrefabs.Length)];
            Instantiate(randomModule, new Vector3(i*90, 0, 0), randomModule.transform.rotation);
        }
    }

    private void SpawnStage3()
    {
        // Stage Length : 920f
        // Obstacle Interval : 7.5f (avg 1.5sec per obstacle)
        // Item Interval : ??? (avg ???sec per item)
        // 12 Modules * 10 Obstacles (Module Length = 75f)

        for(int i = 0; i < 12; i++)
        {
            GameObject randomModule = stage3ModulePrefabs[Random.Range(0, stage3ModulePrefabs.Length)];
            Instantiate(randomModule, new Vector3(i*75, 0, 0), randomModule.transform.rotation);
        }
    }
}
