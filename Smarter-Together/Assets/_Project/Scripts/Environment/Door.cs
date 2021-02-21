using Gisha.Effects.Audio;
using UnityEngine;

namespace Gisha.SmarterTogether.Environment
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
            AudioManager.Instance.PlaySFX("Door_Opening");
        }

        private void Close()
        {
            animator.SetBool("IsOpened", false);
        }
    }
}