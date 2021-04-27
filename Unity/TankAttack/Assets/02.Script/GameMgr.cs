using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    //접속된 플레이어 수를 표시할 Text UI 항목 변수
    public Text txtConnect;

    //접속 로그 표시 Text UI 항목 변수
    public Text txtLogMsg;
    //RPC 호출을 위한 PhotonView
    private PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();


        CreateTank();

        PhotonNetwork.isMessageQueueRunning = true;

        //룸에 입장 후 기존 접속자 정보를 출력
        GetConnectPlayerCount();
    }

    private IEnumerator Start()
    {
        //로그 메시지에 출력할 문자열 생성
        string msg = "\n<color=#00ff00>["
            + PhotonNetwork.player.NickName
            + "] Connected</color>";
        //RPC 호출
        pv.RPC("LogMsg", PhotonTargets.AllBuffered, msg);

        //룸에 있는 네트워크 객체간의 통신이 완료될때까지 잠시 대기
        yield return new WaitForSeconds(1.0f);
        SetConnectPlayerScore();
    }

    //모든 탱크의 스코어 UI에 점수를 표시하는 함수를 호출
    void SetConnectPlayerScore()
    {
        //현재 입장한 룸에 접속한 모든 네트워크 플레이어 정보를 저장
        PhotonPlayer[] players = PhotonNetwork.playerList;
        
        foreach(PhotonPlayer _player in players)
        {
            Debug.Log("[" + _player.ID + "]" + _player.NickName + " " + _player.GetScore() + " kill");
        }

        //모든 Tank프리팹을 배열에 저장
        GameObject[] tanks = GameObject.FindGameObjectsWithTag("TANK");

        foreach (GameObject tank in tanks)
        {
            //각 tank 별 스코어 조회
            int currKillCount = tank.GetComponent<PhotonView>().owner.GetScore();
            //해당 Tank의 UI에 스코어 표시
            tank.GetComponent<TankDamage>().txtKillCount.text = currKillCount.ToString();
        }

    }

    //룸 접속자 정보를 조회하는 함수
    void GetConnectPlayerCount()
    {
        //현재 입장한 룸에서 최대 입장 가능 수와 현재 접속자 수를 문자열로 구성
        Room currRoom = PhotonNetwork.room;
        txtConnect.text = currRoom.PlayerCount.ToString() + "/"
            + currRoom.MaxPlayers.ToString();
    }

    //네트워크 플레이어가 접속했을때 호출되는 함수
    void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        GetConnectPlayerCount();
    }

    //네트워크 플레이어가 룸을 나가거나 접속이 끊어졌을때 호출되는 함수
    void OnPhotonPlayerDisconnected(PhotonPlayer outPlayer)
    {
        GetConnectPlayerCount();
    }

    void CreateTank()
    {
        float pos = Random.Range(-100.0f, 100.0f);
        PhotonNetwork.Instantiate("Tank", new Vector3(pos, 20.0f, pos), Quaternion.identity, 0);
    }

    //룸 나가기 버튼 클릭 이벤트 연결 함수
    public void OnClickExitRoom()
    {
        //로그 메시지에 출력할 문자열 생성
        string msg = "\n<color=#ff0000>["
            + PhotonNetwork.player.NickName
            + "] Disconnected</color>";
        //RPC 호출
        pv.RPC("LogMsg", PhotonTargets.AllBuffered, msg);

        PhotonNetwork.LeaveRoom();
    }

    //룸에서 접속 종료 딨을때 호출되는 콜백함수
    void OnLeftRoom()
    {
        SceneManager.LoadScene("scLobby");
    }

    [PunRPC]
    void LogMsg(string msg)
    {
        //로그 메시지 Text UI 에 누적시켜서 표시
        txtLogMsg.text = txtLogMsg.text + msg;
    }
}
