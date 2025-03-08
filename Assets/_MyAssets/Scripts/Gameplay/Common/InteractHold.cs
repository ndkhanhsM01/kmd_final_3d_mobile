
using UnityEngine;

public abstract class InteractHold : InteractSpecial
{
    protected override void Reset()
    {
        base.Reset();
        if (info == null)
        {
            info = new();
            info.type = SpecialSelection.InputType.Hold;
        }
    }
    protected override abstract void OnBeginHold();
    protected override abstract void OnEndHold();
}