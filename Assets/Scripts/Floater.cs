using UnityEngine;

public class Floater : MonoBehaviour
{
    // set variables
    // references
    public Rigidbody rigidBody;

    // numerical values
    public float depthBeforeSubmerge = 1f;
    public float displacementAmount = 1f;

    private void FixedUpdate()
    {
        // check when underwater if floater y is less than 0 means we need to apply a buoyancy force
        if (transform.position.y < 0f)
        {
            // calculate the displacement mutiplier by taking the inverse of the floaters y position to make it positive,
            // divide it by the depth before submerged value and then clamp it between 0 and 1. this approximates a pecentage of how much
            // the object is submerged which affects the strength of the buoyancy force. clamp it between 0 and 1 because once an
            // object is fully submerged the buoyancy force remains the same regardless of the depth of the object. Multiply the whole thing by the displacement amount.
            float displacementMultiplier = Mathf.Clamp01(-transform.position.y / depthBeforeSubmerge) * displacementAmount;
            // then we add an upward force with the y component equal to the force of gravity multiplied by the displacement multiplier.
            // we use the acceleration force mode as the buoyancy force should not be affected by the objects mass.
            rigidBody.AddForce(new Vector3(0, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0), ForceMode.Acceleration);
        }
    }
}
