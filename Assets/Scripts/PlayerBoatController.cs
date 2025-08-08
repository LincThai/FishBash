using UnityEngine;

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
        public Camera playerCamera;
        public Rigidbody body;

        private void FixedUpdate()
        {
            // forward or backward movement input
            float verticalInput = Input.GetAxis("Vertical");
            // adds a force to push forward
            body.AddRelativeForce(Vector3.forward * verticalInput * playerMoveSpeed);

            // Left or Right torque/rotational input
            float horizontalInput = Input.GetAxis("Horizontal");
            // adds a torque for rotation
            body.AddRelativeTorque(Vector3.up * horizontalInput * playerRotSpeed);
        }

    }
}
