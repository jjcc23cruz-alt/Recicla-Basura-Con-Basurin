using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public GameObject item;

    [Header("Configuración del Slot")]
    [Tooltip("Tipo de objeto que acepta este slot (debe coincidir con el ItemInfo del objeto). Si está vacío, acepta cualquiera.")]
    public string slotType; 

    [Tooltip("Marcar si quieres que el objeto desaparezca (se destruya) al meterlo en esta casilla.")]
    public bool destroyOnDrop = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Si el slot está libre y hay un objeto siendo arrastrado
        if (item == null && DragHandler.itemDragging != null)
        {
            GameObject draggedObject = DragHandler.itemDragging;

            // Verificamos si el objeto tiene un componente ItemInfo
            ItemInfo itemInfo = draggedObject.GetComponent<ItemInfo>();

            // Comprobamos si el slot no pide tipo específico, o si el tipo coincide
            bool isCorrectType = string.IsNullOrEmpty(slotType) || (itemInfo != null && itemInfo.itemType == slotType);

            if (isCorrectType)
            {
                if (destroyOnDrop)
                {
                    // Si el slot hace desaparecer el objeto (ej. casilla de consumir/almacenar)
                    Destroy(draggedObject);
                    DragHandler.itemDragging = null;
                }
                else
                {
                    // Se queda guardado en el slot
                    item = draggedObject;
                    item.transform.SetParent(transform);

                    // Si es un objeto 3D o UI, ajustamos su posición al centro del slot
                    item.transform.position = transform.position;
                }
            }
            // Si NO es el tipo correcto, DragHandler detectará que no cambió de padre y regresará al contenedor
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (item != null && item.transform.parent != transform)
        {
            item = null;
        }
    }
}