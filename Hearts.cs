using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    [SerializeField]
    private GameObject[] hearts;

    private int lifeAmount;

    private bool isDead;

    private void Start()
    {
        lifeAmount = hearts.Length;
    }

    private void Update()
    {
        if (!isDead)
        {
            
        }
    }


    public void TakeDamage(int damageAmount)
    {
        lifeAmount -= damageAmount;
        Destroy(hearts[lifeAmount].gameObject);

        if (lifeAmount < 1) 
        {
            isDead = true;
            FindObjectOfType<Player>().LosePanel.SetActive(true);
            Time.timeScale = 0f;
            FindObjectOfType<Score>().loseSound();
        }
    }


}
