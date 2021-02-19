using TMPro;
using UnityEngine;

namespace Gisha.SmarterTogether.Body.Drone
{
    public class DroneHUD : MonoBehaviour
    {
        [SerializeField] private TMP_Text targetText = default;
        [SerializeField] private TMP_Text altitudeText = default;
        [SerializeField] private TMP_Text distanceText = default;

        public DroneController Drone { get; set; }

        private void Update()
        {
            if (Drone == null)
                return;

            targetText.text = Drone.DroneRaycaster.TargetName.ToUpper();
            altitudeText.text = Drone.transform.position.y.ToString("F1");
            distanceText.text = Drone.DroneRaycaster.Distance.ToString("F1");
        }
    }
}