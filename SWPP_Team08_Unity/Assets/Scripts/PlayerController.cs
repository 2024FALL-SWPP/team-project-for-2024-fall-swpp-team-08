using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    // Arrays for Start & End coordinates of each stage
    // For displaying process rate
    public float[] startArr = {0f, 100f, 200f};
    public float[] endArr = {50f, 150f, 300f};
    
    public float forwardSpeed = 5f;
    public float horizontalStep = 10f;
    public float slideDuration = 1f;
    
    // Related to jump action : implements more 'arcade-game-like' jump
    public float jumpForce = 150f;
    public float gravityMultiplier = 100f;
    // Related to hop action : when player moves left or right, it 'hops' naturally
    public float hopForce = 1000f;
    public float hopDuration = 0.5f;
    
    private int currentLane = 3;
    private int currentStage = 1;

    private bool isGameOver = false;
    private bool isMoving = false;
    private bool isJumping = false;
    private bool isSliding = false;
    private Rigidbody rb;

    private bool itemBoost = false;
    private bool itemThunder = false;
    private bool itemFly = false;
    private bool itemDouble = false;

    private int score = 0;
    private UIManager uiManager;
    private SceneController sceneController;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        sceneController = GameObject.Find("UIManager").GetComponent<SceneController>();

        InitScore();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameOver)
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

            if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping)
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
/*
    private void MoveLeft()
    {
        // TO-DO 
        // Add Animation for Move
        if(currentLane > 1) 
        {
            if(!isJumping)
            {
                rb.velocity = new Vector3(rb.velocity.x, hopForce, rb.velocity.z);
            }

            Vector3 newPosition = transform.position;
            newPosition.z += horizontalStep;
            transform.position = newPosition;
            currentLane--;
        }
    }

    private void MoveRight()
    {
        // TO-DO
        // Add Animation for Move
        if(currentLane < 5) 
        {
            if(!isJumping)
            {
                rb.velocity = new Vector3(rb.velocity.x, hopForce, rb.velocity.z);
            }
            
            Vector3 newPosition = transform.position;
            newPosition.z -= horizontalStep;
            transform.position = newPosition;
            currentLane++;
        }
    }
*/

    private void MoveLeft()
    {
        if(currentLane > 1 && !isJumping && !isMoving)
        {
            StartCoroutine(MoveLeftSmooth());
        }
    }

    private void MoveRight()
    {
        if(currentLane < 5 && !isJumping && !isMoving)
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
        isJumping = true;
        // TO-DO
        // Add Animation for Jump
        rb.velocity = Vector3.up * jumpForce;
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        transform.Rotate(0, 0, 75);
        // TO-DO
        // Add Animation for Slide
        yield return new WaitForSeconds(slideDuration);
        transform.Rotate(0, 0, -75);
        isSliding = false;
    }

    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "Obstacle") 
        {
            if (!itemBoost)
            {
                isGameOver = true;
                uiManager.ShowGameOverUI();
            }
        }
        
        if (collider.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }

        /******************************
        [아이템별 Tag 및 설명]
        1. 데자와 (점수) : Tejava
        2. 오리부스트 (속도 1.5배, 장애물 무시 / Duration 3초) : Item_Boost
        3. 벼락치기 (가장 근접한 장애물 3개 삭제) : Item_Thunder
        4. 오리날다 (이단 점프 + 점프 중 좌우 컨트롤 / Duration 3초) : Item_Fly
        5. 곱빼기 (점수 2배 / Duration 3초) : Item_Double
        *******************************/

        if (collider.gameObject.CompareTag("Tejava"))
        {
            Destroy(collider.gameObject);
            AddScore();
        }

        if (collider.gameObject.tag == "Item_Boost")
        {
            itemBoost = true;
            Destroy(collider.gameObject);

            float initSpeed = forwardSpeed;
            SetSpeed(2f * forwardSpeed);

            Collider[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle")
                                            .Select(o => o.GetComponent<Collider>())
                                            .ToArray();
            foreach (Collider obstacle in obstacles)
            {
                Physics.IgnoreCollision(obstacle, GetComponent<Collider>(), true);
            }

            StartCoroutine(ResetBoost(initSpeed, obstacles));
        }
    }

    private IEnumerator ResetBoost(float initSpeed, Collider[] obstacles)
    {
        yield return new WaitForSeconds(3f);
        
        SetSpeed(initSpeed);
        foreach (Collider obstacle in obstacles)
        {
            Physics.IgnoreCollision(obstacle, GetComponent<Collider>(), false);
        }

        itemBoost = false;
    }

    public float GetProcessRate()
    {
        float currentXCoordinate = transform.position.x;
        float totalDistance = 0;
        CheckStageCleared(currentXCoordinate);

        for (int i = 0; i < 3; i++)
        {
            totalDistance += endArr[i] - startArr[i];
        }

        switch (currentStage)
        {
            case 1:
                return (currentXCoordinate-startArr[0]) / totalDistance;
            case 2:
                return (currentXCoordinate-startArr[1]+endArr[0]-startArr[0]) / totalDistance;
            case 3:
                return (currentXCoordinate-startArr[2]+endArr[1]-startArr[1]+endArr[0]-startArr[0]) / totalDistance;
            default:
                return 0;
        }
    }

    public void CheckStageCleared(float currentXCoordinate)
    {
        if (currentXCoordinate > endArr[currentStage - 1])
        {
            sceneController.ChangeScene();
        }
    }

    public void SetSpeed(float speed)
    {
        forwardSpeed = speed;
    }
    
    public void InitScore()
    {
        score = PlayerPrefs.GetInt("Score");  // TODO - Initial Value?
    }

    public void AddScore()
    {
        score++;
        PlayerPrefs.SetInt("Score", score);
        uiManager.UpdateScoreText(score);
    }

    public int GetScore() 
    {
        return score;
    }
}


