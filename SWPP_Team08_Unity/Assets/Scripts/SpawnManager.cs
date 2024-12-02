using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
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
                //SpawnStage2();
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
        // Stage Length : 1820f (첫 20f는 장애물 X)
        // Module Length : 200f
        // Obstacle Interval : avg 20.0f (avg 2.00sec per obstacle)
        // 9 Modules * (10 Obstacles + 3 Items + 10 Tejavas)

        for(int i = 0; i < 9; i++)
        {
            GameObject randomModule = stage1ModulePrefabs[Random.Range(0, stage1ModulePrefabs.Length)];
            Instantiate(randomModule, new Vector3(20 + i*200, 0, 0), randomModule.transform.rotation);
        }
    }

    private void SpawnStage2()
    {
        // Stage Length : 1820f (첫 20f는 장애물 X)
        // Module Length : 180f
        // Obstacle Interval : 18.0f (avg 1.80sec per obstacle)
        // 10 Modules * (10 Obstacles + 3 Items + 10 Tejavas)

        for(int i = 0; i < 10; i++)
        {
            GameObject randomModule = stage2ModulePrefabs[Random.Range(0, stage2ModulePrefabs.Length)];
            Instantiate(randomModule, new Vector3(20 + i*180, 0, 0), randomModule.transform.rotation);
        }
    }

    private void SpawnStage3()
    {
        // Stage Length : 1820f (첫 20f는 장애물 X)
        // Module Length : 150f
        // Obstacle Interval : 15.0f (avg 1.50sec per obstacle)
        // 12 Modules * (10 Obstacles + 5 Items + 10 Tejavas)

        for(int i = 0; i < 12; i++)
        {
            GameObject randomModule = stage3ModulePrefabs[Random.Range(0, stage3ModulePrefabs.Length)];
            Instantiate(randomModule, new Vector3(20 + i*150, 0, 0), randomModule.transform.rotation);
        }
    }
}
