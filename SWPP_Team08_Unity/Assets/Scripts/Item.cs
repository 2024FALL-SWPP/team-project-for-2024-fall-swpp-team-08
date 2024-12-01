using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

// List of Item
abstract class Item : MonoBehaviour
{
    abstract public string GetName();
    abstract public float GetTime();
    abstract public void ApplyItemEffect(PlayerController playerController);
    abstract public void ShowTimeUI(GameObject[] timeCanvasPrefabs);
    abstract public void RemoveItemEffect(PlayerController playerController);
}


class ItemTejava : Item
{
    private static string itemName = "Tejava";
    private static float time = 0.0f;

    public override string GetName()
    {
        return itemName;
    }

    public override float GetTime()
    {
        return time;
    }

    public override void ApplyItemEffect(PlayerController playerController)
    {
        playerController.AddScore();
    }

    public override void ShowTimeUI(GameObject[] timeCanvasPrefabs)
    {

    }

    public override void RemoveItemEffect(PlayerController playerController)
    {
        
    }
}

class ItemBoost : Item
{
    private static string itemName = "Item_Boost";
    private static float time = 2.0f;
    private static GameObject timeUI;
    private static Slider slider;

    void Update()
    {
        if (slider != null)
        {
            slider.value -= Time.deltaTime * (1.0f / time);
        }
    }

    public override string GetName()
    {
        return itemName;
    }

    public override float GetTime()
    {
        return time;
    }

    public override void ApplyItemEffect(PlayerController playerController)
    {
        playerController.BoostOn();

        Collider[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle")
                                        .Select(o => o.GetComponent<Collider>())
                                        .ToArray();
        Collider[] obstacleChildren = GameObject.FindGameObjectsWithTag("ObstacleChild")
                                        .Select(o => o.GetComponent<Collider>())
                                        .ToArray();

        foreach (Collider obstacle in obstacles)
        {
            if (obstacle != null)
            {
                Physics.IgnoreCollision(obstacle, playerController.GetComponent<Collider>(), true);
            }
        }
        foreach (Collider obstacleChild in obstacleChildren)
        {
            if (obstacleChild != null)
            {
                Physics.IgnoreCollision(obstacleChild, playerController.GetComponent<Collider>(), true);
            }
        }
    }

    public override void ShowTimeUI(GameObject[] timeCanvasPrefabs)
    {
        timeUI = Instantiate(timeCanvasPrefabs[0], new Vector3(0, 0, 0), Quaternion.identity);
        slider = timeUI.transform.Find("BoostSlider").GetComponent<Slider>();
    }

    public override void RemoveItemEffect(PlayerController playerController)
    {
        playerController.BoostOff();
        
        Collider[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle")
                                        .Select(o => o.GetComponent<Collider>())
                                        .ToArray();
        Collider[] obstacleChildren = GameObject.FindGameObjectsWithTag("ObstacleChild")
                                        .Select(o => o.GetComponent<Collider>())
                                        .ToArray();

        foreach (Collider obstacle in obstacles)
        {
            if (obstacle != null)
            {
                Physics.IgnoreCollision(obstacle, playerController.GetComponent<Collider>(), false);
            }
        }
        foreach (Collider obstacleChild in obstacleChildren)
        {
            if (obstacleChild != null)
            {
                Physics.IgnoreCollision(obstacleChild, playerController.GetComponent<Collider>(), false);
            }
        }

        if (timeUI != null)
        {
            Destroy(timeUI);
        }
    }
}

class ItemThunder : Item
{
    private static string itemName = "Item_Thunder";
    private static float time = 0.0f;

    public override string GetName()
    {
        return itemName;
    }

    public override float GetTime()
    {
        return time;
    }

    public override void ApplyItemEffect(PlayerController playerController)
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        var closestObstacles = obstacles.Where(o => o.transform.position.x > transform.position.x)
                                        .OrderBy(o => Mathf.Abs(o.transform.position.x - transform.position.x))
                                        .Take(3)
                                        .ToArray();
        
        foreach (GameObject obstacle in closestObstacles)
        {
            Destroy(obstacle);
        }
    }

    public override void ShowTimeUI(GameObject[] timeCanvasPrefabs)
    {
        
    }

    public override void RemoveItemEffect(PlayerController playerController)
    {

    }
}

class ItemFly : Item
{
    private static string itemName = "Item_Fly";
    private static float time = 3.0f;
    private static GameObject timeUI;
    private static Slider slider;

    void Update()
    {
        if (slider != null)
        {
            slider.value -= Time.deltaTime * (1.0f / time);
        }
    }

    public override string GetName()
    {
        return itemName;
    }

    public override float GetTime()
    {
        return time;
    }
    
    public override void ApplyItemEffect(PlayerController playerController)
    {
        playerController.FlyOn();
    }

    public override void ShowTimeUI(GameObject[] timeCanvasPrefabs)
    {
        timeUI = Instantiate(timeCanvasPrefabs[1], new Vector3(0, 0, 0), Quaternion.identity);
        slider = timeUI.transform.Find("FlySlider").GetComponent<Slider>();
    }

    public override void RemoveItemEffect(PlayerController playerController)
    {
        playerController.FlyOff();

        if (timeUI != null)
        {
            Destroy(timeUI);
        }
    }
}

class ItemDouble : Item
{
    private static string itemName = "Item_Double";
    private static float time = 3.0f;
    private static GameObject timeUI;
    private static Slider slider;

    void Update()
    {
        if (slider != null)
        {
            slider.value -= Time.deltaTime * (1.0f / time);
        }
    }

    public override string GetName()
    {
        return itemName;
    }

    public override float GetTime()
    {
        return time;
    }
    
    public override void ApplyItemEffect(PlayerController playerController)
    {
        playerController.DoubleOn();
    }

    public override void ShowTimeUI(GameObject[] timeCanvasPrefabs)
    {
        timeUI = Instantiate(timeCanvasPrefabs[2], new Vector3(0, 0, 0), Quaternion.identity);
        slider = timeUI.transform.Find("DoubleSlider").GetComponent<Slider>();
    }

    public override void RemoveItemEffect(PlayerController playerController)
    {
        playerController.DoubleOff();

        if (timeUI != null)
        {
            Destroy(timeUI);
        }
    }
}