using UnityEngine;

public class RotateSwing : MonoBehaviour
{

    public float amplitude = 30f; 
    public float speed = 2f;

    private float time;

    void Update()
    {
        time += Time.deltaTime;
        float angle = amplitude * Mathf.Sin(speed * time);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
