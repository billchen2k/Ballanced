using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuHandler : MonoBehaviour
{
    private Global global;

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("map");
    }
    public void PauseGame()
    {
        if (global.isPaused)
        {
            global.isPaused = false;
            GameObject.Find("btnPauseText").GetComponent<TMP_Text>().text = "PAUSE";    
            Time.timeScale = 1f;
        }
        else
        {
            
            global.isPaused = true;
            GameObject.Find("btnPauseText").GetComponent<TMP_Text>().text = "PAUSED";
            GameObject.Find("AudioFX").GetComponent<AudioSource>().Stop();
            Time.timeScale = 0f;
            Debug.Log("Paused");
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        global = GameObject.Find("Global").GetComponent<Global>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.P))
        {
            PauseGame();
        }
        if (Input.GetKeyUp(KeyCode.R) || Input.GetKeyUp(KeyCode.K))
        {
            RestartGame();
        }
    }
}
