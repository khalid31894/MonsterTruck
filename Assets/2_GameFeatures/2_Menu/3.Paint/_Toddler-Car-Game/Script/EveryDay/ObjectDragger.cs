using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarGame_Asad
{
    public class ObjectDragger : MonoBehaviour
    {
        private Vector3 screenPoint;
        private Vector3 startDragPosition;
        private bool isDragging;
        private Rigidbody2D rb;
        private Vector3 offset;
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnMouseDown()
        {
            isDragging = true;
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            startDragPosition = Input.mousePosition;
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
        private void OnMouseDrag()
        {
            if (isDragging)
            {
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
                transform.position = curPosition;
                offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            }
        }
        private void OnMouseUp()
        {
            if (isDragging)
            {
                isDragging = false;
                Vector2 force = (Input.mousePosition - startDragPosition) * 0.005f;
                rb.AddForce(force, ForceMode2D.Impulse);
            }
        }
    }
}
