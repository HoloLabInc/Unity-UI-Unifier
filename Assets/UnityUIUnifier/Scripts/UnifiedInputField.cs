using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

#if UUU_TEXTMESHPRO_PRESENT
using TMPro;
#endif

namespace UnityUIUnifier
{
    public class UnifiedInputField : UnifiedUIComponent
    {
        [Serializable]
        public class OnChangeEvent : UnityEvent<string> { }

        [Serializable]
        public class SubmitEvent : UnityEvent<string> { }

        enum InputFieldComponentType
        {
            None = 0,
            UnityInputField,
            TMProInputField
        }

        InputFieldComponentType componentType = InputFieldComponentType.None;

        InputField unityInputField;

#if UUU_TEXTMESHPRO_PRESENT
        TMP_InputField tmpInputField;
#endif

        [SerializeField]
        private OnChangeEvent onValueChangedUnityEvent = new OnChangeEvent();

        public event Action<string> OnValueChanged;

        [SerializeField]
        private SubmitEvent onEndEditUnityEvent = new SubmitEvent();

        public event Action<string> OnEndEdit;


        public string Text
        {
            set
            {
                SetText(value);
            }
            get
            {
                return GetText();
            }
        }

        /// <summary>
        /// Whether the user can interact with the input field
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

        private void InvokeOnValueChagnedEvent(string text)
        {
            OnValueChanged?.Invoke(text);
            onValueChangedUnityEvent.Invoke(text);
        }

        private void InvokeEndEditEvent(string text)
        {
            OnEndEdit?.Invoke(text);
            onEndEditUnityEvent.Invoke(text);
        }

        private void SetText(string text)
        {
            if (!initialized)
            {
                Initialize();
            }

            switch (componentType)
            {
                case InputFieldComponentType.UnityInputField:
                    unityInputField.text = text;
                    return;
#if UUU_TEXTMESHPRO_PRESENT
                case InputFieldComponentType.TMProInputField:
                    tmpInputField.text = text;
                    return;
#endif
                case InputFieldComponentType.None:
                default:
                    return;
            }
        }

        private string GetText()
        {
            if (!initialized)
            {
                Initialize();
            }

            switch (componentType)
            {
                case InputFieldComponentType.UnityInputField:
                    return unityInputField.text;
#if UUU_TEXTMESHPRO_PRESENT
                case InputFieldComponentType.TMProInputField:
                    return tmpInputField.text;
#endif
                case InputFieldComponentType.None:
                default:
                    return null;
            }
        }

        private void SetInteractable(bool interactable)
        {
            if (!initialized)
            {
                Initialize();
            }

            switch (componentType)
            {
                case InputFieldComponentType.UnityInputField:
                    unityInputField.interactable = interactable;
                    return;
#if UUU_TEXTMESHPRO_PRESENT
                case InputFieldComponentType.TMProInputField:
                    tmpInputField.interactable = interactable;
                    return;
#endif
                case InputFieldComponentType.None:
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
                case InputFieldComponentType.UnityInputField:
                    return unityInputField.interactable;
#if UUU_TEXTMESHPRO_PRESENT
                case InputFieldComponentType.TMProInputField:
                    return tmpInputField.interactable;
#endif
                case InputFieldComponentType.None:
                default:
                    return false;
            }
        }

        protected override void GetUIComponent()
        {
            // UnityEngine.UI.InputField
            unityInputField = GetComponent<InputField>();
            if (unityInputField != null)
            {
                componentType = InputFieldComponentType.UnityInputField;
                unityInputField.onValueChanged.AddListener(text =>
                {
                    InvokeOnValueChagnedEvent(text);
                });
                unityInputField.onEndEdit.AddListener(text =>
                {
                    InvokeEndEditEvent(text);
                });
                return;
            }

            // TMPro.TMP_InputField
#if UUU_TEXTMESHPRO_PRESENT
            tmpInputField = GetComponent<TMP_InputField>();
            if (tmpInputField != null)
            {
                componentType = InputFieldComponentType.TMProInputField;
                tmpInputField.onValueChanged.AddListener(text =>
                {
                    InvokeOnValueChagnedEvent(text);
                });
                tmpInputField.onEndEdit.AddListener(text =>
                {
                    InvokeEndEditEvent(text);
                });
                return;
            }
#endif
            // Not Found
            Debug.LogError("Text component not found");
        }
    }
}
