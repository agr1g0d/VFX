using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Crack : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> _effects;
    [SerializeField] float[] _scales;
    [SerializeField] float[] _rotations;
    [SerializeField] float _transitionTime = 0.5f;

    private CameraShake _cameraShake;

    private void Start()
    {
        _cameraShake = FindObjectOfType<CameraShake>();
    }

    public void CastCrack()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(Earthquake());
        }
    }

    IEnumerator Earthquake()
    {
        for (int i = 0; i < _scales.Length; i++)
        {
            foreach (ParticleSystem effect in _effects)
            {
                effect.transform.localScale = Vector3.one * _scales[i];
            }
            transform.localScale = Vector3.one * _scales[i];
            transform.eulerAngles = Vector3.up * _rotations[i];
            _cameraShake.Shake(_scales[i]);
            yield return new WaitForSeconds(_transitionTime);
        }
        for (float f = _scales[^1]; f > 0; f -= Time.deltaTime * 7f)
        {
            foreach (ParticleSystem effect in _effects)
            {
                effect.transform.localScale = Vector3.one * f;
            }
            transform.localScale = Vector3.one * f;
            yield return null;
        }
        foreach (ParticleSystem effect in _effects)
        {
            effect.transform.localScale = Vector3.zero;
        }
        transform.localScale = Vector3.zero;

    }

}
