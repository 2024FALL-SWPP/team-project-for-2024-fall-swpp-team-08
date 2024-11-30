using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float catSpeed = 3f;
    public float weaselSpeed = 5f;
    public float moveRange = 25f;
    private bool movingRight = true;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.name.Contains("Cat"))
        {
            HandleLRMovement(catSpeed);
        }

        if(gameObject.name.Contains("Weasel"))
        {
            HandleLRMovement(weaselSpeed);
        }
    }

    private void HandleLRMovement(float speed)
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if(movingRight && transform.position.z >= moveRange)
        {
            FlipDirection();
        }
        else if(!movingRight && transform.position.z <= -moveRange)
        {
            FlipDirection();
        }
    }

    private void FlipDirection()
    {
        movingRight = !movingRight;
        transform.Rotate(0, 180, 0);
    }
}
