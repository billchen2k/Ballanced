using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Global : MonoBehaviour
{
    // Start is called before the first frame update
    public int INIT_SCORE = 999;
    public int SCORE = 999;
    public bool isPaused = false;
    public bool isDying = false;
    public bool isWinning = false;

    public Vector3 originBirthPlace;
    GameObject PanelLose;
    GameObject PanelWin;
    GameObject PanelGaming;
    GameObject[] toHideAtWin;

    private bool isWaitingForRestart = false;
    private int temp = 0;
    void Start()
    {
        SCORE = INIT_SCORE;
        PanelLose = GameObject.Find("PanelLose");
        PanelWin = GameObject.Find("PanelWin");
        PanelGaming = GameObject.Find("PanelGaming");
        toHideAtWin = new GameObject[] { GameObject.Find("btnRestartWin"), GameObject.Find("btnQuitWin"), GameObject.Find("TextWinConfirm") };
        PanelWin.SetActive(false);
        PanelLose.SetActive(false);
        
    }


    // Update is called once per frame
    void Update()
    {
        if (isWaitingForRestart)
        {
            if(Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return))
            {
                playClipByName("Menu_click");
                Time.timeScale = 1f;
                SceneManager.LoadScene("map");
            }
        }
    }

    public void GameOver()
    {
        Debug.Log("GameOver!");
        StartCoroutine(IEGameOver());
 
    }
    IEnumerator IEGameOver()
    {
        yield return new WaitForSeconds(2);
        isDying = true;
        isPaused = true;
        PanelLose.SetActive(true);//注意等待时间的写法
        isWaitingForRestart = true;
    }

    public void GameWin()
    {
        // 防止球死亡
        GameObject.Find("controller_ball").GetComponent<BallController>().DEATH_HEIGHT = -1000;
        Debug.Log("WIN SCORE:" + SCORE);
        AudioSource audioBGM = GameObject.Find("AudioBGM").GetComponent<AudioSource>();
        GameObject.Find("AudioScroll").GetComponent<AudioSource>().Stop();
        GameObject.Find("AudioBGM").GetComponent<BGMController>().StopPlayingOrigin();
        playClipByName(audioBGM, "Music_Final");
        PanelGaming.SetActive(false);
        StartCoroutine(IEGameWin());
    }
    IEnumerator IEGameWin()
    {
        yield return new WaitForSeconds(3f);
        isWinning = true;
        PanelWin.SetActive(true);
        foreach (GameObject one in toHideAtWin)
        {
            one.SetActive(false);
        }
        StartCoroutine(LerpScore(SCORE));
    }
    IEnumerator LerpScore(int target)
    {
        AudioSource audioFX = GameObject.Find("AudioFX").GetComponent<AudioSource>();
        audioFX.loop = true;
        playClipByName(audioFX, "Menu_counter");
        while(temp <= target - 4)
        {
            temp += 4;    
            GameObject.Find("TextWinScore").GetComponent<TMP_Text>().text = "SCORE: " + temp.ToString();
            yield return new WaitForSeconds(0.01f);
        }
        GameObject.Find("TextWinScore").GetComponent<TMP_Text>().text = "SCORE: " + target.ToString();
        audioFX.Stop();
        audioFX.loop = false;
        yield return new WaitForSeconds(2f);
        foreach (GameObject one in toHideAtWin)
        {
            one.SetActive(true);
        }
        playClipByName("Music_Highscore");
        isWaitingForRestart = true;
        isPaused = true;
        StopCoroutine(LerpScore(target));

    }
    public void playClipByName(AudioSource audioSource , string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sound/" + name);
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void playClipByName(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sound/" + name);
        AudioSource audioFX = GameObject.Find("AudioFX").GetComponent<AudioSource>();
        audioFX.clip = clip;
        audioFX.Play();
    }
}
