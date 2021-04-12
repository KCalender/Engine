using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    public enum MonsterState { idle, trace, attack, die, hit };

    public MonsterState monsterState = MonsterState.idle;

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent nvAgent; //AI 탭으로 들어가서 따로 using UnityEngine.AI; 선언 해줘야 사용가능

    private Animator animator;

    public float traceDist = 10.0f;
    public float attackDist = 2.0f;

    private bool isDie = false;

    public GameObject bloodEffect;
    public GameObject bloodDecal;


    private int hp = 100;


    //GameUI 접근용 변수
    private GameUI gameUI;

    // Start is called before the first frame update
    void Awake()
    {
        monsterTr = this.gameObject.GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();

        animator = this.gameObject.GetComponent<Animator>();


        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();
        //nvAgent.destination = playerTr.position;
        //nvAgent.SetDestination(playerTr.position);

    }

    private void OnEnable()
    {
        PlayerCtrl.OnPlayerDie += this.OnPlayerDie;
        StartCoroutine(this.CheckMonsterState());

        StartCoroutine(this.MonsterAction());
    }

    private void OnDisable()
    {
        PlayerCtrl.OnPlayerDie -= this.OnPlayerDie;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CheckMonsterState()
    {
        while(!isDie)
        {
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(playerTr.position, monsterTr.position);

            if (dist<=attackDist)
            {
                monsterState = MonsterState.attack;
            }
            else if (dist <= traceDist)
            {
                monsterState = MonsterState.trace;
            }
            else
            {
                monsterState = MonsterState.idle;
            }
        }
    }


    IEnumerator MonsterAction()
    {
        while(!isDie)
        {
            switch (monsterState)
            {
                case MonsterState.idle:
                    nvAgent.isStopped = true;
                    animator.SetBool("IsAttack", false);
                    animator.SetBool("IsTrace", false);
                    break;
                case MonsterState.trace:
                    nvAgent.destination = playerTr.position;
                    nvAgent.isStopped = false;

                    animator.SetBool("IsAttack", false);
                    animator.SetBool("IsTrace", true);

                    break;
                case MonsterState.attack:
                    animator.SetBool("IsAttack", true);
                    nvAgent.isStopped = true;
                    break;
                case MonsterState.hit:

                    nvAgent.isStopped = true;
                    break;
            }
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "BULLET")
        {
            CreateBloodEffect(collision.transform.position);

            hp -= collision.gameObject.GetComponent<BulletCtrl>().damage;
            if(hp<=0)
            {
                MonsterDie();
            }

            Destroy(collision.gameObject);

            animator.SetTrigger("IsHit");

            monsterState = MonsterState.hit;
        }
    }

    void MonsterDie()
    {
        gameObject.tag = "Untagged";
        
        StopAllCoroutines();

        isDie = true;
        monsterState = MonsterState.die;
        nvAgent.isStopped = true;
        animator.SetTrigger("IsDie");

        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;


        foreach(Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
        {
            coll.enabled = false;
        }

        gameUI.DispScore(50);

        StartCoroutine(this.PushObjectPool());
    }

    IEnumerator PushObjectPool()
    {
        yield return new WaitForSeconds(3.0f);

        isDie = false;
        hp = 100;
        gameObject.tag = "MONSTER";
        monsterState = MonsterState.idle;

        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = true;

        foreach(Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
        {
            coll.enabled = true;
        }

        gameObject.SetActive(false);
    }

    private void CreateBloodEffect(Vector3 pos)
    {
        GameObject blood1 = (GameObject)Instantiate(bloodEffect, pos, Quaternion.identity);
        Destroy(blood1, 2.0f);

        Vector3 decalPos = monsterTr.position + (Vector3.up * 0.05f);

        Quaternion decalRot = Quaternion.Euler(90, 0, Random.Range(0, 360));

        GameObject blood2 = (GameObject)Instantiate(bloodDecal, decalPos, decalRot);

        float scale = Random.Range(1.5f, 3.5f);
        blood2.transform.localScale = Vector3.one * scale;

        Destroy(blood2, 5.0f);
    }

    void OnPlayerDie()
    {
        StopAllCoroutines();

        nvAgent.isStopped = true;

        animator.SetTrigger("IsPlayerDie");
    }

    void OnDamage(object[] _params)
    {
        Debug.Log(string.Format("Hit ray {0} : {1}", _params[0], _params[1]));

        CreateBloodEffect((Vector3)_params[0]);

        hp -= (int)_params[1];

        if(hp<= 0)
        {
            MonsterDie();
        }

        animator.SetTrigger("IsHit");
    }
}
