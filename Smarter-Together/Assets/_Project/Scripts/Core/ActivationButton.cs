using System.Linq;
using UnityEngine;

namespace Gisha.SmarterTogether.Core
{
    public class ActivationButton : MonoBehaviour
    {
        [SerializeField] private float sqrDistForActivation = default;


        bool _isTriggering = false;
        Transform[] robots;

        private void Start()
        {
            robots = GameObject.FindGameObjectsWithTag("Robot")
                .Select(x => x.transform)
                .ToArray();
        }

        private void Update()
        {
            foreach (var robot in robots)
            {
                if (Vector3.SqrMagnitude(robot.transform.position - transform.position) < sqrDistForActivation)
                {
                    if (_isTriggering)
                        return;

                    OnEnterArea();

                    _isTriggering = true;
                    return;
                }

                else
                {
                    if (!_isTriggering)
                        return;

                    OnExitArea();

                    _isTriggering = false;
                }
            }
        }

        private void OnEnterArea()
        {
            Debug.Log("<color=green>Button was activated!</color>");
        }

        private void OnExitArea()
        {
            Debug.Log("<color=red>Button was deactivated</color>");
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, Mathf.Sqrt(sqrDistForActivation));
        }
    }
}