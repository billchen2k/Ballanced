using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BallController : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rd;//刚体变量
    private Vector3 birthPosition;
    public int FORCE = 8;//力量
    public int DEATH_HEIGHT = 70;
    public int BONES_VALUE = 200;
    public int DEATH_COST = 300;
    private Global global;
    private AudioSource audioCollision;
    private AudioSource audioScroll;
    private AudioSource audioFX;
    void Start()
    {
        rd = GetComponent<Rigidbody>();
        audioCollision = GameObject.Find("AudioCollision").GetComponent<AudioSource>();
        audioScroll = GameObject.Find("AudioScroll").GetComponent<AudioSource>();
        audioFX = GameObject.Find("AudioFX").GetComponent<AudioSource>();
        global = GameObject.Find("Global").GetComponent<Global>();
        global.originBirthPlace = birthPosition;
    }

    //分数球
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BonesBall")
        {
            global.playClipByName(audioFX, "Extra_Start");
            GameObject.Find("TextScore").GetComponent<ScoreCounter>().AddScore(BONES_VALUE);
            ParticleSystem bps = other.gameObject.transform.Find("BonesParticle").GetComponent<ParticleSystem>();
            bps.Play();
            other.gameObject.transform.Find("Fire").GetComponent<ParticleSystem>().Stop();
            other.gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COIISION!");
        var clipCollision = Resources.Load<AudioClip>("Sound/Hit_Wood_Stone");
        audioCollision.volume = collision.relativeVelocity.magnitude / 6;
        audioCollision.clip = clipCollision;
        audioCollision.Play();
       
    }
    void OnCollisionStay(Collision collisionInfo)
    {
        // Debug-draw all contact points and normals
        // Handle Rolling
        if(rd.velocity.magnitude >= 0.5)
        {
            audioScroll.volume = Mathf.Lerp(0.0f, 0.9f, (rd.velocity.magnitude - 0.5f) / 1.8f);
            if (!audioScroll.isPlaying)
            {
                audioScroll.Play();
            }
            
        }
        else
        {
            audioScroll.Stop();
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        audioScroll.Stop();
    }
    void Awake()
    {
        birthPosition = transform.position;
    }

    void DieHandler()
    {
        //死亡
        global.playClipByName(audioFX, "Misc_Fall");
        if(global.SCORE <= DEATH_COST)
        {
            GameObject.Find("TextScore").GetComponent<ScoreCounter>().SubScore(global.SCORE);
            global.GameOver();
        }
        else
        {
            GameObject.Find("TextScore").GetComponent<ScoreCounter>().SubScore(DEATH_COST);
            transform.position = birthPosition;
            global.isDying = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (global.isPaused)
            return;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        rd.AddForce(new Vector3(h, 0, v) * FORCE);

        // J键调试跳跃
        if (Input.GetKey(KeyCode.J))
        {
            rd.AddForce(new Vector3(0, 50, 0));
            GameObject.Find("TextScore").GetComponent<ScoreCounter>().AddScore(BONES_VALUE);

        }
        if (transform.position.y <= 50)
        {
            if (!global.isDying)
            {
                global.isDying = true;
                DieHandler();
            }
                
          
        }
    }
}
