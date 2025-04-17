using UnityEngine;

public class Path_Drawer : MonoBehaviour
{
    [SerializeField] private float sphereRadius;
    [SerializeField] private Color sphereColor;
    [SerializeField] private Color lineColor;

    private void OnDrawGizmos()
    {
        Transform lastChild = null;

        foreach (Transform child in transform)
        {
            Gizmos.color = sphereColor;
            Gizmos.DrawSphere(child.transform.position, sphereRadius);

            if (lastChild)
            {
                Gizmos.color = lineColor;
                Gizmos.DrawLine(lastChild.position, child.position);
            }

            lastChild = child;
        }
    }
}
