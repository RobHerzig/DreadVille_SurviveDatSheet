using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGizmos : MonoBehaviour {

    public Color GizmoColor = Color.green;
    public float GizmoSize = 0.2f;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = GizmoColor;
        Gizmos.DrawSphere(transform.position, GizmoSize);
    }
}
