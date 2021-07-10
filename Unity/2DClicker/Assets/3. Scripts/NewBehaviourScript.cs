using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public List<(int, int, int, int, int)> a = new List<(int, int, int, int, int)>();
    // Start is called before the first frame update
    void Start()
    {
        a.Add((1, 2, 3, 4, 5));
        a.Add((6, 7, 8, 9, 10));
        a.Add((11, 12, 13, 14, 15));
        a.Add((16, 17, 18, 19, 20));
        a.Add((21, 22, 23, 24, 25));
        a.Add((26, 27, 28, 29, 30));
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(a[0].Item3);
    }
}
