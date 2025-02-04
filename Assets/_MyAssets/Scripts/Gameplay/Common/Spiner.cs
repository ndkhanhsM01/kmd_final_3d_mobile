
using UnityEngine;

public class Spiner: MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 upward = Vector3.up;

    private Transform body;

    private void Awake()
    {
        body = transform;
    }

    private void Update()
    {
        body.Rotate(upward * speed * Time.deltaTime);
    }
}