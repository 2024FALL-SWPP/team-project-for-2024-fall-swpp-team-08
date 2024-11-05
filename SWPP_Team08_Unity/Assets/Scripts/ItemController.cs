using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private UIManager uiManager;
    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Tejava"))
        {
            Debug.Log("aa");
            Destroy(other.gameObject);
            score++;
            uiManager.UpdateScoreText(score);
        }
    }

    public int getScore() 
    {
        return score;
    }
}
