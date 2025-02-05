using System;
using System.Collections;
using UnityEngine;

public class HostileProjectile : MonoBehaviour, ITriggerable
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeLife = -1;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private Collider hitbox;

    private Coroutine crMoveForward;
    private Action onStop;
    public void Move(Vector3 direction)
    {
        rigid.isKinematic = false;
        hitbox.enabled = true;

        rigid.rotation = Quaternion.LookRotation(direction.normalized);
        crMoveForward = StartCoroutine(IE_MoveForward());
    }
    public void SetOnStop(Action callback)
    {
        onStop = callback;
    }
    public void Stop()
    {
        if (crMoveForward != null)
            StopCoroutine(crMoveForward);

        rigid.linearVelocity = Vector3.zero;
        rigid.isKinematic = true;
        hitbox.enabled = false;
        onStop?.Invoke();
    }

    public void Trigger()
    {
        GameplayController.Instance.LoseLevel();

        Stop();
    }

    private IEnumerator IE_MoveForward()
    {
        float timer = 0f;
        while (!TimeOut())
        {
            timer += Time.deltaTime;
            rigid.linearVelocity = moveSpeed * Time.deltaTime * transform.forward;

            yield return null;
        }

        bool TimeOut()
        {
            if (timeLife <= 0)
                return false;

            return timer > timeLife;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(CustomTags.StaticStructure))
        {
            Stop();
        }
    }
}