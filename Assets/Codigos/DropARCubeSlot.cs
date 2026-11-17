using UnityEngine;
using UnityEngine.EventSystems;

public class DropARCubeSlot : MonoBehaviour, IDropHandler
{
    public GameObject item;

    [Header("Configuración AR")]
    [Tooltip("Tipo de objeto que acepta este cubo (debe coincidir con el ItemInfo). Si está vacío, acepta cualquiera.")]
    public string slotType; 

    [Tooltip("Marcar si quieres que el objeto desaparezca (se destruya) al soltarlo en este cubo.")]
    public bool destroyOnDrop = false;
    void Start()
    {
        // Desactiva la parte visual (Mesh Renderer) automáticamente al iniciar el juego
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }
    }
    // Este método se activa automáticamente si la ARCamera tiene un GraphicRaycaster o si el sistema de eventos lo detecta
    public void OnDrop(PointerEventData eventData)
    {
        ProcesarSoltado();
    }

    // Como respaldo por si el Canvas AR bloquea el evento OnDrop, usamos un Raycast adaptado a la ARCamera en el Update
    private void Update()
    {
        // Si el usuario suelta el clic o el dedo en la pantalla
        if (Input.GetMouseButtonUp(0) && DragHandler.itemDragging != null)
        {
            // Lanzamos un rayo desde la ARCamera hacia donde tocó el usuario
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Si el rayo golpea exactamente a este cubo
                if (hit.transform == transform)
                {
                    ProcesarSoltado();
                }
            }
        }

        // Si el objeto guardado se sale o cambia de padre, liberamos el slot
        if (item != null && item.transform.parent != transform)
        {
            item = null;
        }
    }
   
    void ProcesarSoltado()
    {
        if (item == null && DragHandler.itemDragging != null)
        {
            GameObject draggedObject = DragHandler.itemDragging;
            ItemInfo itemInfo = draggedObject.GetComponent<ItemInfo>();

            bool isCorrectType = string.IsNullOrEmpty(slotType) || (itemInfo != null && itemInfo.itemType == slotType);

            if (isCorrectType)
            {
                if (destroyOnDrop)
                {
                    Destroy(draggedObject);
                    DragHandler.itemDragging = null;
                }
                else
                {
                    item = draggedObject;
                    item.transform.SetParent(transform);
                    item.transform.position = transform.position;
                }
            }
        }
    }
}