using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// List of Item
abstract class Item : MonoBehaviour
{
    abstract public string GetName();
    abstract public float GetTime();
    abstract public void ApplyItemEffect(PlayerController playerController);
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

    public override void RemoveItemEffect(PlayerController playerController)
    {
        
    }
}

class ItemBoost : Item
{
    private static string itemName = "Item_Boost";
    private static float time = 1.5f;

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
        foreach (Collider obstacle in obstacles)
        {
            if (obstacle != null)
            {
                Physics.IgnoreCollision(obstacle, playerController.GetComponent<Collider>(), true);
            }
        }
    }

    public override void RemoveItemEffect(PlayerController playerController)
    {
        playerController.BoostOff();
        
        Collider[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle")
                                        .Select(o => o.GetComponent<Collider>())
                                        .ToArray();
        foreach (Collider obstacle in obstacles)
        {
            if (obstacle != null)
            {
                Physics.IgnoreCollision(obstacle, playerController.GetComponent<Collider>(), false);
            }
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

    public override void RemoveItemEffect(PlayerController playerController)
    {

    }
}

class ItemFly : Item
{
    private static string itemName = "Item_Fly";
    private static float time = 3.0f;

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

    public override void RemoveItemEffect(PlayerController playerController)
    {
        playerController.FlyOff();
    }
}

class ItemDouble : Item
{
    private static string itemName = "Item_Double";
    private static float time = 3.0f;

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

    public override void RemoveItemEffect(PlayerController playerController)
    {
        playerController.DoubleOff();
    }
}