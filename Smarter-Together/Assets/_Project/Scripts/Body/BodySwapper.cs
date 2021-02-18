using Gisha.SmarterTogether.Body.Drone;
using Gisha.SmarterTogether.Body.Robot;

namespace Gisha.SmarterTogether.Body
{
    public static class BodySwapper
    {
        public static BodyPlaceholder CurrentBody { private set; get; }

        static DroneController _droneController;
        static RobotController _currentRobot;

        public static void Initialize(DroneController drone)
        {
            _droneController = drone;
            UpdateCurrentBody(_droneController);
        }

        public static void ReturnToDrone()
        {
            if (_currentRobot == null)
                return;

            UpdateCurrentBody(_droneController);

            _droneController.Camera.gameObject.SetActive(true);
            _currentRobot.Camera.gameObject.SetActive(false);
        }

        public static void ChangeToRobot(RobotController robot)
        {
            _currentRobot = robot;
            UpdateCurrentBody(robot);

            // Update State.
            _droneController.Camera.gameObject.SetActive(false);
            robot.Camera.gameObject.SetActive(true);
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