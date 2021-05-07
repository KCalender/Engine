using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionWaveCtrl : MonoBehaviour
{
    public GameObject Minion;
    public Transform[] Node;
    public float WaveTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MinionWave());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// 미니언 웨이브 생성 coroutine
    /// 이중 coroutine으로 반복 생성
    /// </summary>
    IEnumerator MinionWave()
    {
        while (true)
        {
            StartCoroutine(CreateMinion());
            
            yield return new WaitForSeconds(30.0f);
        }
    }

    IEnumerator CreateMinion()
    {
        int n = 6;
        while (n-- != 0)
        {
            GameObject Clone = Instantiate(Minion, this.transform.position, this.transform.rotation);

            Debug.Log(this.transform.position);

            Debug.Log(Clone.transform.position);
            //Minion NewMinion = Clone.GetComponent<Minion>();

            //NewMinion.Setup(Node);
            //몬스터의 생성 주기 시간만큼 대기
            yield return new WaitForSeconds(WaveTime);
        }
    }

}
