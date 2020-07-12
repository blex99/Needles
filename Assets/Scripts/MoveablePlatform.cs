using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePlatform : MonoBehaviour
{
    [SerializeField]
    public float speed;

    public GameObject objectToMove;
    public Transform start;
    public Transform end;

    Transform target; // current transform object is moving towards

    private void Awake()
    {
        target = end; // assuming objectToMove at starting pos
    }

    private void FixedUpdate()
    {
        float step = speed * Time.deltaTime;
        objectToMove.transform.position = Vector3.MoveTowards(
            objectToMove.transform.position, target.position, step);

        if (Vector3.Distance(start.position, objectToMove.transform.position) < 0.001f) target = end;
        else if (Vector3.Distance(end.position, objectToMove.transform.position) < 0.001f) target = start;
    }

    private void OnDrawGizmos()
    {
        if (start != null && end != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(start.position, end.position);
        }
        else
        {
            Debug.LogWarning("posStart and posEnd must be assigned!");
        }
    }
}
