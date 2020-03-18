using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMove : MonoBehaviour
{
    public Vector3 startPos;

    void Start()
    {
        startPos=transform.position;
        transform.position=new Vector3(Random.value*8-4,Random.value*4-2,0);
    }
    // Update is called once per frame
    void Update()
    {
    }

}
