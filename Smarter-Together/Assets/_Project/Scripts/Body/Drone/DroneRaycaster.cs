using Gisha.SmarterTogether.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Gisha.SmarterTogether.Body.Drone
{
    [System.Serializable]
    public class DroneRaycaster
    {
        public float Distance { get; private set; }
        public string TargetName { get; private set; } = "NUN";

        public void UpdateRaycast(DroneController controller)
        {
            var raycastHits = Physics.RaycastAll(controller.transform.position, controller.Camera.transform.forward);
            var raycastTargets = new List<IRaycastTarget>();

            foreach (var hit in raycastHits)
                if (hit.collider.TryGetComponent(out IRaycastTarget raycastTarget))
                {
                    Distance = hit.distance;
                    TargetName = hit.collider.name;
                    raycastTargets.Add(raycastTarget);
                }

            if (raycastTargets.Count > 0)
                raycastTargets[0].OnRaycast();
            else
            {
                Distance = 0f;
                TargetName = "NUN";
            }
        }
    }
}