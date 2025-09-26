using UnityEngine;

public class MaxFloaterTest : MonoBehaviour
{
    // set variables
    // references
    public Rigidbody rigidBody;

    // numerical values
    public float depthBeforeSubmerge = 1f;
    public float displacementAmount = 3f;
    public int floaterCount = 1;
    public float waterDrag = 1f;
    public float waterAngularDrag = 1f;

    private void FixedUpdate()
    {
        // add a gravitational force to the 4 floater points and divide that force by the number of floaters
        rigidBody.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);
        // get the wave height
        float waveHeight = MaxWaveManager.instance.GetWaveHeight(transform.position.x, transform.position.z);
        // check when underwater if floater y is less than the height of the wave means we need to apply a buoyancy force
        if (transform.position.y < waveHeight)
        {
            // calculate the displacement mutiplier by subtracting the wave height from the floaters y position to make it positive,
            // divide it by the depth before submerged value and then clamp it between 0 and 1. this approximates a pecentage of how much
            // the object is submerged which affects the strength of the buoyancy force. clamp it between 0 and 1 because once an
            // object is fully submerged the buoyancy force remains the same regardless of the depth of the object. Multiply the whole thing by the displacement amount.
            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubmerge) * displacementAmount;
            
            // then we add an upward force with the y component equal to the force of gravity multiplied by the displacement multiplier.
            // we use the acceleration force mode as the buoyancy force should not be affected by the objects mass.
            // we change add force to add force at position and add the position of the floater to make the ship rotate rather than stay flat
            // but gravity is at the center forcing only one side down.
            rigidBody.AddForceAtPosition(new Vector3(0, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0), transform.position, ForceMode.Acceleration);
            
            // applies drag
            rigidBody.AddForce(displacementMultiplier * -rigidBody.linearVelocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rigidBody.AddTorque(displacementMultiplier * -rigidBody.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
