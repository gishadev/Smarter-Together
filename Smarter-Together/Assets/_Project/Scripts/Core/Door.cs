using UnityEngine;

namespace Gisha.SmarterTogether
{
    [RequireComponent(typeof(Animator))]
    public class Door : MonoBehaviour
    {
        [Header("Opening")]
        [SerializeField] private Transform modelTrans = default;
        [SerializeField] private Vector3 shiftDirection = default;
        [SerializeField] private float shiftDistance = default;
        [SerializeField] private float shiftSpeed = default;

        Animator animator;

        private void OnValidate()
        {
            shiftDirection = shiftDirection.normalized;
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Trigger(bool isActivated)
        {
            if (isActivated)
                OnActivated();
            else
                OnDeactivated();
        }
        
        private void OnActivated()
        {
            animator.SetBool("IsOpened", true);
        }

        private void OnDeactivated()
        {
            animator.SetBool("IsOpened", false);
        }
    }
}