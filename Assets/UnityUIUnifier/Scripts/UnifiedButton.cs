using System;
using UnityEngine;
using UnityEngine.UI;

#if UUU_MRTK_PRESENT
using Microsoft.MixedReality.Toolkit.UI;
#endif

namespace UnityUIUnifier
{
    public class UnifiedButton : UnifiedText
    {
        enum ButtonComponentType
        {
            None = 0,
            UnityButton,
            MrtkInteractable
        }

        ButtonComponentType componentType = ButtonComponentType.None;

        Button unityButton;

#if UUU_MRTK_PRESENT
        Interactable interactable;
#endif

        public event Action OnClick;

        /// <summary>
        /// Trigger click event
        /// </summary>
        public void Click()
        {
            switch (componentType)
            {
                case ButtonComponentType.UnityButton:
                    unityButton.onClick.Invoke();
                    break;
#if UUU_MRTK_PRESENT
                case ButtonComponentType.MrtkInteractable:
                    interactable.TriggerOnClick();
                    break;
#endif
            }
        }

        protected override void GetUIComponent()
        {
            // UnityEngine.UI.Button
            unityButton = GetComponent<Button>();
            if (unityButton != null)
            {
                componentType = ButtonComponentType.UnityButton;
                unityButton.onClick.AddListener(() =>
                {
                    OnClick?.Invoke();
                });
                return;
            }

            // Microsoft.MixedReality.Toolkit.UI.Interactable;
#if UUU_MRTK_PRESENT
            interactable = GetComponent<Interactable>();
            if (interactable != null)
            {
                componentType = ButtonComponentType.MrtkInteractable;
                interactable.OnClick.AddListener(() =>
                {
                    OnClick?.Invoke();
                });
                return;
            }
#endif
            // Not Found
            Debug.LogError("Text component not found");
        }
    }
}
