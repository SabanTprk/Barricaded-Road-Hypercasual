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

    //Unity i�inden yeni rengi belirlemek i�in
    public Color newColor;

    //Eski rengine geri d�nmesi i�in
    public Color oldColor;

    //Rengini de�i�mesini istedi�imiz nesne
    public GameObject targetObject;

    public float colorChangeDelay = 0.1f;





    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ChangeMaterial()
    {
        //Player'�n renderer bile�enine ula�mak i�in
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

        //Telefona dokundu�umuzda top ve kamera hareketlenmesi i�in
        if (Variables.firsttouch == 1)
        {
            // Oyuncuya h�z veriyoruz Z ekseninde.
            transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);

            // Kameran�n oyuncu ile h�zl� ayn� olmas� gerekli
            cam.transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);

            //�n ve arka s�n�rlay�c� player�m�zla ayn� anda ilerleyecek.
            vectorback.transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
            vectorforward.transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
        }


        //Parmak ekrana dokundu > 0 art�k
        if (Input.touchCount > 0)
        {
            //�lk dokunan parma�� baz al�yoruz.
            touch = Input.GetTouch(0);

            //E�er ekrana 1 kere dokunulduysa firsttouch 1 olsun
            if (touch.phase == TouchPhase.Began)
            {
                Variables.firsttouch = 1;
            }

            //Began ilk dokundu�u anda fonksiyon �al���r
            //Ended Parma��n� �ekri�in anda fonksiyon �al���r.
            //Moved Parma�� hareket ettiriyorken fonksiyon �al���r.
            //Stationary Ki�inin parma�� de�iyor ama hareket etmiyorsa ge�erli olan fonksiyon
            //Canceled Sistem parma�� alg�lamad���nda �al���r.



            //touch.phase dokunmati�i kontrol eder ve hareket varsa e�itler.
            else if (touch.phase == TouchPhase.Moved)
            {
                // Oyun sahnesindeki z ekseni telefon ekran�ndaki ekran�ndaki dokunaca��m�z y eksenine
                // Oyun sahnesindeki x eksenini telefon ekran�ndaki dokunaca��m�z x eksenine e�itlememiz gerekli
                // Oyun i�erisindeki Y pozisyonunu oldu�u gibi b�rakal�m 
                // touch.deltaPosition nedir ? Dokunmatik ekranda dokundu�umuz ve s�r�kledi�imiz alan aras�ndaki pozisyon fark�n� �l�er ve fark�n� al�r. 

                rb.velocity = new Vector3(touch.deltaPosition.x * speedModifier * Time.deltaTime,
                          rb.velocity.y,
                          0f);

            }

            //Elin telefondan �ekildi�i anda
            else if (touch.phase == TouchPhase.Ended)
            {
                //Parma��m�z� �ekti�imiz anda hareketi s�f�rla.
                rb.velocity = Vector3.zero;
            }

        }
    }





    


}
