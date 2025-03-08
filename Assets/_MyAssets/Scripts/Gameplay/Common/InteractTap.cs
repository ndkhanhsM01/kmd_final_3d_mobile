
using UnityEngine;

public abstract class InteractTap : InteractSpecial
{
    protected override void Reset()
    {
        base.Reset();
        if (info == null)
        {
            info = new();
            info.type = SpecialSelection.InputType.Tap;
        }
    }
    protected override abstract void OnTap();
}