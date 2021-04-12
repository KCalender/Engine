using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//클래스에 System.Serializable 이라는 어트리뷰트(Attribute)를 명시하면 Inspector 뷰에 노출된다(변수 public 쓴거랑 같은 의미)
[System.Serializable]
public class Anim
{
    public AnimationClip idle;
    public AnimationClip runForward;
    public AnimationClip runBackward;
    public AnimationClip runRight;
    public AnimationClip runLeft;
}


public class PlayerCtrl : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;

    //접근해야하는 컴포넌트는 반드시 변수에 할당한 후 사용
    private Transform tr;

    //이동속도 변수 public으로 선언 시 유니티 엔진에서 접근 가능
    public float moveSpeed = 10.0f;

    //회전 속도 변수
    public float rotSpeed = 100.0f;

    //inspector 뷰에 표시알 anim 변수
    public Anim anim;

    //아래에 있는 3D 모델의 Animation 컴포넌트에 접근하기 위한 변수
    public Animation _animation;

    public int hp = 100;

    private int initHp;
    public Image imgHpbar;

    //private GameMgr gameMgr;


    public delegate void PlayerDieHandler();
    public static event PlayerDieHandler OnPlayerDie;

    // Start is called before the first frame update
    void Start()
    {
        initHp = hp;
        //스크립트 처음에 Transform 컴포넌트 할당  중요 ★★★★★★★★★★★
        //------------ 변수에 컴포넌트를 할당하는것은 꽤 부하가 걸리는 작업이므로 최대한 Start or Awake 함수에서 사용할것
        tr = GetComponent<Transform>();

        //자신의 하위에 있는 Animation 컴포넌트를 찾아와 변수에 할당
        _animation = GetComponentInChildren<Animation>();
        

        //Animation 컴포넌트의 애니메이션 클립을 지정하고 실행
        _animation.clip = anim.idle;
        _animation.Play();

        //gameMgr = GameObject.Find("GameManager").GetComponent<GameMgr>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        //Debug.Log("H=" + h.ToString());
        //Debug.Log("V=" + v.ToString());

        //전후좌우 이동방향 벡터 계산
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        //Translate(이동 방향 * 속도 * 변위값 * Time.deltaTime(이전 프레임 부터 현재 프레임까지 걸린 시간), 기준좌표)
        //tr.Translate(Vector3.forward * moveSpeed * v * Time.deltaTime, Space.Self);
        //tr.Translate(moveDir * moveSpeed * Time.deltaTime, Space.Self);
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);

        // 정규화와 벡터의 크기 get
        //Debug.Log("Normalized =" + moveDir.normalized.ToString());
        //Debug.Log("Magnitude =" + moveDir.normalized.magnitude.ToString());

        tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));


        //키보드 입력값을 기준으로 동작할 애니메이션 수행
        if(v >= 0.1f)
        {
            _animation.CrossFade(anim.runForward.name, 0.3f);
        }
        else if (v <= -0.1f)
        {
            _animation.CrossFade(anim.runBackward.name, 0.3f);
        }
        else if (h >= 0.1f)
        {
            _animation.CrossFade(anim.runRight.name, 0.3f);
        }
        else if (h <= -0.1f)
        {
            _animation.CrossFade(anim.runLeft.name, 0.3f);
        }
        else
        {
            _animation.CrossFade(anim.idle.name, 0.3f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PUNCH")
        {
            hp -= 10;
            imgHpbar.fillAmount = (float)hp / (float)initHp;
            Debug.Log("Player HP = " + hp.ToString());

            if(hp<=0)
            {
                PlayerDie();
            }
        }
    }

    private void PlayerDie()
    {
        //Debug.Log("Player Die ! !");

        //GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");

        //foreach(GameObject monster in monsters)
        //{
        //    monster.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        //}
        OnPlayerDie();

        //gameMgr.isGameOver = true;
        GameMgr.instance.isGameOver = true;
    }
}
