using UnityEngine;

namespace Gisha.SmarterTogether.Core
{
    public class NextLevelTrigger : MonoBehaviour
    {
        [SerializeField] private int nextLevel = default;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Drone"))
                GameManager.LoadLevel(nextLevel);
        }
    }
}