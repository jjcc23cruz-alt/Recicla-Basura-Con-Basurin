using System.Collections;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    public RectTransform panelDesplegable;
    public float duracionAnimacion = 0.25f;

    private bool estaAbierto = false;
    private Coroutine animacionActual;

    void Start()
    {
        // Asegura que el panel empiece cerrado (escala Y en 0) y activo
        panelDesplegable.gameObject.SetActive(true);
        panelDesplegable.localScale = new Vector3(1, 0, 1);
    }

    public void AlternarPanel()
    {
        estaAbierto = !estaAbierto;

        // Si ya hay una animación corriendo, la detenemos para evitar errores
        if (animacionActual != null)
        {
            StopCoroutine(animacionActual);
        }

        // Iniciamos la nueva animación hacia 1 (abierto) o 0 (cerrado)
        float escalaObjetivoY = estaAbierto ? 1f : 0f;
        animacionActual = StartCoroutine(AnimarEscala(escalaObjetivoY));
    }

    private IEnumerator AnimarEscala(float objetivoY)
    {
        float tiempoTranscurrido = 0f;
        float escalaInicialY = panelDesplegable.localScale.y;

        while (tiempoTranscurrido < duracionAnimacion)
        {
            tiempoTranscurrido += Time.deltaTime;
            // SmoothStep hace que el movimiento acelere y frene suavemente
            float progreso = Mathf.SmoothStep(0f, 1f, tiempoTranscurrido / duracionAnimacion);

            float nuevaEscalaY = Mathf.Lerp(escalaInicialY, objetivoY, progreso);
            panelDesplegable.localScale = new Vector3(1, nuevaEscalaY, 1);

            yield return null; // Espera al siguiente fotograma
        }

        // Asegura que termine en el valor exacto
        panelDesplegable.localScale = new Vector3(1, objetivoY, 1);
    }
}
