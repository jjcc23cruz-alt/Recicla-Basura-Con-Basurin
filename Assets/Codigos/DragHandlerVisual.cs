using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandlerVisual : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public static GameObject itemDragging;

    Vector3 startPosition;
    Transform startParent;
    Transform dragParent;

    // Referencia para la copia visual que flota al arrastrar
    private GameObject visualClone;

    void Start()
    {
        dragParent = GameObject.FindGameObjectWithTag("DragParent").transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        itemDragging = gameObject;

        startPosition = transform.position;
        startParent = transform.parent;

        // 1. Cambiamos el padre del objeto original al contenedor de arrastre
        transform.SetParent(dragParent);

        // 2. CREAMOS UN CLON VISUAL: Esto evita que el objeto original desaparezca o se oculte por el Canvas.
        // Creamos una copia exacta del objeto en la interfaz para que sea lo que realmente flote bajo el dedo/ratón.
        visualClone = Instantiate(gameObject, dragParent);
        
        // Desactivamos los scripts de arrastre en el clon para que no interfiera
        Destroy(visualClone.GetComponent<DragHandlerVisual>());
        
        // Si tiene componentes de raycast o colisión en el clon, los desactivamos para que no bloqueen
        CanvasGroup canvasGroup = visualClone.GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = visualClone.AddComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false; // Permite que el ratón "atraviese" el clon y detecte los cubos debajo

        // Ocultamos temporalmente el objeto original mientras el clon hace el trabajo visual
        gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        // Hacemos que el clon visual siga exactamente la posición del ratón o toque en la pantalla
        if (visualClone != null)
        {
            visualClone.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        itemDragging = null;

        // Destruimos el clon visual que estábamos arrastrando
        if (visualClone != null)
        {
            Destroy(visualClone);
        }

        // Volvemos a mostrar el objeto original
        gameObject.SetActive(true);

        // Si el objeto sigue en el dragParent significa que no se soltó en ningún slot válido, así que regresa
        if (transform.parent == dragParent)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
    }

    void Update()
    {
        
    }
}