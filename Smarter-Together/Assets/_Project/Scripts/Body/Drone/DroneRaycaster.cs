using Gisha.SmarterTogether.Core;
using System.Linq;
using UnityEngine;

namespace Gisha.SmarterTogether.Body.Drone
{
    public class DroneRaycaster : MonoBehaviour
    {
        DroneController drone;
        private void Awake()
        {
            drone = GetComponent<DroneController>();
        }

        private void Update()
        {
            var raycastHits = Physics.RaycastAll(transform.position, drone.DroneCamera.transform.forward);
            var targetRaycastHits = raycastHits.Where(x => x.collider.CompareTag("RaycastTarget")).ToArray();

            if (targetRaycastHits.Length > 0)
            {
                var raycastTarget = targetRaycastHits.FirstOrDefault().collider.GetComponent<IRaycastTarget>();
                raycastTarget.OnRaycast();
            }
        }
    }
}