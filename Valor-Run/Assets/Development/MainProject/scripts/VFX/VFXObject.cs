using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "VFXData", menuName = "SOs/VFXData")]
public class VFXObject : ScriptableObject
{
    [SerializeField] ParticleSystem _arrowHit;
    public ParticleSystem ArrowHit => _arrowHit;

    [SerializeField] ParticleSystem _coinCollection;
    public ParticleSystem CoinCollection => _coinCollection;

    [SerializeField] ParticleSystem _elephantHit;
    public ParticleSystem ElephantHit => _elephantHit;

    [SerializeField] ParticleSystem _fireMonsterHit;
    public ParticleSystem FireMonsterHit => _fireMonsterHit;

    [SerializeField] ParticleSystem _treasureCollection;
    public ParticleSystem TreasureCollection => _treasureCollection;

    [SerializeField] ParticleSystem _hitCameraShake;
    public ParticleSystem HitCameraShake => _hitCameraShake;
}
