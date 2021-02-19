using System;
using UnityEngine;

namespace Gisha.SmarterTogether.UI
{
    public class Fader : MonoBehaviour
    {
        #region Singleton
        public static Fader Instance { get; private set; }
        #endregion

        public event Action FullDark;

        Animation _animation;

        private void Awake()
        {
            CreateInstance();
            _animation = GetComponentInChildren<Animation>();
        }

        private void CreateInstance()
        {
            DontDestroyOnLoad(gameObject);

            if (Instance == null)
                Instance = this;

            if (Instance != null && Instance != this)
                Destroy(gameObject);
        }

        public void CallFullDark() => FullDark();

        public void StartFade()
        {
            _animation.Play("Darken");
        }

        public void ExitFade()
        {
            _animation.Play("Lighten");
        }
    }
}