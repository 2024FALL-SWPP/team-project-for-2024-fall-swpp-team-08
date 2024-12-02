using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    public GameObject itemThunderEffectPrefab;
    public GameObject thunderParticlePrefab;
    public Light directionalLight;

    // Start is called before the first frame update
    void Start()
    {
        directionalLight = GameObject.Find("Directional Light").GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnThunderEffect(GameObject[] closestObstacles)
    {
        directionalLight.intensity = 0.5f;
        StartCoroutine(DestroyThunderEffect(closestObstacles));
    }
    
    private IEnumerator DestroyThunderEffect(GameObject[] closestObstacles)
    {
        yield return new WaitForSeconds(0.1f);

        directionalLight.intensity = 2.0f;
        List<GameObject> thunderEffectList = new List<GameObject>();
        foreach (GameObject obstacle in closestObstacles)
        {
            Vector3 position = obstacle.transform.position;
            thunderEffectList.Add(Instantiate(itemThunderEffectPrefab, new Vector3(position.x + 1.0f, 18.0f, position.z), Quaternion.Euler(-90, 0, 0)));
            Destroy(obstacle);
        }
        yield return new WaitForSeconds(0.4f);

        directionalLight.intensity = 0.5f;
        foreach (GameObject thunderEffect in thunderEffectList)
        {
            Vector3 position = thunderEffect.transform.position;
            GameObject particleObject = Instantiate(thunderParticlePrefab, new Vector3(position.x, 2.0f, position.z), Quaternion.Euler(0, 0, 0));
            particleObject.GetComponent<ParticleSystem>().Play();
            Destroy(thunderEffect);
        }
        yield return new WaitForSeconds(0.1f);

        directionalLight.intensity = 1.0f;
    }
}