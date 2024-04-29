using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace UI
{
    public class UISounds : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            AudioManager.Instance.Play("uiHover");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            AudioManager.Instance.Play("uiClick");
        }
    }
}