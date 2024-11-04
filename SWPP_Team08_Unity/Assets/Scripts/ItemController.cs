using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public GameObject gameManager;
    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Tejava"))
        {
            Destroy(other.gameObject);
            score++;
        }
    }

    public int getScore() 
    {
        return score;
    }
}
