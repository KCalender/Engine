using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannon : MonoBehaviour
{
    //cannon 프리팹을 연결할 변수
    public GameObject cannon = null;
    //포탄 발사 사운드 파일
    public AudioClip fireSfx = null;
    //AudioSource 컴포넌트를 할당할 변수
    private AudioSource sfx = null;
    //cannon 발사 지점
    public Transform firePos;

    private PhotonView pv = null;

    private void Awake()
    {
        //cannon 프리팹을 Resources 폴더에서 불러와 변수에 할당
        cannon = (GameObject)Resources.Load("Cannon");
        //포탄 발사 사운드 파일을 Resources 폴더에서 불러와 변수에 할당
        fireSfx = (AudioClip)Resources.Load("CannonFire");
        //AudioSource 컴포넌트 할당
        sfx = GetComponent<AudioSource>();

        pv = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MouseHover.instance.isUIHover) return;
        
        if (pv.isMine && Input.GetMouseButtonDown(0))
        {
            Fire();

            pv.RPC("Fire", PhotonTargets.Others, null);
        }
    }

    [PunRPC]
    void Fire()
    {
        sfx.PlayOneShot(fireSfx, 1.0f);
        GameObject _cannon = (GameObject)Instantiate(cannon, firePos.position, firePos.rotation);
        _cannon.GetComponent<Cannon>().playerId = pv.ownerId;
    }
}
