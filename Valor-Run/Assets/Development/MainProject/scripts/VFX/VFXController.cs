using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    public static VFXController _vFXControllerSingle;

    private void Start()
    {
        _vFXControllerSingle = this;
    }

    public void DoVfxEffect(ParticleSystem particleSystem, Vector3 spawnPos)
    {
        ParticleSystem particle = Instantiate(particleSystem);
        particle.transform.position =spawnPos;
    }
}