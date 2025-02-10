
using MLib;
using System.Collections;
using UnityEngine;

public class TrapFire : MonoBehaviour
{
    [SerializeField] private bool playOnStart = true;
    [SerializeField] private bool autoLoop = true;
    [SerializeField] private float delay = 0f;
    [SerializeField] private float activeDuration = 1f;
    [SerializeField] private float deactiveDuration = 1f;
    [SerializeField] private HarmfulArea harmfulArea;

    private Coroutine crActive;
    private Coroutine crDeactive;

    private void Start()
    {
        if (playOnStart)
            Play();
    }

    [MButton]
    public void Play()
    {
        Kill();
        StartCoroutine(IE_Start());
    }

    [MButton]
    public void Kill()
    {
        harmfulArea.SetActive(false);
        StopAllCoroutines();
    }

    private IEnumerator IE_Start()
    {
        yield return new WaitForSeconds(delay);
        crActive = StartCoroutine(IE_Active());
    }

    private IEnumerator IE_Active()
    {
        if(crDeactive != null)
        {
            StopCoroutine(crDeactive);
        }

        harmfulArea.SetActive(true);
        yield return new WaitForSeconds(activeDuration);

        crDeactive = StartCoroutine(IE_Deactive());
    }
    private IEnumerator IE_Deactive()
    {
        if (crActive != null)
        {
            StopCoroutine(crActive);
        }

        harmfulArea.SetActive(false);
        yield return new WaitForSeconds(deactiveDuration);

        if(autoLoop)
            crActive = StartCoroutine(IE_Active());
    }
}