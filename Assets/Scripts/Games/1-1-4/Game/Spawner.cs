using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] objectPrefabs; // 三种物体预制体
    public float spawnInterval = 1.3f;
    public bool isBack = false;
    
    void OnEnable() => StartCoroutine(SpawnRoutine());

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnRandomObject();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnRandomObject()
    {
        int index = Random.Range(0, objectPrefabs.Length);
        GameObject obj = Instantiate(objectPrefabs[index], transform.position, Quaternion.identity);
        obj.transform.SetParent(this.transform);
        if(isBack){
            obj.transform.localScale = new Vector3(1,1,1);
        }
    }
}