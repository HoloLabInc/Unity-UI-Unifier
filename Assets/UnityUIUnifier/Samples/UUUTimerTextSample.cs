using System;
using UnityEngine;

namespace UnityUIUnifier.Samples
{
    public class UUUTimerTextSample : MonoBehaviour
    {
        [SerializeField]
        UnifiedText text = null;

        void Update()
        {
            text.Text = DateTime.Now.ToString();
        }
    }
}
