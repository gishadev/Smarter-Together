using Gisha.SmarterTogether.Body.Drone;
using Gisha.SmarterTogether.Body.Robot;
using UnityEngine;

namespace Gisha.SmarterTogether.Body
{
    public static class BodySwapper
    {
        public static BodyPlaceholder CurrentBody { private set; get; }

        static DroneController _droneController;
        static RobotController _currentRobot;

        public static void Initialize()
        {
            _droneController = Object.FindObjectOfType<DroneController>();
            UpdateCurrentBody(_droneController);
        }

        public static void ReturnToDrone()
        {
            if (_currentRobot == null)
                return;

            UpdateCurrentBody(_droneController);

            _droneController.DroneCamera.gameObject.SetActive(true);
            _currentRobot.RobotCamera.gameObject.SetActive(false);
        }

        public static void ChangeToRobot(RobotController robot)
        {
            _currentRobot = robot;
            UpdateCurrentBody(robot);

            // Update State.
            _droneController.DroneCamera.gameObject.SetActive(false);
            robot.RobotCamera.gameObject.SetActive(true);
        }

        private static void UpdateCurrentBody(BodyPlaceholder newBody)
        {
            if (CurrentBody != null)
                CurrentBody.IsWorking = false;

            newBody.IsWorking = true;
            CurrentBody = newBody;
        }
    }
}