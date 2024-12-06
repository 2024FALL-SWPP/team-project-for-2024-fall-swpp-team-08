using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float catSpeed = 3f;
    public float weaselSpeed = 5f;
    public float robotSpeed = 10f;
    public float studentSpeed = 1f;
    public float maxBoundary = 25f;
    public float haltDuration = 1f;
    public float fbMovementDuration = 1f;
    public float complexMovementDuration = 1f;
    private bool movingRight = true;
    private bool movingForward = true;
    private int step = 0;
    private bool isHalted = false;
    private float haltTimer = 0f;
    private float actionTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.name.Contains("Cat"))
        {
            LeftRightMovement(catSpeed);
        }

        if(gameObject.name.Contains("Weasel"))
        {
            LeftRightMovement(weaselSpeed);
        }

        if(gameObject.name.Contains("Robot"))
        {
            if(gameObject.name.Contains("LR"))
            {
                LeftRightMovement(robotSpeed);
            }
            if(gameObject.name.Contains("FB")) 
            {
                FrontBackMovement(robotSpeed);   
            }
            if(gameObject.name.Contains("Complex")) 
            {
                ComplexMovement(robotSpeed);   
            }
        }

        if(gameObject.name.Contains("Student"))
        {
            LeftRightMovement(studentSpeed);
        }
    }

    private void LeftRightMovement(float speed)
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        CheckBoundary();
    }

    private void FrontBackMovement(float speed)
    {
        if(isHalted)
        {
            haltTimer += Time.deltaTime;
            if(haltTimer > haltDuration)
            {
                isHalted = false;
                haltTimer = 0f;
                movingForward = !movingForward;
            }
            return;
        }

        actionTimer += Time.deltaTime;
        if(actionTimer < fbMovementDuration)
        {
            if(movingForward)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.back * speed * Time.deltaTime);
            }
        }
        else
        {
            Halt();
            actionTimer = 0f;
        }
    }

    private void ComplexMovement(float speed)
    {
        if(isHalted)
        {
            haltTimer += Time.deltaTime;
            if(haltTimer > haltDuration)
            {
                isHalted = false;
                haltTimer = 0f;
                step++;
                if(step > 3)
                {
                    step = 0;
                }
            }
            return;
        }

        actionTimer += Time.deltaTime;
        if(actionTimer < complexMovementDuration)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            Halt();
            actionTimer = 0f;

            switch(step)
            {
                case 0:
                    if(movingForward && Math.Abs(transform.position.z - 20.0f) < 1f ||
                    !movingForward && Math.Abs(transform.position.z - -20.0f) < 1f)
                    {
                        RotateLeft();
                        step = 2;
                    }
                    else {
                        RotateRight();
                    }
                    break;
                case 1:
                    RotateRight();
                    movingForward = !movingForward;
                    break;
                case 2:
                    if(!movingForward && Math.Abs(transform.position.z - 20.0f) < 1f ||
                    movingForward && Math.Abs(transform.position.z - -20.0f) < 1f) 
                    {
                        RotateRight();
                        step = 0;
                    }
                    else {
                        RotateLeft();
                    }
                    break;
                case 3:
                    RotateLeft();
                    movingForward = !movingForward;
                    break;
            }
        }
    }

    private void CheckBoundary()
    {
        if(transform.position.z < -maxBoundary || transform.position.z > maxBoundary)
        {
            FlipDirection();
        }
    }
    
    private void FlipDirection()
    {
        movingRight = !movingRight;
        transform.Rotate(0, 180, 0);
    }

    private void Halt()
    {
        isHalted = true;
    }

    private void RotateLeft()
    {
        transform.Rotate(0, -90, 0);
    }

    private void RotateRight()
    {
        transform.Rotate(0, 90, 0);
    }
}
