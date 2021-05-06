using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionWaveCtrl : MonoBehaviour
{
    public GameObject Minion;
    public Transform StopoverNode, NexusNode;
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

    IEnumerator MinionWave()
    {
        while (true)
        {
            int n = 3;
            while (n != 0)
            {
                Minion.GetComponent<Minion>().stopoverNode = StopoverNode;
                Minion.GetComponent<Minion>().nexusNode = NexusNode;
                GameObject NewMinion = Instantiate(Minion, this.transform.position, this.transform.rotation);
                //몬스터의 생성 주기 시간만큼 대기
                yield return new WaitForSeconds(WaveTime);
            }
            
            yield return new WaitForSeconds(10.0f);
        }
    }

}
