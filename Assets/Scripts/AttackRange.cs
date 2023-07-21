using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public float distanceThreshold = 5f;
    private bool showAttackRange = false;

    private void OnDrawGizmosSelected()
    {
        if (showAttackRange)
        {
            // Set the color for the transparent circle (e.g., red with 50% transparency)
            Gizmos.color = new Color(1f, 0f, 0f, 0.5f);

            // Draw the transparent circle at the parent's position with the specified attack range
            Gizmos.DrawWireSphere(transform.position, distanceThreshold);
        }
    }

    private void OnMouseDown()
    {
        showAttackRange = !showAttackRange;
    }
}