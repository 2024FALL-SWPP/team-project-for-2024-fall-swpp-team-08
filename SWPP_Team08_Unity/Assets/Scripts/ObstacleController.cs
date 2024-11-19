using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.name.Contains("Cat"))
        {
            HandleCat();
        }

        if(gameObject.name.Contains("Weasel"))
        {
            HandleWeasel();
        }
    }

    private void HandleCat()
    {

    }

    private void HandleWeasel()
    {
    
    }
}
