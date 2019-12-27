using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogFollow : MonoBehaviour
{
    // Start is called before the first frame update
    Transform follow;
    Vector3 offset;

    public float moveSmooth = 0.001f;
    public float moveSpeed = 0.05f;
    int direction = 1;

    private Global global;
    void Start()
    {
        global = GameObject.Find("Global").GetComponent<Global>();
        follow = GameObject.Find("Camera").transform;
        offset = transform.position - follow.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, follow.position + offset, moveSmooth);
        if (global.isPaused)
            return;
  
        //控制云层来回移动
        offset += direction * new Vector3(moveSpeed, 0,moveSpeed);
        if(offset.x >= 35)
        {
            direction = -1;
        }
        if(offset.x <= -35)
        {
            direction = 1;
        }
        

    }
}
