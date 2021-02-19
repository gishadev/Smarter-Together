using UnityEngine;

namespace Gisha.SmarterTogether.UI
{
    public enum HUDType { Drone, Robot }

    public class HUDManager : MonoBehaviour
    {
        [Header("GameObjects")]
        [SerializeField] private GameObject robotHUD = default;
        [SerializeField] private GameObject droneHUD = default;

        public void UpdateHUD(HUDType type)
        {
            switch (type)
            {
                case HUDType.Drone:
                    robotHUD.SetActive(false);
                    droneHUD.SetActive(true);
                    break;
                case HUDType.Robot:
                    robotHUD.SetActive(true);
                    droneHUD.SetActive(false);
                    break;
            }
        }
    }
}