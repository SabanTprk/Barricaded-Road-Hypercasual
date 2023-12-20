using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    public Transform vectorback;
    public Transform vectorforward;
    public Transform vectorleft;
    public Transform vectorright;


    public void LateUpdate()
    {
        // Vector3 olu�turup pozisyonunu ba�l� player'�m�za e�itliyoruz.
        Vector3 viewPos = transform.position;
        
        // Olu�turdu�umuz Vector3 de�erinin eksenlerini tek tek iki de�er aras�na s�n�rl�yoruz.
        viewPos.z = Mathf.Clamp(viewPos.z, vectorback.transform.position.z, vectorforward.transform.position.z);
        viewPos.x = Mathf.Clamp(viewPos.x, vectorleft.transform.position.x, vectorright.transform.position.x);
        transform.position = viewPos;
    }

}
