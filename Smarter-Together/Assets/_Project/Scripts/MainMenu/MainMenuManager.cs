using Gisha.SmarterTogether.Core;
using UnityEngine;

namespace Gisha.SmarterTogether.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        public void OnClick_LoadLevel(int levelNumber) => GameManager.LoadLevel(levelNumber);

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}