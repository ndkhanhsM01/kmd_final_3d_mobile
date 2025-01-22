

using UnityEngine;
using UnityEngine.Events;

public abstract class MCSearchMachine : MonoBehaviour
{
    [SerializeField] protected byte intervalFrame = 2;
    [SerializeField] protected UnityEvent onMCEnter;
    [SerializeField] protected UnityEvent onMCExit;
    protected MainCharacter mc => GameplayController.Instance.MC;

    protected bool isMCInside;
    protected virtual void Update()
    {
        if (Time.frameCount % intervalFrame != 0) return;

        if (CheckMCInside())
        {
            if (!isMCInside)
            {
                onMCEnter?.Invoke();
                isMCInside = true;
            }
        }
        else if (isMCInside)
        {
            onMCExit?.Invoke();
            isMCInside = false;
        }
    }

    protected abstract bool CheckMCInside();
}