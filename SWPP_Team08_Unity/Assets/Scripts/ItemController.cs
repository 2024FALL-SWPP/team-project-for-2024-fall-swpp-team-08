using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private PlayerController playerController;
    private ItemPlayerHandler itemPlayerHandler;
    private ItemUIHandler itemUIHandler;
    private ItemParticleHandler itemParticleHandler;
    private ItemTimeHandler itemTimeHandler;

    private Item itemDejava;
    private Item itemBoost;
    private Item itemThunder;
    private Item itemFly;
    private Item itemDouble;

    public GameObject[] timeCanvasPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        itemPlayerHandler = gameObject.AddComponent<ItemPlayerHandler>();
        itemUIHandler = gameObject.AddComponent<ItemUIHandler>();
        itemParticleHandler = gameObject.AddComponent<ItemParticleHandler>();
        itemTimeHandler = gameObject.AddComponent<ItemTimeHandler>();
        itemPlayerHandler.setNext(itemUIHandler).setNext(itemParticleHandler).setNext(itemTimeHandler);

        itemDejava = gameObject.AddComponent<ItemTejava>();
        itemBoost = gameObject.AddComponent<ItemBoost>();
        itemThunder = gameObject.AddComponent<ItemThunder>();
        itemFly = gameObject.AddComponent<ItemFly>();
        itemDouble = gameObject.AddComponent<ItemDouble>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Tejava":
                // 데자와 (점수)
                Destroy(other.gameObject);
                itemPlayerHandler.trigger(playerController, itemDejava, timeCanvasPrefabs);
                break;

            case "Item_Boost":
                // 오리부스트 (속도 1.5배, 장애물 무시 / Duration 3초)
                Destroy(other.gameObject);
                itemPlayerHandler.trigger(playerController, itemBoost, timeCanvasPrefabs);
                break;
            
            case "Item_Thunder":
                // 벼락치기 (가장 근접한 장애물 3개 삭제)
                Destroy(other.gameObject);
                itemPlayerHandler.trigger(playerController, itemThunder, timeCanvasPrefabs);
                break;

            case "Item_Fly":
                // 오리날다 (이단 점프 + 점프 중 좌우 컨트롤 / Duration 3초)
                Destroy(other.gameObject);
                itemPlayerHandler.trigger(playerController, itemFly, timeCanvasPrefabs);
                break;

            case "Item_Double":
                // 곱빼기 (점수 2배 / Duration 3초)
                Destroy(other.gameObject);
                itemPlayerHandler.trigger(playerController, itemDouble, timeCanvasPrefabs);
                break;
        }
    }
}
