using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class InputManager
{
    public Vector2 PointerDownPosition;
    public Vector2 PointerPosition;
    public Vector2 PointerUpPosition;

    public Vector2 DeltaPosition =>
        new ((PointerPosition.x - PointerDownPosition.x) / Screen.width,
            (PointerPosition.y - PointerDownPosition.y) / Screen.height);

    public UnityEvent OnPointerDownEvent;
    public UnityEvent OnPointerEvent;
    public UnityEvent OnPointerUpEvent;

    public void PointerUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnPointerDown();
        }

        if (Input.GetMouseButton(0))
        {
            OnPointer();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnPointerUp();
        }
    }

    public void OnPointerDown()
    {
        PointerDownPosition = Input.mousePosition;
        OnPointerDownEvent?.Invoke();
    }

    public void OnPointer()
    {
        PointerPosition = Input.mousePosition;

        OnPointerEvent?.Invoke();
    }

    public void OnPointerUp()
    {
        PointerUpPosition = Input.mousePosition;
        OnPointerUpEvent?.Invoke();
    }
}