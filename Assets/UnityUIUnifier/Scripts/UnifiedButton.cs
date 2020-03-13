using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UUU_MRTK_PRESENT
using Microsoft.MixedReality.Toolkit.UI;
#endif

namespace UnityUIUnifier
{
    public class UnifiedButton : UnifiedUIComponent
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
        Interactable mrtkInteractable;
#endif

        /// <summary>
        /// Whether the user can interact with the buttton
        /// </summary>
        public bool Interactable
        {
            set
            {
                SetInteractable(value);
            }
            get
            {
                return GetInteractable();
            }
        }

        [SerializeField]
        private UnityEvent onClickUnityEvent = new UnityEvent();

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
                    mrtkInteractable.TriggerOnClick();
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
                    InvokeOnClickEvent();
                });
                return;
            }

            // Microsoft.MixedReality.Toolkit.UI.Interactable;
#if UUU_MRTK_PRESENT
            mrtkInteractable = GetComponent<Interactable>();
            if (mrtkInteractable != null)
            {
                componentType = ButtonComponentType.MrtkInteractable;
                mrtkInteractable.OnClick.AddListener(() =>
                {
                    InvokeOnClickEvent();
                });
                return;
            }
#endif
            // Not Found
            Debug.LogError("Text component not found");
        }

        private void InvokeOnClickEvent()
        {
            OnClick?.Invoke();
            onClickUnityEvent.Invoke();
        }


        private void SetInteractable(bool interactable)
        {
            if (!initialized)
            {
                Initialize();
            }

            switch (componentType)
            {
                case ButtonComponentType.UnityButton:
                    unityButton.interactable = interactable;
                    return;
#if UUU_MRTK_PRESENT
                case ButtonComponentType.MrtkInteractable:
                    mrtkInteractable.IsEnabled = interactable;
                    return;
#endif
                case ButtonComponentType.None:
                default:
                    return;
            }
        }

        private bool GetInteractable()
        {
            if (!initialized)
            {
                Initialize();
            }

            switch (componentType)
            {
                case ButtonComponentType.UnityButton:
                    return unityButton.interactable;
#if UUU_MRTK_PRESENT
                case ButtonComponentType.MrtkInteractable:
                    return mrtkInteractable.IsEnabled;
#endif
                case ButtonComponentType.None:
                default:
                    return false;
            }
        }
    }
}
