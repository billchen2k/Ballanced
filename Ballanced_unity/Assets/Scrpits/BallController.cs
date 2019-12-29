using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BallController : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rd;//刚体变量
    private Vector3 birthPosition;
    public float FORCE = 8;//力量
    public int DEATH_HEIGHT = 60;
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
        global.playClipByName("Misc_StartLevel");
    }

    //分数球
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "BonesBall":
               //分数球
                global.playClipByName(audioFX, "Extra_Start");
                GameObject.Find("TextScore").GetComponent<ScoreCounter>().AddScore(BONES_VALUE);
                ParticleSystem bps = other.gameObject.transform.Find("BonesParticle").GetComponent<ParticleSystem>();
                bps.Play();
                other.gameObject.transform.Find("Fire").GetComponent<ParticleSystem>().Stop();
                other.gameObject.GetComponent<SphereCollider>().enabled = false;
                break;
            case "CheckPoint":
                //检查点
                if(other.name == "EndCheckPoint")
                {
                    global.playClipByName(GameObject.Find("AudioBGM").GetComponent<AudioSource>(), "Music_EndCheckpoint");
                }
                global.playClipByName(audioFX, "Misc_Checkpoint");
                birthPosition = other.gameObject.transform.position;
                other.gameObject.GetComponent<SphereCollider>().enabled = false;
                foreach (ParticleSystem one in other.gameObject.GetComponentsInChildren<ParticleSystem>())
                {
                    one.Play();
                }
                foreach(Light one in other.gameObject.GetComponentsInChildren<Light>(true))
                {
                    one.enabled = true;
                }
                break;
            case "WinningPoint":
                other.gameObject.GetComponent<SphereCollider>().enabled = false;
                GameObject.Find("AudioWinningBGM").GetComponent<AudioSource>().Stop();
                global.isWinning = true;
                global.GameWin();
                break;
            default:
                break;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COIISION!");
        var clipCollision = Resources.Load<AudioClip>("Sound/Hit_Wood_Stone");
        audioCollision.volume = collision.relativeVelocity.magnitude / 6;
        switch (collision.gameObject.tag)
        {
            case "WoodObject":
                global.playClipByName(audioCollision, "Hit_Wood_Wood");
                break;
            case "MetalObject":
                global.playClipByName(audioCollision, "Hit_Wood_Metal");
                break;
            case "PaperObject":
                global.playClipByName(audioCollision, "Hit_Paper");
                break;
            default:
                global.playClipByName(audioCollision, "Hit_Wood_Stone");
                break;
        }
        /* 用于调试胜利界面 */
        if (collision.gameObject.name == "SphereWIN")
        {
            global.GameWin();
        }

    }
    void OnCollisionStay(Collision collisionInfo)
    {
        // Debug-draw all contact points and normals
        // Handle Rolling
        if (rd.velocity.magnitude >= 0.5)
        {
            audioScroll.volume = Mathf.Lerp(0.0f, 0.9f, (rd.velocity.magnitude - 0.5f) / 1.8f);
            if (!audioScroll.isPlaying)
            {
                switch (collisionInfo.gameObject.tag)
                {
                    case "MetalObject":
                        audioScroll.clip = Resources.Load<AudioClip>("Sound/Roll_Wood_Metal");
                        break;
                    case "WoodObject":
                        audioScroll.clip = Resources.Load<AudioClip>("Sound/Roll_Wood_Wood");
                        break;
                    default:
                        audioScroll.clip = Resources.Load<AudioClip>("Sound/Roll_Wood_Stone");
                        break;
                }
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
        if (global.SCORE <= DEATH_COST)
        {
            GameObject.Find("TextScore").GetComponent<ScoreCounter>().SubScore(global.SCORE);
            global.GameOver();
        }
        else
        {
            GameObject.Find("TextScore").GetComponent<ScoreCounter>().SubScore(DEATH_COST);
            StartCoroutine(RebornAfterFX());

        }
    }
    IEnumerator RebornAfterFX()
    {
        yield return new WaitForSeconds(1f);
        transform.position = birthPosition;
        global.isDying = false;

    }
    // Update is called once per frame
    void Update()
    {
        if (global.isPaused)
        {
            rd.Sleep();
            return;
        }
        if (global.isWinning)
        {
            return;
        }
        /*控制运动*/
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        rd.AddForce(new Vector3(h, 0, v) * FORCE);
        // J键调试跳跃
        if (Input.GetKey(KeyCode.J))
        {
            rd.AddForce(new Vector3(0, 50, 0));
            //GameObject.Find("TextScore").GetComponent<ScoreCounter>().AddScore(BONES_VALUE);

        }
        /* 死亡处理 */
        if (transform.position.y <= DEATH_HEIGHT)
        {
            if (!global.isDying)
            {
                global.isDying = true;
                DieHandler();
            }
        }

        /* 一键胜利 */
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W))
        {
            global.GameWin();
        }


        /* 一键死亡 */
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.D))
        {
            global.GameOver();
        }
        
    }
}
