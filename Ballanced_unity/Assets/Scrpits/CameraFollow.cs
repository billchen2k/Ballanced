using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //// Start is called before the first frame update
    //public float distanceAway = 10;          // 摄像机距离跟随物体背后的距离
    //public float distanceUp = 2;            // 距离物体的最小距离
    //public float smooth = 3;                // 摄像机移动平滑指数
    ////public Transform follow;             //通过赋值取得物体（1-1）
    //private Vector3 targetPosition;     // the position the camera is trying to be in
    //Transform follow;

    public float smooth = 3;
    public float raise_height = 6;
    public float raise_smooth = 0.5f;
    public float raise_angle = 30f;
    public Transform follow; // 运行物体的Transforem
    private Vector3 offset; // 用来记录偏移量

    private bool isRaised = false;
    private Vector3 old_offset;
    private Quaternion old_rotation;
    private Quaternion rotationTarget;
    void Start()
    {
        follow = GameObject.Find("controller_ball").transform;
        offset = transform.position - follow.position;
        old_offset = offset;
        old_rotation = transform.rotation;
    }



    // Update is called once per frame
    void Update()
    {
        //设置视野中心是目标物体
        transform.position = Vector3.Lerp(transform.position, follow.position + offset, Time.deltaTime * smooth);
        if (Input.GetKey(KeyCode.Space))
        {
            if(isRaised == false)
            {
                isRaised = true;
                // 抬起摄像头
                offset.y += raise_height;
                rotationTarget = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(raise_angle, 0, 0));
                Debug.Log("R" + rotationTarget.ToString());
            }

        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            //恢复摄像头
            isRaised = false;
            offset = old_offset;
            transform.position = Vector3.Lerp(transform.position, follow.position + offset, Time.deltaTime * raise_smooth);
            rotationTarget = old_rotation;

        }
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, Time.deltaTime * raise_smooth);

        //float tiltAngle = 60f;
        //float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
        //float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

        //// Rotate the cube by converting the angles into a quaternion.
        //Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
        //// Dampen towards the target rotation
  
        ////transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }
}
