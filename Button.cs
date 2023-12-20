using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public Toggle sesAcik;

    public AudioSource audioSource;
    public AudioClip buttonSound;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //durum PlayerPrefs'ini kontrol ediyoruz
        sesAcik.GetComponent<Toggle>();
        if (PlayerPrefs.HasKey("durum"))
        { 
        if(PlayerPrefs.GetInt("durum")==1)
            {
                sesAcik.isOn = true;
            }
            else
            {
                sesAcik.isOn = false;
            }
        }
        
    }
    private void Update()
    {
        AudioClose(sesAcik.isOn);
    }

    public void AudioClose(bool durum)
    {
        if (durum == true) 
        {
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("durum", 1);
        }
        else
        {
            PlayerPrefs.SetInt("durum", 0);
            AudioListener.volume = 0;
        }
    }
    public void Quit()
    {
        audioSource.clip = buttonSound;
        audioSource.Play();

        StartCoroutine(LoadNextSceneQuitAfterSound());

    }

    private IEnumerator LoadNextSceneQuitAfterSound()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Application.Quit();
    }



    public void LoadSceneSettings()
    {
        audioSource.clip = buttonSound;
        audioSource.Play();

        StartCoroutine(LoadNextSceneAfterSound());
    }

    // Clip uzunluðunu esas alýr ve ses bitince diger sahneye gecer.
    private IEnumerator LoadNextSceneAfterSound()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        SceneManager.LoadScene("Settings");
    }

    public void LoadNextScene()
    {
        // Aktif sahnenin indeksini al
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Bir sonraki sahnenin indeksini hesapla
        int nextSceneIndex = currentSceneIndex + 1;

        // Eðer bir sonraki sahne mevcutsa, ona geç
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Son sahnedesiniz, bir sonraki sahne yok.");
        }

        Time.timeScale = 1f;
    }


    public void SceneReset()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void LoadSceneHome()
    {
        SceneManager.LoadScene("Home");
    }

    public void LoadSceneLevels()
    {
        audioSource.clip = buttonSound;
        audioSource.Play();

        StartCoroutine(LoadNextSceneLevelsAfterSound());
    }

    private IEnumerator LoadNextSceneLevelsAfterSound()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        SceneManager.LoadScene("Level");
    }

    public void LoadSceneLevel(string sceneName)
    {
        audioSource.clip = buttonSound;
        audioSource.Play();

        StartCoroutine(LoadNextSceneLevelOneAfterSound());
    }

    private IEnumerator LoadNextSceneLevelOneAfterSound()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1f;
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("highScore");
    }

    public void Level1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 1");
    }
    public void Level2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 2");
    }
    public void Level3()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 3");
    }
    public void Level4()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 4");
    }

}
