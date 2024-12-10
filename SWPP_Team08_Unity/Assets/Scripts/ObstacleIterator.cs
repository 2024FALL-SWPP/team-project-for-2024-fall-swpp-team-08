using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Aggregate : MonoBehaviour
{
    public abstract Iterator iterator();
} 

public interface Iterator 
{
    bool hasNext();
    GameObject next();
}

public class ObstacleIterator : Iterator
{
    private List<GameObject> obstacles;
    private int nextIndex = 0;

    public ObstacleIterator(List<GameObject> obstacleModules)
    {
        obstacles = new List<GameObject>();
        foreach (GameObject obstacleModule in obstacleModules)
        {
            foreach (Transform child in obstacleModule.transform)
            {
                obstacles.Add(child.gameObject);
            }
        }
    }

    public bool hasNext() {
        return nextIndex < obstacles.Count;
    }

    public GameObject next() {
        return obstacles[nextIndex++];
    }

}
