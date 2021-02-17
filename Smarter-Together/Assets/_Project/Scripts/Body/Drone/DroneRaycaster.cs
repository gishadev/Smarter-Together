using Gisha.SmarterTogether.Core;
using System.Collections.Generic;
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
            var raycastHits = Physics.RaycastAll(transform.position, drone.Camera.transform.forward);
            var raycastTargets = new List<IRaycastTarget>();

            foreach (var hit in raycastHits)
                if (hit.collider.TryGetComponent(out IRaycastTarget raycastTarget))
                    raycastTargets.Add(raycastTarget);

            if (raycastTargets.Count > 0)
                raycastTargets[0].OnRaycast();
        }
    }
}