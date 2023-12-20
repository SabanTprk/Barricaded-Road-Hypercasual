using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameIntro : MonoBehaviour
{
    float time;


    private void Update()
    {
        timeNextScene();
    }
    void timeNextScene()
    {
        time +=1 * Time.deltaTime;
        if (time > 5)
        {
            SceneManager.LoadScene("Home");
        }
    }
}
