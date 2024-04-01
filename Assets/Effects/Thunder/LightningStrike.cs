using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LightningStrike : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] List<LineRenderer> _lightnings;
    [SerializeField] List<ParticleSystem> _additionalEffects;
    [SerializeField] float _distortion;
    [SerializeField] float _fadeOut;
    [SerializeField] float _repeat;

    private CameraShake _cameraShake;
    private bool flag;

    private void Start()
    {
        _cameraShake = FindObjectOfType<CameraShake>();
    }

    private void Update()
    {
        // if ((int) Time.time % _repeat == 0 && !flag) // calls every 3sec
        // {
        //     Strike();
        //     flag = true;
        // } else if ((int) Time.time % _repeat != 0)
        // {
        //     flag = false;
        // }
    }

    public void Strike()
    {       

        

        foreach(ParticleSystem effect in _additionalEffects) //play all lightning effects
        {
            effect.transform.LookAt(_target);
            effect.Play();
        }
        _cameraShake.Shake(5f);
        StartCoroutine(SmoothFadeOut());
        StartCoroutine(distortSeveralTimes(3));

    }

    Vector3[] DistortPositions(Vector3[] positions, bool distortLast) //function that makes break line effect
    {
        for (int i = 0; i < positions.Length; ++i)
        {
            if (i != 0 && i != positions.Length - 1) 
            {
                positions[i] += new Vector3(Random.Range(-1f, 1f) * _distortion,
                    Random.Range(-1f, 1f) * _distortion,
                    Random.Range(-1f, 1f) * _distortion);
            }
        }
        if (distortLast)
        {
            positions[positions.Length-1] += new Vector3(Random.Range(-1f, 1f) * _distortion,
                    Random.Range(-1f, 1f) * _distortion,
                    Random.Range(-1f, 1f) * _distortion);
        }
        return positions;
    }

    IEnumerator SmoothFadeOut()     // lightning fade out
    {
        
        foreach (LineRenderer _line in _lightnings)
        {
            _line.endColor = new Color(1, 1, 1, 1);
            _line.startColor = new Color(1, 1, 1, 1);
        }
        
        yield return new WaitForSeconds(0.1f);
        for (float t = 1; t > 0; t -= Time.deltaTime * _fadeOut)
        {
            if (t < 0)
            {
                t = 0;
            }
            foreach (LineRenderer _line in _lightnings)
            {
                _line.endColor = new Color(1, 1, 1, t);
                _line.startColor = new Color(1, 1, 1, t);
            }
            yield return null;
        }
        foreach (LineRenderer _line in _lightnings)
        {
            _line.enabled = false;
        }
    }

    IEnumerator distortSeveralTimes(int n)
    {
        Vector3 path = _target.position - transform.position;
        Vector3[] positions = new Vector3[(int)path.magnitude * 2];
        for (int i = 0; i < positions.Length; ++i)      //set positions of LR along its path
        {
            positions[i] = path * Mathf.InverseLerp(0, positions.Length-1, i); 
        }

        for (int i = 0; i < n; i++)
        {
            foreach (LineRenderer _line in _lightnings)
            {
                _line.enabled = true;
                Vector3[] distortedPositions = DistortPositions(positions, 
                    _lightnings.IndexOf(_line) == 0);
                _line.positionCount = distortedPositions.Length;
                _line.SetPositions(distortedPositions);
            }
            yield return new WaitForSeconds(0.05f);
        }
        
    }


}
