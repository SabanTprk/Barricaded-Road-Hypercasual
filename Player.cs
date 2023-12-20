using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{

    public GameObject vectorback;

    public GameObject vectorforward;

    public bool isGround = true;

    public GameObject cam;

    public Rigidbody rb;

    public float jumpSpeed;

    private Touch touch;

    [Range(0f, 40f)]
    public float speedModifier;

    [Range(0f, 20f)]
    public float forwardSpeed;

    public int damage = 1;

    public GameObject LosePanel;

    public GameObject WinPanel;

    //Unity içinden yeni rengi belirlemek için
    public Color newColor;

    //Eski rengine geri dönmesi için
    public Color oldColor;

    //Rengini deðiþmesini istediðimiz nesne
    public GameObject targetObject;

    public float colorChangeDelay = 0.1f;





    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ChangeMaterial()
    {
        //Player'ýn renderer bileþenine ulaþmak için
        Renderer renderer = targetObject.GetComponent<Renderer>();

        if (renderer != null)
        {
            Material material = renderer.material;
            material.color = newColor;
            Invoke("ChangeColor", colorChangeDelay);
        }
    }

    void ChangeColor()
    {
        Renderer playerRenderer = targetObject.GetComponent<Renderer>();
        if (playerRenderer != null)
        {
            playerRenderer.material.color = oldColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isGround = true;
        }

        if(other.gameObject.tag == "dead")
        {
            FindObjectOfType<Hearts>().TakeDamage(damage);
        }

        if(other.gameObject.tag == "finish")
        {
            Time.timeScale = 0f;
            WinPanel.SetActive(true);

        }

        if (other.gameObject.tag == "lose")
        {
            Time.timeScale = 0f;
            LosePanel.SetActive(true);
            FindObjectOfType<Score>().loseSound();

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }

    public void OnJumpButtonClicked()
    {
        if (isGround)
        {
            Jump();
            FindObjectOfType<Score>().jumpSound();
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
    }   


    void Update()
    {

        //Telefona dokunduðumuzda top ve kamera hareketlenmesi için
        if (Variables.firsttouch == 1)
        {
            // Oyuncuya hýz veriyoruz Z ekseninde.
            transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);

            // Kameranýn oyuncu ile hýzlý ayný olmasý gerekli
            cam.transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);

            //Ön ve arka sýnýrlayýcý playerýmýzla ayný anda ilerleyecek.
            vectorback.transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
            vectorforward.transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
        }


        //Parmak ekrana dokundu > 0 artýk
        if (Input.touchCount > 0)
        {
            //Ýlk dokunan parmaðý baz alýyoruz.
            touch = Input.GetTouch(0);

            //Eðer ekrana 1 kere dokunulduysa firsttouch 1 olsun
            if (touch.phase == TouchPhase.Began)
            {
                Variables.firsttouch = 1;
            }

            //Began ilk dokunduðu anda fonksiyon çalýþýr
            //Ended Parmaðýný çekriðin anda fonksiyon çalýþýr.
            //Moved Parmaðý hareket ettiriyorken fonksiyon çalýþýr.
            //Stationary Kiþinin parmaðý deðiyor ama hareket etmiyorsa geçerli olan fonksiyon
            //Canceled Sistem parmaðý algýlamadýðýnda çalýþýr.



            //touch.phase dokunmatiði kontrol eder ve hareket varsa eþitler.
            else if (touch.phase == TouchPhase.Moved)
            {
                // Oyun sahnesindeki z ekseni telefon ekranýndaki ekranýndaki dokunacaðýmýz y eksenine
                // Oyun sahnesindeki x eksenini telefon ekranýndaki dokunacaðýmýz x eksenine eþitlememiz gerekli
                // Oyun içerisindeki Y pozisyonunu olduðu gibi býrakalým 
                // touch.deltaPosition nedir ? Dokunmatik ekranda dokunduðumuz ve sürüklediðimiz alan arasýndaki pozisyon farkýný ölçer ve farkýný alýr. 

                rb.velocity = new Vector3(touch.deltaPosition.x * speedModifier * Time.deltaTime,
                          rb.velocity.y,
                          0f);

            }

            //Elin telefondan çekildiði anda
            else if (touch.phase == TouchPhase.Ended)
            {
                //Parmaðýmýzý çektiðimiz anda hareketi sýfýrla.
                rb.velocity = Vector3.zero;
            }

        }
    }





    


}
