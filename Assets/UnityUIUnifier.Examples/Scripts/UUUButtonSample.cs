using UnityEngine;

namespace UnityUIUnifier.Samples
{
    public class UUUButtonSample : MonoBehaviour
    {
        [SerializeField]
        UnifiedButton button = null;

        private void Awake()
        {
            button.OnClick += () =>
            {
                Debug.Log("OnClick");
            };
        }
    }
}
