using UnityEngine;
using UnityEngine.EventSystems;

public class DropARCubeSlot : MonoBehaviour, IDropHandler
{
    public GameObject item;

    [Header("Configuración AR")]
    public string slotType; 
    public bool destroyOnDrop = false;

    void Start()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null) renderer.enabled = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        ProcesarSoltado();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && (DragHandlerVisual.itemDragging != null || DragHandler.itemDragging != null))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    ProcesarSoltado();
                }
            }
        }
    }

    void ProcesarSoltado()
    {
        GameObject objetoArrastrado = DragHandlerVisual.itemDragging != null ? DragHandlerVisual.itemDragging : DragHandler.itemDragging;

        if (objetoArrastrado != null)
        {
            ItemInfo itemInfo = objetoArrastrado.GetComponent<ItemInfo>();
            bool isCorrectType = string.IsNullOrEmpty(slotType) || (itemInfo != null && itemInfo.itemType == slotType);

            if (isCorrectType)
            {
                if (destroyOnDrop)
                {
                    // 1. Restamos del contador oficial solo al soltarlo en el cubo correcto
                    ItemPool.RestarItem();

                    // 2. Destruimos el objeto
                    Destroy(objetoArrastrado);
    
                    DragHandlerVisual.itemDragging = null;
                    DragHandler.itemDragging = null;
                }
                else
                {
                    item = objetoArrastrado;
                    item.transform.SetParent(transform);
                    item.transform.position = transform.position;
                }
            }
        }
    }
}