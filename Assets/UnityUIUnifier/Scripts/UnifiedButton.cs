using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityUIUnifier
{
    public class UnifiedButton : UnifiedText
    {
        enum ButtonComponentType
        {
            None = 0,
            UnityButton
        }

        ButtonComponentType componentType = ButtonComponentType.None;
        Button unityButton;

        public event Action OnPressed;

        public void Press()
        {
            switch (componentType)
            {
                case ButtonComponentType.UnityButton:
                    unityButton.onClick.Invoke();
                    break;
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
                    OnPressed?.Invoke();
                });
                return;
            }

            // Not Found
            Debug.LogError("Text component not found");
        }
    }
}
