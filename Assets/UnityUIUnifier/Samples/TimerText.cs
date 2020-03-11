using System;
using UnityEngine;

namespace UnityUIUnifier.Samples
{
    public class TimerText : MonoBehaviour
    {
        [SerializeField]
        UnifiedText text = null;

        void Update()
        {
            text.Text = DateTime.Now.ToString();
        }
    }
}
