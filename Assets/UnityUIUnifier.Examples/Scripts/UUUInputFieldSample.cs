using UnityEngine;

namespace UnityUIUnifier.Samples
{
    public class UUUInputFieldSample : MonoBehaviour
    {
        [SerializeField]
        UnifiedInputField inputField = null;

        private void Awake()
        {
            inputField.Text = "Default Text";

            inputField.OnValueChanged += (text) =>
            {
                Debug.Log($"OnValueChanged: {text}");
            };

            inputField.OnEndEdit += (text) =>
            {
                Debug.Log($"OnEndEdit: {text}");
            };
        }
    }
}
