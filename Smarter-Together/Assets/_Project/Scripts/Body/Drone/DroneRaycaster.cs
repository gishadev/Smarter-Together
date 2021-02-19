using Gisha.SmarterTogether.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Gisha.SmarterTogether.Body.Drone
{
    [System.Serializable]
    public class DroneRaycaster
    {
        [SerializeField] private float raycastRadius = 0.5f;

        public float Distance { get; private set; }
        public string TargetName { get; private set; } = "NUN";

        public void UpdateRaycast(DroneController controller)
        {
            var raycastHits = Physics.SphereCastAll(controller.transform.position, raycastRadius, controller.Camera.transform.forward);
            var raycastTargets = new List<IRaycastTarget>();

            foreach (var hit in raycastHits)
                if (hit.collider.TryGetComponent(out RaycastHUD raycastHUD))
                {
                    Distance = hit.distance;
                    TargetName = raycastHUD.RaycastName;

                    if (hit.collider.TryGetComponent(out IRaycastTarget raycastTarget))
                        raycastTargets.Add(raycastTarget);

                    break;
                }

                else
                {
                    Distance = 0f;
                    TargetName = "NUN";
                }

            if (raycastTargets.Count > 0)
                raycastTargets[0].OnRaycast();
        }
    }
}