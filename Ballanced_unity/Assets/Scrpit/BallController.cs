using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rd;//刚体变量
    private Vector3 birthPosition;
    public int FORCE = 8;//力量
    public int DEATH_HEIGHT = 70;
    void Start()
    {
        rd = GetComponent<Rigidbody>();
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COIISION!");
    }
    void Awake()
    {
        birthPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        rd.AddForce(new Vector3(h, 0, v) * FORCE);
        //Debug.Log("H: " + h.ToString() + " " + "V: " + v.ToString());
        if (Input.GetKey(KeyCode.Space))
        {
            rd.AddForce(new Vector3(0, 10, 0));
        }
        if (transform.position.y <= 50)
        {
            //死亡
            Debug.Log("Dead.");
            transform.position = birthPosition;
            
        }
      
    }
}
