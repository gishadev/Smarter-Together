using Gisha.SmarterTogether.Body;
using Gisha.SmarterTogether.Environment;
using UnityEngine;

namespace Gisha.SmarterTogether.Core
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Level")]
        [SerializeField] private Door enterLevelDoor = default;
        [SerializeField] private Transform spawnPoint = default;

        public Door EnterLevelDoor => enterLevelDoor;
        public Transform SpawnPoint => spawnPoint;

        private void Awake()
        {
            // Instance = this;
            if (enterLevelDoor == null)
                Debug.LogError("enterLevelDoor is NULL");
            if (SpawnPoint == null)
                Debug.LogError("spawnPoint is NULL");
        }

        private void Start()
        {
            EnterLevelDoor.Trigger(true);
            BodySwapper.Initialize(GameManager.SpawnDrone(spawnPoint.position));
        }

    }
}