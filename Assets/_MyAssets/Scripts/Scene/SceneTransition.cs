

using UnityEngine;
using UnityEngine.UI;

public class SceneTransition: MonoBehaviour
{
    [SerializeField] private float defaultDuration = 0.75f;
    [SerializeField] private Image imgFade;

    public bool IsDoneIn { get; private set; }
    public bool IsDoneOut { get; private set; }
    public void DoIn(float duration = -1f)
    {

    }
    public void DoOut(float duration = -1f)
    {

    }
}