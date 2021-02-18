using UnityEngine;

namespace Gisha.SmarterTogether
{
    [RequireComponent(typeof(Animator))]
    public class Door : MonoBehaviour
    {
        Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Trigger(bool isOpen)
        {
            if (isOpen)
                Open();
            else
                Close();
        }

        private void Open()
        {
            animator.SetBool("IsOpened", true);
        }

        private void Close()
        {
            animator.SetBool("IsOpened", false);
        }
    }
}