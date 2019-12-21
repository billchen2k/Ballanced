using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    //// Start is called before the first frame update
    //public float distanceAway = 10;          // 摄像机距离跟随物体背后的距离
    //public float distanceUp = 2;            // 距离物体的最小距离
    //public float smooth = 3;                // 摄像机移动平滑指数
    ////public Transform follow;             //通过赋值取得物体（1-1）
    //private Vector3 targetPosition;     // the position the camera is trying to be in
    //Transform follow;

    public float smooth = 3;
    public Transform follow; // 运行物体的Transforem
    public Vector3 offset; // 用来记录偏移量
    void Start()
    {
        follow = GameObject.Find("controller_ball").transform;
        offset = transform.position - follow.position;

    }



    // Update is called once per frame
    void Update()
    {
        // 设置追踪目标的坐标作为调整摄像机的偏移量
        //targetPosition = follow.position + Vector3.up * distanceUp - follow.forward * distanceAway;

        // 在摄像机和被追踪物体之间制造一个顺滑的变化
        //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);

        //设置视野中心是目标物体
        transform.position = Vector3.Lerp(transform.position, follow.position + offset, Time.deltaTime * smooth);
    }
}
