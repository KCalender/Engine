﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankDamage : MonoBehaviour
{
    //탱크 폭파 후 투명처리를 위한 meshRenderer 컴포넌트 배열
    private MeshRenderer[] renderers;

    //탱크 폭발 효과 프리팹을 연결할 변수
    private GameObject expEffect = null;
    //탱크 생명치
    private int initHp = 100;
    private int currHp = 0;

    //UI 연결용
    public Canvas hudCanvas;
    public Image hpBar;

    private void Awake()
    {
        //탱크 모델의 모든 MeshRenderer 컴포넌트를 추출한 후 배열에 할당
        renderers = GetComponentsInChildren<MeshRenderer>();

        //현재 생명치를 초기 생명치로 초기값 설정
        currHp = initHp;

        //탱크 폭발 시 생성시킬 폭발 효과를 로드
        expEffect = Resources.Load<GameObject>("Large Explosion");

        hpBar.color = Color.green;
    }

    private void OnTriggerEnter(Collider other)
    {
        //충돌한 Collider의 태그 비교
        if (currHp > 0 && other.tag == "CANNON")
        {
            currHp -= 20;

            hpBar.fillAmount = (float)currHp / (float)initHp;

            if (hpBar.fillAmount <= 0.4f)
                hpBar.color = Color.red;
            else if (hpBar.fillAmount <= 0.6f)
                hpBar.color = Color.yellow;

            if(currHp <= 0)
            {
                StartCoroutine(this.ExplosionTank());
            }
        }
    }


    IEnumerator ExplosionTank()
    {
        //폭발 효과 생성
        Object effect = GameObject.Instantiate(expEffect, transform.position, Quaternion.identity);

        Destroy(effect, 3.0f);

        //hud 비활성화
        hudCanvas.enabled = false;
        
        //탱크 투명 처리
        SetTankVisible(false);
        //3초 기다렸다가 활성화
        yield return new WaitForSeconds(3.0f);

        //hud 초기화
        hpBar.fillAmount = 1.0f;
        hpBar.color = Color.green;
        hudCanvas.enabled = true;

        currHp = initHp;
        SetTankVisible(true);
    }

    void SetTankVisible(bool isVisible)
    {
        foreach(Renderer renderer in renderers)
        {
            renderer.enabled = isVisible;
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
