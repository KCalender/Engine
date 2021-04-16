using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DisplayUserId : MonoBehaviour
{
    public Text userId;
    private PhotonView pv = null;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        userId.text = pv.owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
