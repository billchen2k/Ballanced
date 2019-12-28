using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuHandler : MonoBehaviour
{
    private Global global;

    private GameObject panelOverley;
    private GameObject btnQuit;
    public void RestartGame()
    {
        global.playClipByName("Menu_click");
        Time.timeScale = 1f;
        SceneManager.LoadScene("map");
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        global.playClipByName("Menu_click");
        StartCoroutine(QuitAfterFX());
    }
    IEnumerator QuitAfterFX()
    {
        yield return new WaitForSeconds(0.05f);
        Application.Quit();
    }
    public void PauseGame()
    {
        global.playClipByName("Menu_click");
        if (global.isPaused)
        {
            /* 恢复过程 */
            global.isPaused = false;
            GameObject.Find("btnPauseText").GetComponent<TMP_Text>().text = "PAUSE";
            Time.timeScale = 1f;
            panelOverley.SetActive(false);
            btnQuit.SetActive(false);
        }
        else
        {
            /* 暂停过程 */
            StartCoroutine(PauseAfterFX());
        }
        

    }
    IEnumerator PauseAfterFX()
    {
        yield return new WaitForSeconds(0.05f);
        global.isPaused = true;
        GameObject.Find("btnPauseText").GetComponent<TMP_Text>().text = "CONTINUE";
        GameObject.Find("AudioFX").GetComponent<AudioSource>().Stop();
        panelOverley.SetActive(true);
        btnQuit.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Paused");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        global = GameObject.Find("Global").GetComponent<Global>();
        panelOverley = GameObject.Find("PanelOverley");
        panelOverley.SetActive(false);
        btnQuit = GameObject.Find("btnQuit");
        btnQuit.SetActive(false);
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
        if (Input.GetKeyUp(KeyCode.Q))
        {
            QuitGame();
        }
    }
}
