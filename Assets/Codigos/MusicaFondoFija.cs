using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicaFondoFija : MonoBehaviour
{
    private static MusicaFondoFija instancia;

    void Awake()
    {
        // Patrón Singleton para evitar que la música se duplique si recargas la escena
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject); // Hace que este objeto NO se destruya al cambiar de escena
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnEnable()
    {
        // Nos suscribimos al evento de cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Este método se ejecuta automáticamente cada vez que cargas una escena nueva
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Si la escena que se acaba de cargar es la de victoria (asegúrate de que se llame "Win" o cámbialo por tu nombre exacto)
        if (scene.name == "Win")
        {
            Destroy(gameObject); // Destruye la música de fondo para dejar paso a la música de victoria
        }
    }
}