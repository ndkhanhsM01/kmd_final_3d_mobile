using UnityEngine;

public interface IReceiveDamage
{
    bool ReceiveDamage(Transform source);
    void ReceiveForce(Vector3 force);
}