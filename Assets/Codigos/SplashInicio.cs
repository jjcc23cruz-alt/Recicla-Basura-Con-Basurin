using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashInicio : MonoBehaviour
{
    public GameObject can2; //Variable 
    public float dBL; //Tiempo de cambio
    private float tE = 0;

    // Update is called once per frame
    void Update()
    {
        tE += Time.deltaTime;
        if (tE > dBL)
        {
            Destroy(gameObject);
            can2.SetActive(true);
        }

    }
}
