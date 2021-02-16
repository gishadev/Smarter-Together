using UnityEngine;

namespace Gisha.SmarterTogether.Body
{
    public class BodyManager : MonoBehaviour
    {
        public BodyPlaceholder currentBody;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                BodySwapper.ReturnToDrone();
        }
    }
}