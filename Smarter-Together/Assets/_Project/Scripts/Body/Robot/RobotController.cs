using Gisha.SmarterTogether.Core;
using UnityEngine;

namespace Gisha.SmarterTogether.Body.Robot
{
    public class RobotController : BodyPlaceholder, IRaycastTarget
    {
        public Camera RobotCamera { private set; get; }

        private void Awake()
        {
            RobotCamera = GetComponentInChildren<Camera>();
        }

        public void OnRaycast()
        {
            if (Input.GetMouseButtonDown(1))
                BodySwapper.ChangeToRobot(this);
        }
    }
}