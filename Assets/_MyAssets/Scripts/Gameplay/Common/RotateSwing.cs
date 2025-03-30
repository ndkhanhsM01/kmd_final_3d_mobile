using UnityEngine;

public class RotateSwing : MonoBehaviour
{
    [SerializeField] private Vector3 axis = Vector3.forward;
    [SerializeField] private float amplitude = 30f;
    [SerializeField] private float speed = 2f;

    private float time;

    void Update()
    {
        time += Time.deltaTime;
        float angle = amplitude * Mathf.Sin(speed * time);
        transform.localRotation = Quaternion.Euler(axis * angle);
    }
}
