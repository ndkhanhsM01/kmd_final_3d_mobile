using UnityEngine;

public interface IReceiveDamage
{
    void ReceiveDamage(Transform source);
    void ReceiveForce(Vector3 force);
}