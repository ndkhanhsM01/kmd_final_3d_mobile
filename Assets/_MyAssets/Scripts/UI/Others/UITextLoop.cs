using System.Collections;
using TMPro;
using UnityEngine;

public class UITextLoop: MonoBehaviour
{
    [SerializeField] private string[] contents;
    [SerializeField] private float timeInterval = 0.1f;
    [SerializeField] private TMP_Text tmp;
    private int id = 0;

    private void OnEnable()
    {
        StartCoroutine(IE_Loop());
    }

    private IEnumerator IE_Loop()
    {
        var waiter = new WaitForSecondsRealtime(timeInterval);
        while (true)
        {
            tmp.text = contents[id];
            id++;
            if (id == contents.Length) id = 0;
            yield return waiter;
        }
    }

}