using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioBGM;
    AudioClip[] BGMs;

    private float interval = 3;
    public bool ifNoBGM = false;

    private void playSong(int toPlay)
    {
        audioBGM.clip = BGMs[toPlay];
        audioBGM.Play();
    }
    void Start()
    {
        audioBGM = this.GetComponent<AudioSource>();   
        BGMs = new AudioClip[] { Resources.Load<AudioClip>("Sound/BGM/Music_Theme_3_1"), Resources.Load<AudioClip>("Sound/BGM/Music_Theme_3_2"), Resources.Load<AudioClip>("Sound/BGM/Music_Theme_3_3") };
        
    }

    public void StopPlayingOrigin()
    {
        audioBGM.Stop();
        ifNoBGM = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (ifNoBGM)
            return;
        if (audioBGM.isPlaying && interval < 0.1)
        {
            interval = Random.Range(10f, 60f);
            Debug.Log("Next interval:" + interval.ToString());
        }
        if (!audioBGM.isPlaying)
        {
            interval -= Time.deltaTime;
            if(interval < 0.1)
            {
                playSong((int)(Random.value * 3));
            }
        }
        
    }
}
