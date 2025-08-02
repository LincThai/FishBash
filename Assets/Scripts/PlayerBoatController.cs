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

        private void Update()
        {
            
        }

    }
}
