using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] float _lerpCoeff;

    public void Shake(float force)
    {
        StartCoroutine(addShake(force));
    }

    IEnumerator addShake(float force)
    {
        _camera.transform.localPosition += Vector3.up / 10f * force;
        while (Vector3.Distance(_camera.transform.localPosition, Vector3.zero) > 0.01f)
        {
            _camera.transform.localPosition = Vector3.Lerp(_camera.transform.localPosition, Vector3.zero, Time.deltaTime * _lerpCoeff);
            yield return null;
        }
        _camera.transform.localPosition = Vector3.zero;
    }
}
