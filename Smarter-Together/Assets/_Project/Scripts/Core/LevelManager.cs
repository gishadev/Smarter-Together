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

        private void Awake()
        {
            // Instance = this;
            if (enterLevelDoor == null)
                Debug.LogError("enterLevelDoor is NULL");
            if (spawnPoint == null)
                Debug.LogError("spawnPoint is NULL");
        }

        private void Start()
        {
            EnterLevelDoor.Trigger(true);
            BodySwapper.Initialize(spawnPoint);
        }
    }
}