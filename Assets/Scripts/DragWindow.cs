using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragWindow : MonoBehaviour, IDragHandler, IPointerDownHandler
{


    [SerializeField] private RectTransform dragRectTransform;
    [SerializeField] private Canvas canvas;
    public bool isDragable;

    private void Awake() {

        if(dragRectTransform == null) {
            dragRectTransform = transform.parent.GetComponent<RectTransform>();
        }

        if(canvas == null) {
            Transform testCanvasTransform = transform.parent;
            while (testCanvasTransform != null) {
                canvas = testCanvasTransform.GetComponent<Canvas>();
                if(canvas != null) {
                    break;
                }
                testCanvasTransform = testCanvasTransform.parent;
            }
        }
    }

    private void Update() {

        if(dragRectTransform.anchoredPosition.y + (dragRectTransform.rect.height/2) >= canvas.GetComponent<RectTransform>().rect.height/2){

            dragRectTransform.anchoredPosition = new Vector2(dragRectTransform.anchoredPosition.x, (canvas.GetComponent<RectTransform>().rect.height/2) - dragRectTransform.rect.height/2);

        }
        if(dragRectTransform.anchoredPosition.x - (dragRectTransform.rect.width/2) <= -canvas.GetComponent<RectTransform>().rect.width/2){

            dragRectTransform.anchoredPosition = new Vector2((-canvas.GetComponent<RectTransform>().rect.width/2) + dragRectTransform.rect.width/2, dragRectTransform.anchoredPosition.y);

        }
        if(dragRectTransform.anchoredPosition.y - (dragRectTransform.rect.height/2) <= -canvas.GetComponent<RectTransform>().rect.height/2){

            dragRectTransform.anchoredPosition = new Vector2(dragRectTransform.anchoredPosition.x, (-canvas.GetComponent<RectTransform>().rect.height/2) + dragRectTransform.rect.height/2);

        }
        if(dragRectTransform.anchoredPosition.x + (dragRectTransform.rect.width/2) >= canvas.GetComponent<RectTransform>().rect.width/2){

            dragRectTransform.anchoredPosition = new Vector2((canvas.GetComponent<RectTransform>().rect.width/2) - dragRectTransform.rect.width/2, dragRectTransform.anchoredPosition.y);

        }
    }

    public void OnDrag(PointerEventData eventData)
    {

        if(isDragable)
        dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragRectTransform.SetAsLastSibling();
    }

    public void CloseWindow(){

        Destroy(this.dragRectTransform.gameObject);

    }

}

