using UnityEngine;

namespace Gisha.SmarterTogether.Core
{
    public class RaycastHUD : MonoBehaviour
    {
        [SerializeField] private string raycastName = default;
        public string RaycastName => raycastName;
    }
}