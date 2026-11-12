using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para gestionar escenas

public class CambiarEscena : MonoBehaviour
{
    // Método para cambiar usando el nombre de la escena
    public void CargarEscenaPorNombre(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }

    // Método opcional para cambiar usando el índice de compilación
    public void CargarEscenaPorIndice(int indiceEscena)
    {
        SceneManager.LoadScene(indiceEscena);
    }
}
