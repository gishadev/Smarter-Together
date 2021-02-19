using System;
using UnityEngine;

namespace Gisha.SmarterTogether.UI
{
    public class FullDarkCaller : MonoBehaviour
    {
        public event Action FullDark;

        public void CallFullDark() => FullDark();
    }
}