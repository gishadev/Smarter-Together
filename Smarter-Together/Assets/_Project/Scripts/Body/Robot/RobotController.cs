using Gisha.SmarterTogether.Core;
using UnityEngine;

namespace Gisha.SmarterTogether.Body.Robot
{
    public class RobotController : BodyPlaceholder, IRaycastTarget
    {
        [SerializeField] private Camera robotCamera = default;

        public Camera RobotCamera => robotCamera;

        private void Update()
        {
            if (!IsWorking)
                return;
        }

        public void OnRaycast()
        {
            if (Input.GetMouseButtonDown(1))
                BodySwapper.ChangeToRobot(this);
        }
    }
}