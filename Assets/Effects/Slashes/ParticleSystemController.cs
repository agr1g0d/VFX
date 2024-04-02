using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> _effects;
    private int index = 0;

    public void CallNext()
    {
        _effects[index].Play();
        index++;
        if (index == _effects.Count)
        {
            index = 0;
        }
    }
}
