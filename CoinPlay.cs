using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinPlay : MonoBehaviour
{


    private void Update()
    {
        transform.Rotate(0f, 180f * Time.deltaTime, 0f);
    }

}
