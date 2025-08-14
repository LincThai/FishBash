using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[ExecuteInEditMode]
public class TriggerDisplay : MonoBehaviour
{
    // set colour variables
    public Color triggerColour;
    public Color triggerWireColour;

    // reference to box collider
    private BoxCollider bc;

    private void OnEnable()
    {
        // reference box collider
        bc = GetComponent<BoxCollider>();
    }

    private void OnDrawGizmos()
    {
        // call RedrawBox function
        RedrawBox();
    }
    private void OnDrawGizmosSelected()
    {
        // call RedrawBox function
        RedrawBox();
    }

    private void RedrawBox()
    {
        // checks if there is a box collider
        if (bc != null)
        {
            // set as trigger means isTrigger is true
            bc.isTrigger = true;
            Vector3 drawBoxScale = new Vector3(transform.lossyScale.x * bc.size.x, transform.lossyScale.y * bc.size.y, transform.lossyScale.z * bc.size.z);
            Vector3 tempScale = transform.worldToLocalMatrix.MultiplyPoint(transform.lossyScale);
            Vector3 drawBoxPosition = transform.localToWorldMatrix.MultiplyPoint(bc.center);

            Gizmos.matrix = Matrix4x4.TRS(drawBoxPosition, transform.rotation, drawBoxScale);
            Gizmos.color = triggerColour;
            Gizmos.DrawCube(Vector3.zero, Vector3.one);
            Gizmos.color = triggerWireColour;
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }
}
