
using MLib;
using UnityEngine;

public class SwipeAndRotateHorizontal: MonoBehaviour
{
    [SerializeField] private float sensitivity = 1f;
    [SerializeField] private Transform body;

    private Vector2 lastMousePosition = Vector2.zero;
    private Camera cam;
    private bool hitBody;

    private void Awake()
    {
        cam = Camera.main;
        enabled = false;
    }

    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        if(Input.GetMouseButtonDown(0))
        {
            hitBody = CheckHitBody(mousePos);
        }
        else if (hitBody && Input.GetMouseButton(0))
        {
            float deltaX = mousePos.x - lastMousePosition.x;
            float speed = deltaX / Time.deltaTime;
            Rotate(speed);
        }
        else
        {
            hitBody = false;
        }

        lastMousePosition = mousePos;
    }

    private void Rotate(float speed)
    {
        body.Rotate(0f, -speed * sensitivity * Time.deltaTime, 0f);
    }
    private bool CheckHitBody(Vector2 touchPoint)
    {
        if (MHelper.CheckTouchUI())
            return false;

        Ray ray = cam.ScreenPointToRay(touchPoint);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == body)
            {
                return true;
            }
        }
        return false;
    }
}