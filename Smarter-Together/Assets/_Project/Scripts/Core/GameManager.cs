using Gisha.SmarterTogether.Body.Drone;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gisha.SmarterTogether
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        private static GameManager Instance { get; set; }
        #endregion

        [SerializeField] private GameObject dronePrefab = default;

        private void Awake()
        {
            CreateInstance();
        }

        private void Start()
        {
            LoadLevel(1);
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
            SceneManager.LoadScene($"Level_{levelNumber}");
            SceneManager.LoadScene("Game", LoadSceneMode.Additive);
        }

        public static DroneController SpawnDrone(Vector3 position)
        {
            var droneGO = Instantiate(Instance.dronePrefab, position, Quaternion.identity);
            return droneGO.GetComponent<DroneController>();
        }
    }
}