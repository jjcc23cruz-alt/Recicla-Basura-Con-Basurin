using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SplashTimer : MonoBehaviour
{
    [Header("Configuración del Splash")]

    [Tooltip("Tiempo en segundos que se mostrará el Splash")]
    public float tiempoSplash = 3f;

    [Tooltip("Nombre de la escena que se cargará después")]
    public string siguienteEscena = "MenuPrincipal";

    void Start()
    {
        StartCoroutine(EsperarYCambiarEscena());
    }

    IEnumerator EsperarYCambiarEscena()
    {
        // Esperar el tiempo indicado por el usuario
        yield return new WaitForSeconds(tiempoSplash);

        // Cambiar a la siguiente escena
        SceneManager.LoadScene(siguienteEscena);
    }
}
