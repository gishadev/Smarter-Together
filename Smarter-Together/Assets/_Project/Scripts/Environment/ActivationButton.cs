using Gisha.Effects.Audio;
using System;
using System.Linq;
using UnityEngine;

namespace Gisha.SmarterTogether.Environment
{
    public class ActivationButton : MonoBehaviour
    {
        [SerializeField] private float sqrDistForActivation = default;
        [SerializeField] private Door[] doors = default;

        public event Action<bool> Triggered;

        bool _isTriggering = false;

        GameObject[] _robots;
        Animator _animator;

        private void Awake()
        {
            _robots = GameObject.FindGameObjectsWithTag("Robot");
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            foreach (var door in doors)
                Triggered += door.Trigger;
        }

        private void OnDisable()
        {
            foreach (var door in doors)
                Triggered -= door.Trigger;
        }

        private void Update()
        {
            if (_robots.Any(x => Vector3.SqrMagnitude(x.transform.position - transform.position) < sqrDistForActivation))
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

        [ContextMenu("Activate")]
        private void OnEnterArea()
        {
            Debug.Log("<color=green>Button was activated!</color>");

            _animator.SetBool("IsPressed", true);

            if (doors.Length > 0)
                Triggered(true);

            AudioManager.Instance.PlaySFX("click_005");
        }

        [ContextMenu("Deactivate")]
        private void OnExitArea()
        {
            Debug.Log("<color=red>Button was deactivated</color>");

            _animator.SetBool("IsPressed", false);

            if (doors.Length > 0)
                Triggered(false);

            AudioManager.Instance.PlaySFX("click_005");
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, Mathf.Sqrt(sqrDistForActivation));
        }
    }
}