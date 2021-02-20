using Gisha.SmarterTogether.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gisha.SmarterTogether.Core
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        private static GameManager Instance { get; set; }
        #endregion

        private void Awake()
        {
            CreateInstance();
        }

        private void Start()
        {
            LoadLevel(5);
        }

        private void CreateInstance()
        {
            DontDestroyOnLoad(gameObject);

            if (Instance == null)
                Instance = this;


            if (Instance != null && Instance != this)
                Destroy(gameObject);
        }

        public static void LoadLevel(int levelNumber)
        {
            Fader.Instance.StartFade();
            Fader.Instance.FullDark += () => Instance.SyncLevelLoad(levelNumber);
        }

        private void SyncLevelLoad(int levelNumber)
        {
            SceneManager.LoadScene($"Level_{levelNumber}");
            SceneManager.LoadScene("Game", LoadSceneMode.Additive);

            Fader.Instance.FullDark -= () => Instance.SyncLevelLoad(levelNumber);
            Fader.Instance.ExitFade();
        }
    }
}