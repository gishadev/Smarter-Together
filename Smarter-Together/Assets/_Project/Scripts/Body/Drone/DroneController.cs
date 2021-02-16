using UnityEngine;

namespace Gisha.SmarterTogether.Body.Drone
{
    [RequireComponent(typeof(DroneRaycaster))]
    public class DroneController : BodyPlaceholder
    {
        [Header("Drone")]
        [SerializeField] private float moveForce = default;
        [SerializeField] private float turnFollowForce = default;
        [SerializeField] private float turnRealignForce = default;

        [Header("Camera")]
        [SerializeField] private Camera droneCamera = default;
        [SerializeField] private float mouseSensitivity = default;

        public Camera Camera => droneCamera;

        float _xRot, _yRot;
        float _zInput, _xInput, _yInput;

        Rigidbody _rb;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (!IsWorking)
                return;

            UpdateMovementInput();
        }

        private void FixedUpdate()
        {
            if (!IsWorking)
                return;
            
            RotateCamera();
            MoveBody();
            RotateBody();
        }

        private void UpdateMovementInput()
        {
            _zInput = Input.GetAxis("Vertical");
            _xInput = Input.GetAxis("Horizontal");

            if (Input.GetKey(KeyCode.Space))
                _yInput = 1f;
            else if (Input.GetKey(KeyCode.LeftControl))
                _yInput = -1f;
            else
                _yInput = 0f;
        }
        private void RotateCamera()
        {
            var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;

            _xRot -= mouseY;
            _xRot = Mathf.Clamp(_xRot, -90f, 90f);

            _yRot += mouseX;
            Camera.transform.rotation = Quaternion.Euler(Vector3.right * _xRot + Vector3.up * _yRot);
        }
        private void MoveBody()
        {
            var vel = transform.right * _xInput + transform.forward * _zInput;
            vel.y = _yInput;
            _rb.velocity = vel * moveForce * Time.fixedDeltaTime;
        }
        private void RotateBody()
        {
            var droneRotationTarget = Quaternion.Euler(Vector3.up * _yRot);

            if (Mathf.Abs(_rb.rotation.x) > 0 || Mathf.Abs(_rb.rotation.z) > 0)
                _rb.rotation = Quaternion.Slerp(_rb.rotation, droneRotationTarget, turnRealignForce * Time.fixedDeltaTime);
            else
                _rb.rotation = Quaternion.Slerp(_rb.rotation, droneRotationTarget, turnFollowForce * Time.fixedDeltaTime);
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * 5f);

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(Camera.transform.position, Camera.transform.forward * 5f);
        }
    }
}