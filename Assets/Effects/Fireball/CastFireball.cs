using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastFireball : MonoBehaviour
{
    [SerializeField] float _chargingTime;
    [SerializeField] float _flyTime;
    [SerializeField] float _movement;
    [SerializeField] Transform _origin;
    [SerializeField] List<ParticleSystem> _trailEffects;
    [SerializeField] List<ParticleSystem> _effects;
    [SerializeField] List<ParticleSystem> _burstEffects;
    private Material _material;
    private TrailRenderer _trail;
    private CameraShake _cameraShake;

    private void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        _trail = GetComponent<TrailRenderer>();
        _cameraShake = FindObjectOfType<CameraShake>();
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
        foreach(var effect in _trailEffects)
        {
            effect.Play();
        }
        for (float f = 0; f < _flyTime; f += Time.deltaTime)
        {
            transform.Translate(Vector3.forward * _movement * Time.deltaTime);
            yield return null;
        }
        foreach (var effect in _effects)
        {
            effect.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        foreach(var effect in _burstEffects)
        {
            effect.Play();
        }
        _cameraShake.Shake(3f);
        for (float f = 0.3f; f > 0; f -= Time.deltaTime)
        {
            float t = Mathf.InverseLerp(0, 0.5f, f);
            _material.SetFloat("_Alpha",
                Mathf.Lerp(0, 1, t));
            _material.SetFloat("_Speed",
                Mathf.Lerp(0, 7, t));
            yield return null;
        }
        

        foreach(var effect in _trailEffects)
        {
            effect.Stop();
        }
    }

    IEnumerator charging(float time)
    {
        foreach (var effect in _effects)
        {
            effect.Play();
        }
        transform.SetParent(_origin, false);
        transform.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
        for (float f = 0; f <= time; f += Time.deltaTime)
        {
            float t = Mathf.InverseLerp(0, time, f);
            _material.SetFloat("_Alpha",
                Mathf.Lerp(0, 1, t));
            _material.SetFloat("_Speed",
                Mathf.Lerp(0, 7, t));
            transform.localScale = Vector3.one * Mathf.Lerp(0.2f, 1f, t);
            foreach(var effect in _effects)
            {
                effect.transform.localScale = Vector3.one * Mathf.Lerp(0.2f, 1f, t);
            }
            yield return null;
        }
    }
}
