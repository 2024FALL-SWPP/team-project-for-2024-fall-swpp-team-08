using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    // Arrays for Start & End coordinates of each stage
    // For displaying process rate
    private float[] endArr = {1820f, 1820f, 1820f}; // TODO : 종료지점 설정
    private float totalDistance = 0.0f;
    
    public float forwardSpeed = 10.0f;
    private float initSpeed;
    public float horizontalStep = 10.0f;
    public float slideDuration = 1.0f;
    public float slideOffset = 1.0f;
    
    // Related to jump action : implements more 'arcade-game-like' jump
    public float jumpForce = 50.0f;
    public float gravityMultiplier = 70.0f;
    // Related to hop action : when player moves left or right, it 'hops' naturally
    public float hopForce = 10.0f;
    public float hopDuration = 0.5f;
    
    private int currentLane = 3;
    private int currentStage = 1;

    private bool isMoving = false;
    private bool isJumping = false;
    public bool isSliding = false;
    private bool canDoubleJump = false;
    private Rigidbody rb;

    private bool itemBoost = false;
    private bool itemFly = false;
    private bool itemDouble = false;

    private int score = 0;
    private UIManager uiManager;
    private SceneController sceneController;
    private GameStateManager gameStateManager;

    public ParticleSystem collisionParticle;
    private BoxCollider boxCollider;
    private Animator animator;
    private Vector3 boxColliderCenter;
    private Vector3 boxColliderSize;
    
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

        boxCollider = GetComponent<BoxCollider>();
        boxColliderCenter = boxCollider.center;
        boxColliderSize = boxCollider.size;
        animator = GetComponent<Animator>();
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
            animator.SetTrigger("Jump_t");

            if (itemFly)
            {
                canDoubleJump = true;
            }
        }
        else if (canDoubleJump)
        {
            rb.velocity = Vector3.up * jumpForce;
            animator.SetTrigger("Jump_t");
            canDoubleJump = false;
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetTrigger("Slide_t");
        // transform.position = new Vector3(transform.position.x, transform.position.y - slideOffset, transform.position.z);
        // transform.Rotate(0, 0, 90);
        boxCollider.center = new Vector3(boxColliderCenter.x, 0.5f, boxColliderCenter.z);
        boxCollider.size = new Vector3(boxColliderSize.x, 0.7f, boxColliderSize.z);
        
        yield return new WaitForSeconds(slideDuration);
        // transform.position = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
        // transform.Rotate(0, 0, -90);
        transform.position = new Vector3(transform.position.x, 1.399f, transform.position.z);
        boxCollider.center = boxColliderCenter;
        boxCollider.size = boxColliderSize;
        isSliding = false;
    }

    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "Obstacle" ||
            collider.gameObject.transform.parent != null && collider.gameObject.transform.parent.tag == "Obstacle") 
        {
            if (collisionParticle)
            {
                collisionParticle.Play();
            }

            if (!itemBoost)
            {
                animator.SetBool("Collide_b", true);
                gameStateManager.EnterGameOverState();
            } else
            {
                Destroy(collider.gameObject);
            }
        }
        
        if (collider.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

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
            animator.SetTrigger("Jump_t");
            animator.SetFloat("Speed_f", 0.0f);
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

    public void RemoveEffects()
    {
        foreach (Transform obj in GameObject.Find("Duck").transform)
        {
            if (obj.name == "TejavaParticle" || obj.name == "BoostParticle" || obj.name == "BoostParticleRun"
                || obj.name == "FlyParticle" || obj.name == "DoubleParticle")
            {
                Destroy(obj.gameObject);
            }
        }
    }

    // -------------------------- Item --------------------------
    public void BoostOn()
    {
        itemBoost = true;
        initSpeed = forwardSpeed;
        SetSpeed(2.0f * forwardSpeed);
        animator.SetFloat("Speed_f", 20);
    }

    public void BoostOff()
    {
        itemBoost = false;
        SetSpeed(initSpeed);
        animator.SetFloat("Speed_f", 10);
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