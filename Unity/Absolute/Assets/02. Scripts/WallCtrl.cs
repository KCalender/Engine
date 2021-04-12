using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCtrl : MonoBehaviour
{
    public GameObject sparkEffect;
    
    // OnCollisionEnter is called when the crash starts
    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.collider.tag == "BULLET")
        {
            GameObject spark = (GameObject)Instantiate(sparkEffect, collision.transform.position, Quaternion.identity);
            //Instantiate(sparkEffect, collision.transform.position, Quaternion.identity);
            Destroy(spark, spark.GetComponent<ParticleSystem>().main.duration + 0.2f); /*spark.GetComponent<ParticleSystem>().duration*/ // 좌측의 ParticleSystem.Duration은 사용하지 않고, 대신 ParticleSystem.main.duration으로 대체

            Destroy(collision.gameObject);
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
