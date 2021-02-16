using Gisha.SmarterTogether.Body.Drone;
using Gisha.SmarterTogether.Body.Robot;
using UnityEngine;

namespace Gisha.SmarterTogether.Body
{
    public static class BodySwapper
    {
        static bool _isInitialized = false;
        static DroneController _droneController;
        static RobotController _currentRobot;

        private static void Initialize()
        {
            _droneController = Object.FindObjectOfType<DroneController>();
        }

        public static void ReturnToDrone()
        {
            if (_currentRobot == null)
                return;

            if (!_isInitialized)
                Initialize();


            _droneController.DroneCamera.gameObject.SetActive(false);
            _currentRobot.RobotCamera.gameObject.SetActive(true);
        }

        public static void ChangeToRobot(RobotController robot)
        {
            if (!_isInitialized)
                Initialize();

            _currentRobot = robot;

            // Update State.
            _droneController.DroneCamera.gameObject.SetActive(false);
            robot.RobotCamera.gameObject.SetActive(true);
        }
    }
}