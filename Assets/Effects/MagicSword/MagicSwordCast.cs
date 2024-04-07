using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSwordCast : MonoBehaviour
{
    [SerializeField] Material _bladeMaterial;
    [SerializeField] Material _glowMiddleMaterial;
    [SerializeField] List<ParticleSystem> _effects;

    [SerializeField] float _fadeTime;

    IEnumerator SmoothFade(bool inOut)
    {
        if (inOut)
        {
            for (float t = 0; t < _fadeTime; t += Time.deltaTime)
            {
                float interpolant = Mathf.InverseLerp(0, _fadeTime, t);
                _bladeMaterial.SetFloat("_alpha", interpolant);
                _glowMiddleMaterial.SetFloat("_alpha", interpolant);
                yield return null;
            }
            _bladeMaterial.SetFloat("_alpha", 1);
            _glowMiddleMaterial.SetFloat("_alpha", 1);
            foreach (var effect in _effects)
            {
                effect.Play();
            }

        }
        else
        {
            foreach (var effect in _effects)
            {
                effect.Stop();
            }
            for (float t = _fadeTime; t > 0; t -= Time.deltaTime)
            {
                float interpolant = Mathf.InverseLerp(0, _fadeTime, t);
                _bladeMaterial.SetFloat("_alpha", interpolant);
                _glowMiddleMaterial.SetFloat("_alpha", interpolant);
                yield return null;
            }
            _bladeMaterial.SetFloat("_alpha", 0);
            _glowMiddleMaterial.SetFloat("_alpha", 0);
        }

    }

    public void CastSword()
    {
        StartCoroutine(SmoothFade(true));
    }

    public void HideSword()
    {
        StartCoroutine(SmoothFade(false));
    }



}
