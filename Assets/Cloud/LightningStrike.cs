using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LightningStrike : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    [SerializeField] Transform _target;
    [SerializeField] ParticleSystem _additionalLightnings;
    [SerializeField] float _distortion;
    [SerializeField] float _fadeOut;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;
        Strike();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Strike();
        }
    }

    public void Strike()
    {
        _lineRenderer.enabled = true;
        Vector3 path = _target.position - transform.position;
        Vector3[] positions = new Vector3[(int)path.magnitude * 2];
        for (int i = 0; i < positions.Length; ++i)
        {
            positions[i] = path * Mathf.InverseLerp(0, positions.Length-1, i);
            if (i != 0 && i != positions.Length - 1) 
            {
                positions[i] += new Vector3(Random.Range(-1f, 1f) * _distortion,
                    Random.Range(-1f, 1f) * _distortion,
                    Random.Range(-1f, 1f) * _distortion);
            }
        }
        _lineRenderer.positionCount = positions.Length;
        _lineRenderer.SetPositions(positions);
        _additionalLightnings.transform.LookAt(_target.position);
        _additionalLightnings.Play();
        StartCoroutine(SmoothFadeOut());

    }

    IEnumerator SmoothFadeOut()
    {
        _lineRenderer.endColor = new Color(1, 1, 1, 1);
        _lineRenderer.startColor = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.1f);
        for (float t = 1; t > 0; t -= Time.deltaTime * _fadeOut)
        {
            if (t < 0)
            {
                t = 0;
            }
            _lineRenderer.endColor = new Color(1, 1, 1,t);
            _lineRenderer.startColor = new Color(1, 1, 1,t);
            yield return null;
        }
        _lineRenderer.enabled = false;
    }


}
