using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class HostileProjectile : HarmfulArea
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeLife = -1;
    [SerializeField] private string[] blockedTags = new string[1] {CustomTags.StaticStructure};
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

        //rigid.linearVelocity = Vector3.zero;
        rigid.isKinematic = true;
        hitbox.enabled = false;
        onStop?.Invoke();
    }

    private IEnumerator IE_MoveForward()
    {
        float timer = 0f;
        while (!TimeOut() && rigid.isKinematic == false)
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
        if (blockedTags.Contains(collision.gameObject.tag))
        {
            Stop();
        }
    }
}