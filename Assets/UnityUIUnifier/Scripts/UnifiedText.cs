using UnityEngine;
using UnityEngine.UI;

#if UUU_TEXTMESHPRO_PRESENT
using TMPro;
#endif

namespace UnityUIUnifier
{
    public class UnifiedText : UnifiedUIComponent
    {
        enum TextComponentType
        {
            None = 0,
            UnityText,
            TextMesh,
            TMProText
        }

        TextComponentType componentType = TextComponentType.None;

        Text unityText;
        TextMesh textMesh;

#if UUU_TEXTMESHPRO_PRESENT
        TMP_Text tmpText;
#endif

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

        private void SetText(string text)
        {
            if (!initialized)
            {
                Initialize();
            }

            switch (componentType)
            {
                case TextComponentType.UnityText:
                    unityText.text = text;
                    return;
                case TextComponentType.TextMesh:
                    textMesh.text = text;
                    return;
#if UUU_TEXTMESHPRO_PRESENT
                case TextComponentType.TMProText:
                    tmpText.text = text;
                    return;
#endif
                case TextComponentType.None:
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
                case TextComponentType.UnityText:
                    return unityText.text;
                case TextComponentType.TextMesh:
                    return textMesh.text;
#if UUU_TEXTMESHPRO_PRESENT
                case TextComponentType.TMProText:
                    return tmpText.text;
#endif
                case TextComponentType.None:
                default:
                    return null;
            }
        }

        protected override void GetUIComponent()
        {
            // UnityEngine.UI.Text
            unityText = GetComponent<Text>();
            if (unityText != null)
            {
                componentType = TextComponentType.UnityText;
                return;
            }

            // UnityEngine.TextMesh
            textMesh = GetComponent<TextMesh>();
            if (textMesh != null)
            {
                componentType = TextComponentType.TextMesh;
                return;
            }

            // TMPro.TMP_Text
#if UUU_TEXTMESHPRO_PRESENT
            tmpText = GetComponent<TMP_Text>();
            if (tmpText != null)
            {
                componentType = TextComponentType.TMProText;
                return;
            }
#endif
            // Not Found
            Debug.LogError("Text component not found");
        }
    }
}
