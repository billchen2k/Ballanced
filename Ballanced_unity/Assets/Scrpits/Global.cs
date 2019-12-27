using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    // Start is called before the first frame update
    public int SCORE = 999;
    public bool isPaused = false;
    public bool isDying = false;

    public Vector3 originBirthPlace;
    void Start()
    {
        SCORE = 999;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        Debug.Log("GameOver!");
        isDying = true;
    }

    public void Restart()
    {

    }
    public void GameWin()
    {
        Debug.Log("WIN SCORE:" + SCORE);
    }
    public void playClipByName(AudioSource audioSource , string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sound/" + name);
        audioSource.clip = clip;
        audioSource.Play();
    }
}
