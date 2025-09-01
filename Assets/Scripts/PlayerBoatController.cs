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
            // subscribe to the input in your input actions asset
            moveAction = InputSystem.actions.FindAction("Move");
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void FixedUpdate()
        {
            // assign the values read from the input system to a vector 2
            moveAmount = moveAction.ReadValue<Vector2>();
            // Forward or Backward movement input is assigned to the y of the vactor 2
            float verticalInput = moveAmount.y;
            // Left or Right torque/rotational input is assigned to the x of the vector 2
            float horizontalInput = moveAmount.x;

            // adds a force to push forward
            body.AddRelativeForce(Vector3.forward * verticalInput * playerMoveSpeed);

            // adds a torque for rotation
            body.AddRelativeTorque(Vector3.up * horizontalInput * playerRotSpeed);

            //float volume = body.linearVelocity.magnitude / 30;
            //body.linearVelocity.magnitude
        }

    }
}
