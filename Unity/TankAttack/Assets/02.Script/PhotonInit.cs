using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotonInit : MonoBehaviour
{
    //현재 제작한 App의 버전 정보
    public string version = "v1.0";

    public InputField userId;

    private void Awake()
    {
        //포톤 클라우드에 접속
        PhotonNetwork.ConnectUsingSettings(version);
    }

    //포톤 클라우드에 정상적으로 접속한 후 로비에 입장하면 호출되는 콜백 함수
    void OnJoinedLobby()
    {
        Debug.Log("Entered Lobby !");
        userId.text = GetUserId();
        //PhotonNetwork.JoinRandomRoom();//무작위 룸 접속
    }

    string GetUserId()
    {
        string userId = PlayerPrefs.GetString("USER_ID");

        if(string.IsNullOrEmpty(userId))
        {
            userId = "USER_" + Random.Range(0, 999).ToString("000");
        }
        return userId;
    }

    public void OnClickJoinRandomRoom()
    {
        //로컬 유저 이름 설정
        //PhotonNetwork.player.name = userId.text; 사용하지 않음
        PhotonNetwork.player.NickName = userId.text;

        PlayerPrefs.SetString("USER_ID", userId.text);

        PhotonNetwork.JoinRandomRoom();
    }

    //무작위 룸 접속에 실패한 경우 호출되는 콜백 함수
    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("No Rooms !");

        //룸 생성
        PhotonNetwork.CreateRoom("MyRoom");
    }

    //룸에 입장하면 호출되는 콜백 함수
    void OnJoinedRoom()
    {
        Debug.Log("Enter Room");

        // CreateTank();
        StartCoroutine(this.LoadBattleField());
    }

    //룸 씬으로 이동하는 코루틴 함수
    IEnumerator LoadBattleField()
    {
        //씬을 이동하는동안 포톤 클라우드 서버로부터 네트워크 메시지 수신 중단
        PhotonNetwork.isMessageQueueRunning = false;
        //백그라운드로 씬 로딩
        AsyncOperation ao = SceneManager.LoadSceneAsync("scBattleField");
        yield return ao;
    }

    //탱크를 생성하는 함수
    void CreateTank()
    {
        float pos = Random.Range(-100.0f, 100.0f);
        PhotonNetwork.Instantiate("Tank", new Vector3(pos, 20.0f, pos), Quaternion.identity, 0);
    }

    private void OnGUI()
    {
        //화면 좌측 상단에 접속 과정에 대한 로그 출력
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
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
