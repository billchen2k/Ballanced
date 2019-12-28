using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class WelcomeHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        playClipByName("Menu_click");
    }
    public void playClipByName(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sound/" + name);
        AudioSource audioFX = GameObject.Find("AudioFX").GetComponent<AudioSource>();
        audioFX.clip = clip;
        audioFX.Play();
    }
    public void StartGame()
    {
        playClipByName("Menu_load");
        GameObject.Find("btnStartText").GetComponent<TMP_Text>().text = "LOADING";
        StartCoroutine(StartAfterFX());
    }
    IEnumerator StartAfterFX()
    {
        yield return new WaitForSeconds(1f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("map");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void QuitGame()
    {
        playClipByName("Menu_click");
        StartCoroutine(QuitAfterFX());
    }
    IEnumerator QuitAfterFX()
    {
        yield return new WaitForSeconds(0.05f);
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space))
        {
            StartGame();
        }   
    }
}
