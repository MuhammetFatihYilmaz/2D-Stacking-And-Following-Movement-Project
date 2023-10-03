using UnityEngine;

namespace StackingSample.Gameplay.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovementHandler : MonoBehaviour
    {
        [field: SerializeField] private float movementSpeed;
        [field: SerializeField] private float jumpForce;
        [field: SerializeField] private LayerMask groundLayer;
        [field: SerializeField] private float groundCheckValue;
        private Rigidbody2D rigidBody;
        private float inputValueHorizontal;
        private Vector2 movementVelocity;
        private bool isJumping;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 direction = transform.TransformDirection(Vector3.down) * groundCheckValue;
            Gizmos.DrawRay(transform.position, direction);
        }

        private void Awake() => TryGetComponent(out rigidBody);

        private void Update()
        {
            inputValueHorizontal = Input.GetAxis("Horizontal");
            isJumping = Input.GetKeyDown(KeyCode.Space);

            if(isJumping) JumpSequence();
        }

        private void FixedUpdate() => HorizontalMovement();

        private void HorizontalMovement()
        {
            movementVelocity = rigidBody.velocity;
            movementVelocity.x = inputValueHorizontal * movementSpeed;
            rigidBody.velocity = movementVelocity;
        }

        private void JumpSequence()
        {
            if (!IsGrounded()) return;

            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            isJumping = false;
        }

        private bool IsGrounded()
        {
            return Physics2D.Raycast(transform.position, Vector3.down, groundCheckValue, groundLayer);
        }
    }
}
