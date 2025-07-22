using UnityEngine;
using UnityEngine.EventSystems;

public class ViewItem : MonoBehaviour, IDragHandler
{
    [SerializeField] private float rotateSpeed = 0.2f;
    [SerializeField] private float zoomSpeed = 0.5f;
    [SerializeField] private float minScale = 0.3f;
    [SerializeField] private float maxScale = 1.5f;

    public void OnDrag(PointerEventData eventData)
    {
        float rotateX = eventData.delta.x * rotateSpeed;
        float rotateY = eventData.delta.y * rotateSpeed;

        transform.Rotate(Vector3.up, rotateX, Space.World);
        transform.Rotate(Vector3.right, rotateY, Space.Self);
    }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.001f) {
            float scale = Mathf.Clamp(transform.localScale.x + scroll * zoomSpeed, minScale, maxScale);
            transform.localScale = Vector3.one * scale;
        }
    }
}
