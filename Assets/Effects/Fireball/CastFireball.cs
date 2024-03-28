using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastFireball : MonoBehaviour
{
    [SerializeField] float _chargingTime;
    [SerializeField] float _flyTime;
    [SerializeField] float _movement;
    [SerializeField] Transform _origin;
    [SerializeField] List<ParticleSystem> _effects;
    [SerializeField] ParticleSystem _baseEffect;
    private Material _material;
    private TrailRenderer _trail;

    private void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        _trail = GetComponent<TrailRenderer>();
    }

    public void Charge()
    {
        
        _trail.enabled = false;
        StartCoroutine(charging(_chargingTime));
    }

    public void Launch()
    {
        _trail.enabled = true;
        transform.SetParent(null);
        transform.rotation = Quaternion.identity;
        StartCoroutine(through());
    }

    IEnumerator through()
    {
        foreach(var effect in _effects)
        {
            effect.Play();
        }
        for (float f = 0; f < _flyTime; f += Time.deltaTime)
        {
            transform.Translate(Vector3.forward * _movement * Time.deltaTime);
            yield return null;
        }
        foreach(var effect in _effects)
        {
            effect.Stop();
        }
    }

    IEnumerator charging(float time)
    {
        
        transform.SetParent(_origin, false);
        transform.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
        for (float f = 0; f <= time; f += Time.deltaTime)
        {
            _material.SetFloat("_Alpha",
                Mathf.Lerp(0, 1, Mathf.InverseLerp(0, time, f)));
            _material.SetFloat("_Speed",
                Mathf.Lerp(0, 7, Mathf.InverseLerp(0, time, f)));
            yield return null;
        }
    }
}
