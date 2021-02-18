using Gisha.SmarterTogether.Core;
using System.Linq;
using UnityEngine;

namespace Gisha.SmarterTogether.Body.Robot
{
    public class RobotController : BodyPlaceholder, IRaycastTarget
    {
        [Header("General")]
        [SerializeField] private Camera robotCamera = default;
        [SerializeField] private float mouseSensitivity = default;

        [Header("Movement")]
        [SerializeField] private float moveSpeed = 7.5f;
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private float gravityMult = 4.5f;
        [Range(0f, 1f)]
        [SerializeField] private float airMovementMult = 0.5f;
        [Space]
        [SerializeField] private LayerMask whatIsGround = default;

        [SerializeField] private float groundCheckerRadius = 0.5f;

        Vector3 GroundCheckerPoint => transform.position - Vector3.up * (_controller.height / 2f - _controller.center.y);

        float _nowMoveSpeed;
        Vector3 _velocity;
        Vector3 _moveDir;

        bool _isGrounded, _isInAir;
        float _hInput, _vInput;
        float _xRot, _yRot;

        CharacterController _controller;

        public Camera Camera => robotCamera;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (!IsWorking)
                return;

            _hInput = Input.GetAxis("Horizontal");
            _vInput = Input.GetAxis("Vertical");

            _isGrounded = CheckGroundCollider();

            if (Input.GetButtonDown("Jump") && _isGrounded)
                Jump();
        }

        void FixedUpdate()
        {
            Gravity();

            if (!IsWorking)
                return;

            MoveBody();
            RotateCamera();
        }

        private void MoveBody()
        {
            _moveDir = transform.forward * _vInput + transform.right * _hInput;
            _nowMoveSpeed = _isGrounded ? moveSpeed : moveSpeed * airMovementMult;
            _controller.Move(Vector3.ClampMagnitude(_moveDir, 1f) * _nowMoveSpeed * Time.fixedDeltaTime);
        }

        private void Jump()
        {
            _isInAir = true;
            _velocity = CalculateJumpVelocity(_moveDir * _nowMoveSpeed);
            _controller.slopeLimit = 90f;
        }

        private void Gravity()
        {
            if (!IsWorking)
            {
                _velocity.x = 0f;
                _velocity.z = 0f;
            }

            if (_isGrounded && _velocity.y < 0f)
            {
                if (_isInAir)
                    _isInAir = false;

                _velocity = Vector3.up * -2f;
                _controller.slopeLimit = 50f;
            }
            else
                _velocity.y += Physics.gravity.y * gravityMult * Time.fixedDeltaTime;

            _controller.Move(_velocity * Time.fixedDeltaTime);
        }

        private void RotateCamera()
        {
            // Camera Input.
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;

            // Camera/Body Rotating.
            _xRot -= mouseY;
            _xRot = Mathf.Clamp(_xRot, -90f, 90f);

            Camera.transform.localRotation = Quaternion.Euler(_xRot, 0f, 0f);

            _yRot = mouseX;
            transform.Rotate(Vector3.up * _yRot);
        }

        private bool CheckGroundCollider()
        {
            var colliders = Physics.OverlapSphere(GroundCheckerPoint, groundCheckerRadius, whatIsGround);
            return colliders.Count(x => x.gameObject != gameObject) > 0;
        }

        private Vector3 CalculateJumpVelocity(Vector3 moveVel)
        {
            Vector3 vel;
            float multiplier = _isGrounded ? 1f : airMovementMult;

            vel.x = moveVel.x * multiplier;
            vel.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
            vel.z = moveVel.z * multiplier;

            return vel;
        }

        public void OnRaycast()
        {
            if (Input.GetMouseButtonDown(1))
                BodySwapper.ChangeToRobot(this);
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
                return;

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(GroundCheckerPoint, groundCheckerRadius);
        }
    }
}