using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MLib
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    // attach GraphicRaycaster component into this game object
    public class UIDetectClickOutSide : MonoBehaviour
    {
        [SerializeField] private bool isOn;
        [SerializeField] private GraphicRaycaster graphicRaycaster;
        [Header("Evetns")]
        [Tooltip("The event will be invoked when actived and click out side UI area")]
        public UnityEvent OnClickOutSide;

        private EventSystem eventSystem => EventSystem.current;
        private void Update()
        {
            if (!isOn) return;

            if (Input.GetMouseButtonDown(0))
            {
                if (!IsPointerOverUIElement())
                {
                    HandleClickOutside();
                }
            }
        }

        public void SetToggle(bool toggle)
        {
            isOn = toggle;
        }

        private void HandleClickOutside()
        {
            OnClickOutSide?.Invoke();
        }

        private bool IsPointerOverUIElement()
        {

            PointerEventData pointerEventData = new PointerEventData(eventSystem)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();

            graphicRaycaster.Raycast(pointerEventData, results);
            Debug.Log(results.Count);
            return results.Count > 0;
        }
    }
}
