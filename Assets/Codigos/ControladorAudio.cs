using UnityEngine;

public class ControladorAudio : MonoBehaviour
{
    // Variable para llevar el registro de si está silenciado o no
    private static bool estaSilenciado = false;

    // Esta función se vinculará al botón de audio
    public void AlternarSonido()
    {
        // Cambiamos el estado (si estaba mudo pasa a con sonido, y viceversa)
        estaSilenciado = !estaSilenciado;

        if (estaSilenciado)
        {
            // Silencia absolutamente todo el audio de la aplicación
            AudioListener.volume = 0f;
            Debug.Log("App silenciada");
        }
        else
        {
            // Restaura el volumen al máximo en toda la app
            AudioListener.volume = 1f;
            Debug.Log("Audio activado en toda la app");
        }
    }
}