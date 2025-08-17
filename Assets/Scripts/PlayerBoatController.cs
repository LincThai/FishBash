using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerBoatController : MonoBehaviour
    {
        // set variables
        // numerical variables
        [Header ("Player Numerical Variables")]
        public float playerMoveSpeed = 5;
        public float playerRotSpeed = 5;

        // references
        public Rigidbody body;
        //public InputActionAsset inputActions;

        // inputs
        private InputAction moveAction;

        private Vector2 moveAmount;

        private void Awake()
        {
            moveAction = InputSystem.actions.FindAction("Move");
        }

        private void FixedUpdate()
        {
            moveAmount = moveAction.ReadValue<Vector2>();
            float verticalInput = moveAmount.y;
            float horizontalInput = moveAmount.x;

            // forward or backward movement input
            //float verticalInput = Input.GetAxis("Vertical");
            // adds a force to push forward
            body.AddRelativeForce(Vector3.forward * verticalInput * playerMoveSpeed);

            // Left or Right torque/rotational input
            //float horizontalInput = Input.GetAxis("Horizontal");
            // adds a torque for rotation
            body.AddRelativeTorque(Vector3.up * horizontalInput * playerRotSpeed);
        }

    }
}
