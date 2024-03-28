using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpell : MonoBehaviour
{
    [SerializeField] LightningStrike _lightningStrike;
    [SerializeField] Crack[] _cracks;
    [SerializeField] CastFireball _fireball;

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

    void ChargeFireball()
    {
        _fireball.Charge();
    }

    void LaunchFireball()
    {
        _fireball.Launch();
    }
}
