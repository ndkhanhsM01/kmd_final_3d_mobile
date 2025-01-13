

using UnityEngine;

namespace MLib.Examples
{
    public class AwakeDetector: MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Awake: " + gameObject.scene.name);
        }
    }
}