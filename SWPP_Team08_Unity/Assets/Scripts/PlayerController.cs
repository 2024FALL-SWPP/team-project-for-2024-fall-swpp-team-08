using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    // Arrays for Start & End coordinates of each stage
    // For displaying process rate
    private float[] endArr = {900f, 900f, 900f}; // TODO : 종료지점 설정
    private float totalDistance = 0.0f;
    
    public float forwardSpeed = 10.0f;
    private float initSpeed;
    public float horizontalStep = 10.0f;
    public float slideDuration = 1.0f;
    public float slideOffset = 1.0f;
    
    // Related to jump action : implements more 'arcade-game-like' jump
    public float jumpForce = 80.0f;
    public float gravityMultiplier = 80.0f;
    // Related to hop action : when player moves left or right, it 'hops' naturally
    public float hopForce = 10.0f;
    public float hopDuration = 0.5f;
    
    private int currentLane = 3;
    private int currentStage = 1;

    private bool isMoving = false;
    private bool isJumping = false;
    private bool isSliding = false;
    private bool canDoubleJump = false;
    private Rigidbody rb;

    private bool itemBoost = false;
    private bool itemFly = false;
    private bool itemDouble = false;

    private int score = 0;
    private UIManager uiManager;
    private SceneController sceneController;
    private GameStateManager gameStateManager;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        totalDistance = GetTotalDistance();

        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        sceneController = GameObject.Find("UIManager").GetComponent<SceneController>();
        gameStateManager = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();

        InitScore();
        totalDistance = GetTotalDistance();
        currentStage = GetCurrentStage();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStateManager.GetState() == "GamePlay")
        {
            transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRight();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }

            if(Input.GetKeyDown(KeyCode.DownArrow) && !isSliding)
            {
                StartCoroutine(Slide());
            }

            if(isJumping)
            {
                rb.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration);
            }
            uiManager.UpdateProgressBar(GetProcessRate());
        }
    }

    private void MoveLeft()
    {
        if(currentLane > 1 && (!isJumping || itemFly) && !isMoving)
        {
            StartCoroutine(MoveLeftSmooth());
        }
    }

    private void MoveRight()
    {
        if(currentLane < 5 && (!isJumping || itemFly) && !isMoving)
        {
            StartCoroutine(MoveRightSmooth());
        }
    }

    private IEnumerator MoveLeftSmooth()
    {
        isMoving = true;

        // TO-DO
        // Add Animation for Lateral Movement

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.forward * horizontalStep;
        float elapsedTime = 0f;

        // rb.velocity = new Vector3(rb.velocity.x, hopForce, rb.velocity.z);

        while (elapsedTime < hopDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / hopDuration;

            t = t * t * (3f - 2f * t);
            transform.position = Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }

        transform.position = endPos;
        currentLane--;
        isMoving = false;
    }

    private IEnumerator MoveRightSmooth()
    {
        isMoving = true;
        
        // TO-DO
        // Add Animation for Lateral Movement
        
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.back * horizontalStep;
        float elapsedTime = 0f;

        // rb.velocity = new Vector3(rb.velocity.x, hopForce, rb.velocity.z);

        while (elapsedTime < hopDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / hopDuration;

            t = t * t * (3f - 2f * t);
            transform.position = Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }

        transform.position = endPos;
        currentLane++;
        isMoving = false;
    }

    private void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            rb.velocity = Vector3.up * jumpForce;

            if (itemFly)
            {
                canDoubleJump = true;
            }
        }
        else if (canDoubleJump)
        {
            rb.velocity = Vector3.up * jumpForce;
            canDoubleJump = false;
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;

        transform.position = new Vector3(transform.position.x, transform.position.y - slideOffset, transform.position.z);
        transform.Rotate(0, 0, 90);
        // TO-DO
        // Add Animation for Slide
        yield return new WaitForSeconds(slideDuration);
        transform.Rotate(0, 0, -90);
        transform.position = new Vector3(transform.position.x, transform.position.y + slideOffset, transform.position.z);

        isSliding = false;
    }

    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "Obstacle") 
        {
            if (!itemBoost)
            {
                gameStateManager.EnterGameOverState();
            }
        }
        
        if (collider.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
    
    // private void OnTriggerEnter(Collider other)
    // {
    //     /******************************
    //     [아이템별 Tag 및 설명]
    //     1. 데자와 (점수) : Tejava
    //     2. 오리부스트 (속도 1.5배, 장애물 무시 / Duration 3초) : Item_Boost
    //     3. 벼락치기 (가장 근접한 장애물 3개 삭제) : Item_Thunder
    //     4. 오리날다 (이단 점프 + 점프 중 좌우 컨트롤 / Duration 3초) : Item_Fly
    //     5. 곱빼기 (점수 2배 / Duration 3초) : Item_Double
    //     *******************************/

    //     // 1. 데자와 (Tejava)
    //     if (other.gameObject.CompareTag("Tejava"))
    //     {
    //         Destroy(other.gameObject);
    //         AddScore();
    //     }

    //     // 2. 오리부스트 (Item_Boost)
    //     if (other.gameObject.tag == "Item_Boost")
    //     {
    //         itemBoost = true;
    //         Destroy(other.gameObject);

    //         float initSpeed = forwardSpeed;
    //         SetSpeed(2f * forwardSpeed);

    //         Collider[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle")
    //                                         .Select(o => o.GetComponent<Collider>())
    //                                         .ToArray();
    //         foreach (Collider obstacle in obstacles)
    //         {
    //             if(obstacle != null)
    //             {
    //                 Physics.IgnoreCollision(obstacle, GetComponent<Collider>(), true);
    //             }
    //         }

    //         StartCoroutine(ResetBoost(initSpeed, obstacles));
    //     }

    //     // 3. 벼락치기 (Item_Thunder)
    //     if (other.gameObject.tag == "Item_Thunder")
    //     {
    //         Destroy(other.gameObject);

    //         GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
    //         var closestObstacles = obstacles.Where(o => o.transform.position.x > transform.position.x)
    //                                         .OrderBy(o => Mathf.Abs(o.transform.position.x - transform.position.x))
    //                                         .Take(3)
    //                                         .ToArray();
            
    //         foreach (GameObject obstacle in closestObstacles)
    //         {
    //             Destroy(obstacle);
    //         }
    //     }

    //     // 4. 오리날다 (Item_Fly)
    //     if (other.gameObject.tag == "Item_Fly")
    //     {
    //         itemFly = true;
    //         canDoubleJump = true;
    //         Destroy(other.gameObject);

    //         StartCoroutine(ResetFly());
    //     }

    //     // 5. 곱빼기 (Item_Double)
    //     if (other.gameObject.tag == "Item_Double")
    //     {
    //         itemDouble = true;
    //         Destroy(other.gameObject);

    //         StartCoroutine(ResetDouble());
    //     }
    // }
    
    // private IEnumerator ResetBoost(float initSpeed, Collider[] obstacles)
    // {
    //     yield return new WaitForSeconds(3f);
        
    //     SetSpeed(initSpeed);
    //     foreach (Collider obstacle in obstacles)
    //     {
    //         if(obstacle != null)
    //         {
    //             Physics.IgnoreCollision(obstacle, GetComponent<Collider>(), false);
    //         }
    //     }

    //     itemBoost = false;
    // }

    // private IEnumerator ResetFly()
    // {
    //     yield return new WaitForSeconds(3f);
    //     itemFly = false;
    //     canDoubleJump = false;
    // }

    // private IEnumerator ResetDouble()
    // {
    //     yield return new WaitForSeconds(3f);
    //     itemDouble = false;
    // }

    private int GetCurrentStage()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (char.IsDigit(sceneName[5]))
        {
            return sceneName[5] - '0';
        }
        return 0;
    } 

    private float GetTotalDistance()
    {
        float distance = 0.0f;
        for (int i = 0; i < 3; i++)
        {
            distance += endArr[i];
        }
        return distance;
    }

    public float GetProcessRate()
    {
        float currentXCoordinate = transform.position.x;
        CheckStageCleared(currentXCoordinate);
        switch (currentStage)
        {
            case 1:
                return currentXCoordinate / totalDistance;
            case 2:
                return (currentXCoordinate+endArr[0]) / totalDistance;
            case 3:
                return (currentXCoordinate+endArr[1]+endArr[0]) / totalDistance;
            default:
                return 0;
        }
    }

    public void CheckStageCleared(float currentXCoordinate)
    {
        if (currentXCoordinate > endArr[currentStage - 1])
        {
            PlayerPrefs.SetInt("Score", score);
            gameStateManager.EnterStageClearState();
        }
    }

    public void SetSpeed(float speed)
    {
        forwardSpeed = speed;
    }
    
    public void InitScore()
    {
        score = PlayerPrefs.GetInt("Score");
    }

    public void AddScore()
    {
        if (itemDouble) score += 2;
        else score += 1;
        uiManager.UpdateScoreText(score);
    }

    public int GetScore() 
    {
        return score;
    }

    // -------------------------- Item --------------------------
    public void BoostOn()
    {
        itemBoost = true;
        initSpeed = forwardSpeed;
        SetSpeed(2.0f * forwardSpeed);
    }

    public void BoostOff()
    {
        itemBoost = false;
        SetSpeed(initSpeed);
    }

    public void FlyOn()
    {
        itemFly = true;
        canDoubleJump = true;
    }

    public void FlyOff()
    {
        itemFly = false;
        canDoubleJump = false;
    }

    public void DoubleOn()
    {
        itemDouble = true;
    }

    public void DoubleOff()
    {
        itemDouble = false;
    }
}