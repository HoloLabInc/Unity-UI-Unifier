using UnityEngine;

namespace UnityUIUnifier
{
    public abstract class UnifiedUIComponent : MonoBehaviour
    {
        protected bool initialized = false;

        protected virtual void Awake()
        {
            if (!initialized)
            {
                Initialize();
            }
        }

        protected virtual void Initialize()
        {
            GetUIComponent();
            initialized = true;
        }

        protected abstract void GetUIComponent();
    }
}
