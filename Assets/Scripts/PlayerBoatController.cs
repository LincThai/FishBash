using System.Collections;
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

        // inputs
        private InputAction moveAction;

        private Vector2 moveAmount;

        // audio
        private bool isPlaying;

        private void Awake()
        {
            // subscribe to the input in your input actions asset
            moveAction = InputSystem.actions.FindAction("Move");
        }

        private void Start()
        {
            // lock the cursor to the center of the screen
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

            if (!isPlaying && moveAmount != Vector2.zero)
            {
                StartCoroutine(OnBoatMoveSFX());

                //float volume = body.linearVelocity.magnitude / 30;
                //body.linearVelocity.magnitude
            }
        }

        IEnumerator OnBoatMoveSFX()
        {
            // change bool value
            isPlaying = true;
            while (body.linearVelocity.magnitude > 1f)
            {
                // play audio
                FindObjectOfType<AudioManager>().Play("Motor");

                yield return new WaitForSeconds(0.5f);
            }
            FindObjectOfType<AudioManager>().Stop("Motor");
            // change bool value
            isPlaying = false;
        }
    }
}
