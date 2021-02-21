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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
                LoadMenu();
        }

        private void CreateInstance()
        {
            DontDestroyOnLoad(gameObject);

            if (Instance == null)
                Instance = this;


            if (Instance != null && Instance != this)
                Destroy(gameObject);
        }

        #region Level Loading
        public static void LoadLevel(int levelNumber)
        {
            Fader.Instance.StartFade();
            Fader.Instance.FullDark += () => Instance.OnDarkLevelLoad(levelNumber);
        }

        private void OnDarkLevelLoad(int levelNumber)
        {
            SceneManager.LoadScene($"Level_{levelNumber}");
            SceneManager.LoadScene("Game", LoadSceneMode.Additive);

            Fader.Instance.FullDark -= () => Instance.OnDarkLevelLoad(levelNumber);
            Fader.Instance.ExitFade();
        }
        #endregion

        #region Menu Loading
        public static void LoadMenu()
        {
            Fader.Instance.StartFade();
            Fader.Instance.FullDark += () => Instance.OnDarkMenuLoad();
        }

        private void OnDarkMenuLoad()
        {
            SceneManager.LoadScene("MainMenu");

            Fader.Instance.FullDark -= () => Instance.OnDarkMenuLoad();
            Fader.Instance.ExitFade();
        }
        #endregion
    }
}