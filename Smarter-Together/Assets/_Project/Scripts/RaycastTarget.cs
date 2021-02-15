using Gisha.SmarterTogether.Drone;
using UnityEngine;

namespace Gisha.SmarterTogether
{
    public class RaycastTarget : MonoBehaviour
    {
        [SerializeField] private Camera cam;

        public void OnRaycast()
        {
            if (Input.GetMouseButtonDown(1))
            {
                FindObjectOfType<DroneController>().DroneCamera.gameObject.SetActive(false);
                cam.gameObject.SetActive(true);
            }
            Debug.Log("Raycast Target");
        }
    }
}