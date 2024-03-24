using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LightningStrike : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    [SerializeField] Transform _target;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;
    }

    public void Strike()
    {
        Vector3 path = _target.position - transform.position;
        Vector3[] positions = new Vector3[(int)path.magnitude];
        for (int i = 0; i < positions.Length; ++i)
        {
            positions[i] = path * Mathf.InverseLerp(0, positions.Length, i);
        }

    }


}
