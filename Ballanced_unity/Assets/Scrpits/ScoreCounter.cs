using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class ScoreCounter : MonoBehaviour
{
    private Global global;
    private float timer = 0.5f;
    private TMP_Text textScore;

    // Start is called before the first frame update
    void Start()
    {
        global = GameObject.Find("Global").GetComponent<Global>();
        textScore = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = "SCORE  " + global.SCORE.ToString();
        if (global.isPaused || global.isDying)
            return;
        timer -= Time.deltaTime;
        if(global.SCORE == 0)
        {
            global.isPaused = true;
            global.isDying = true;
            global.GameOver();
        }
        if(timer < 0)
        {
            global.SCORE--;
            timer = 0.5f;
        }

    }

    public void AddScore(int addScore)
    {
        textScore.color = Color.green;
        StartCoroutine(LerpAdd(global.SCORE + addScore));
    }
    IEnumerator LerpAdd(int target)
    {
        while(global.SCORE <= target)
        {
            global.SCORE += 4;
            yield return new WaitForSeconds(0.01f);     //每 0.1s 加一次
        }
        textScore.color = Color.white;
        StopCoroutine(LerpAdd(target));
    }

    public void SubScore(int subScore)
    {
        textScore.color = Color.red;
        StartCoroutine(LerpSub(global.SCORE - subScore));
    }
    IEnumerator LerpSub(int target)
    {
        while (global.SCORE >= target)
        {
            if(global.SCORE <= 10)
            {
                global.SCORE = 0;
                StopCoroutine(LerpAdd(target));
            }
            else
            {
                global.SCORE -= 4;
            }
            yield return new WaitForSeconds(0.01f);     //每 0.01s 加一次
        }
        textScore.color = Color.white;
        StopCoroutine(LerpAdd(target));
    }
}
