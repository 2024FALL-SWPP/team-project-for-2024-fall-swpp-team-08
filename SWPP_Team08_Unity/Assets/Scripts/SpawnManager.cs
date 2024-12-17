using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] stage1ModulePrefabs;
    public GameObject[] stage2ModulePrefabs;
    public GameObject[] stage3ModulePrefabs;
    public GameObject[] stage1BackgroundPrefabs;
    public GameObject[] stage2BackgroundPrefabs;
    public GameObject[] stage3BackgroundPrefabs;

    private Scene currentScene;
    private PlayerController playerController;
    private float currentCoordinate = 0.0f;
    private int nextSpawnIndex = 1;

    private List<GameObject> obstacleModules;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Duck").GetComponent<PlayerController>();

        currentScene = SceneManager.GetActiveScene();
        switch(currentScene.name)
        {
            case "Stage1Scene":
                InitStage1();
                break;
            case "Stage2Scene":
                InitStage2();
                break;
            case "Stage3Scene":
                InitStage3();
                break;
        }

        obstacleModules = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        currentCoordinate = playerController.GetCurrentCoordinate();
        
        switch(currentScene.name)
        {
            case "Stage1Scene":
                UpdateStage1();
                break;
            case "Stage2Scene":
                UpdateStage2();
                break;
            case "Stage3Scene":
                UpdateStage3();
                break;
        }

        RemoveBygoneObjects();
    }

    private void InitStage1()
    {
        nextSpawnIndex = 1;
        Instantiate(stage1BackgroundPrefabs[0], new Vector3(-180, 0, 0), stage1BackgroundPrefabs[0].transform.rotation);

        for(int i = 0; i < 2; i++)
        {
            GameObject randomModule = stage1ModulePrefabs[Random.Range(0, stage1ModulePrefabs.Length)];
            Instantiate(randomModule, new Vector3(20 + i*200, 0, 0), randomModule.transform.rotation);

            GameObject initBackground = stage1BackgroundPrefabs[i];
            Instantiate(initBackground, new Vector3(20 + i*200, 0, 0), initBackground.transform.rotation);

            nextSpawnIndex++;
        }
    }

    private void InitStage2()
    {
        nextSpawnIndex = 1;
        Instantiate(stage2BackgroundPrefabs[0], new Vector3(-160, 0, 0), stage2BackgroundPrefabs[0].transform.rotation);

        for(int i = 0; i < 2; i++)
        {
            GameObject randomModule = stage2ModulePrefabs[Random.Range(0, stage2ModulePrefabs.Length)];
            Instantiate(randomModule, new Vector3(20 + i*180, 0, 0), randomModule.transform.rotation);

            GameObject initBackground = stage2BackgroundPrefabs[i];
            Instantiate(initBackground, new Vector3(20 + i*180, 0, 0), initBackground.transform.rotation);

            nextSpawnIndex++;
        }
    }

    private void InitStage3()
    {
        nextSpawnIndex = 1;
        Instantiate(stage3BackgroundPrefabs[0], new Vector3(-130, 0, 0), stage3BackgroundPrefabs[0].transform.rotation);

        for(int i = 0; i < 2; i++)
        {
            GameObject randomModule = stage3ModulePrefabs[Random.Range(0, stage3ModulePrefabs.Length)];
            Instantiate(randomModule, new Vector3(20 + i*150, 0, 0), randomModule.transform.rotation);

            GameObject initBackground = stage3BackgroundPrefabs[i];
            Instantiate(initBackground, new Vector3(20 + i*150,0, 0), initBackground.transform.rotation);

            nextSpawnIndex++;
        }
    }

    private void UpdateStage1()
    {
        if(nextSpawnIndex > 2 && nextSpawnIndex < 10 && currentCoordinate >= 20 + (nextSpawnIndex-3)*200)
        {
            GameObject randomModule = stage1ModulePrefabs[Random.Range(0, stage1ModulePrefabs.Length)];
            Instantiate(randomModule, new Vector3(20 + (nextSpawnIndex-1)*200, 0, 0), randomModule.transform.rotation);
        }

        if(nextSpawnIndex > 2 && nextSpawnIndex < 11 && currentCoordinate >= 20 + (nextSpawnIndex-3)*200)
        {
            GameObject curBackground = stage1BackgroundPrefabs[nextSpawnIndex-1];
            Instantiate(curBackground, new Vector3(20 + (nextSpawnIndex-1)*200,0, 0), curBackground.transform.rotation);
            
            nextSpawnIndex++;
        }
    }

    private void UpdateStage2()
    {
        if(nextSpawnIndex > 2 && nextSpawnIndex < 11 && currentCoordinate >= 20 + (nextSpawnIndex-3)*180)
        {
            GameObject randomModule = stage2ModulePrefabs[Random.Range(0, stage2ModulePrefabs.Length)];
            Instantiate(randomModule, new Vector3(20 + (nextSpawnIndex-1)*180, 0, 0), randomModule.transform.rotation);
        }

        if(nextSpawnIndex > 2 && nextSpawnIndex < 12 && currentCoordinate >= 20 + (nextSpawnIndex-3)*180)
        {
            GameObject curBackground = stage2BackgroundPrefabs[nextSpawnIndex-1];
            Instantiate(curBackground, new Vector3(20 + (nextSpawnIndex-1)*180,0, 0), curBackground.transform.rotation);
            
            nextSpawnIndex++;
        }
    }

    private void UpdateStage3()
    {
        if(nextSpawnIndex > 2 && nextSpawnIndex < 13 && currentCoordinate >= 20 + (nextSpawnIndex-3)*150)
        {
            GameObject randomModule = stage3ModulePrefabs[Random.Range(0, stage3ModulePrefabs.Length)];
            Instantiate(randomModule, new Vector3(20 + (nextSpawnIndex-1)*150, 0, 0), randomModule.transform.rotation);
        }

        if(nextSpawnIndex > 2 && nextSpawnIndex < 14 && currentCoordinate >= 20 + (nextSpawnIndex-3)*150)
        {
            GameObject curBackground = stage3BackgroundPrefabs[nextSpawnIndex-1];
            Instantiate(curBackground, new Vector3(20 + (nextSpawnIndex-1)*150,0, 0), curBackground.transform.rotation);
            
            nextSpawnIndex++;
        }
    }

    private void RemoveBygoneObjects()
    {
        GameObject[] modules = GameObject.FindGameObjectsWithTag("Module");
        GameObject[] backgrounds = GameObject.FindGameObjectsWithTag("Background");

        foreach(GameObject module in modules)
        {
            if(module.transform.position.x < currentCoordinate - 220.0f)
            {
                Destroy(module);
            }
        }

        foreach(GameObject background in backgrounds)
        {
            if(background.transform.position.x < currentCoordinate - 220.0f)
            {
                Destroy(background);
            }
        }
    }
}
