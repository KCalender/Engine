using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnCtrl : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float spawnTime;

    public Transform[] wayPoints;

    public int spawnCount = 30;

    private void Awake()
    {
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        while (spawnCount==0 ? false : true)
        {
            spawnCount--;
            Debug.Log(spawnCount);
            GameObject clone = Instantiate(enemyPrefab);
            Monster monster = clone.GetComponent<Monster>();
            
            monster.Setup(wayPoints);
            yield return new WaitForSeconds(spawnTime);
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
