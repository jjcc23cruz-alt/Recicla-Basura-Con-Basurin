using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemPool : MonoBehaviour
{
    [Header("Configuración de Victoria")]
    public GameObject pantallaVictoriaPanel;
    public string nombreSiguienteEscena = "Win";

    public static int itemsRestantes = 0;
    private bool pantallaActivada = false;

    void Start()
    {
        // Al iniciar la escena, contamos automáticamente cuántos ítems coleccionables hay en total
        ItemInfo[] todosLosItems = FindObjectsOfType<ItemInfo>();
        itemsRestantes = todosLosItems.Length;
        Debug.Log("Ítems totales registrados al iniciar: " + itemsRestantes);
    }

    // Método para restar 1 solo cuando se destruye un ítem en el cubo
    public static void RestarItem()
    {
        itemsRestantes--;
        Debug.Log("Ítem clasificado correctamente. Faltan por clasificar: " + itemsRestantes);
    }

    void Update()
    {
        if (pantallaActivada) return;

        // Si el contador llega a 0 o menos, ganamos
        if (itemsRestantes <= 0)
        {
            DispararVictoria();
        }
    }

    void DispararVictoria()
    {
        pantallaActivada = true;
        Debug.Log("¡Victoria total! Todos los ítems fueron clasificados en sus cubos.");

        if (pantallaVictoriaPanel != null)
        {
            pantallaVictoriaPanel.SetActive(true);
        }

        if (!string.IsNullOrEmpty(nombreSiguienteEscena))
        {
            SceneManager.LoadScene(nombreSiguienteEscena);
        }
    }
}