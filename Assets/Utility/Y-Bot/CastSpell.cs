using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpell : MonoBehaviour
{
    [SerializeField] LightningStrike _lightningStrike;
    [SerializeField] Crack[] _cracks;

    void CastCrack()
    {
        foreach (Crack crack in _cracks)
        {
            crack.CastCrack();
        }
    }

    void CastLightningStrike()
    {
        _lightningStrike.Strike();
    }
}
