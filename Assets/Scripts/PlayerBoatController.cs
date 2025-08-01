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
        public float playerAcceleration = 2.5f;

        // references
        public Camera playerCamera;

    }
}
