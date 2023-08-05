using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "SOs/Audio Data")]
public class AudioData : ScriptableObject
{
    [SerializeField] AudioClip _jump;
    public AudioClip Jump => _jump;

    [SerializeField] AudioClip _arjunRunning;
    public AudioClip ArjunRunning => _arjunRunning;

    [SerializeField] AudioClip _arjunSlidingLeftRight;
    public AudioClip ArjunSlidingLeftRight => _arjunSlidingLeftRight;

    [SerializeField] AudioClip _arjunFallsDown;
    public AudioClip ArjunFallsDown => _arjunFallsDown;

    [SerializeField] AudioClip _obstacleHit;
    public AudioClip ObstacleHit => _obstacleHit;

    [SerializeField] AudioClip _wingsCollected;
    public AudioClip WingsCollected => _wingsCollected;

    [SerializeField] AudioClip _coinCollected;
    public AudioClip CoinCollected => _coinCollected;

    [SerializeField] AudioClip _geetaSholck;
    public AudioClip GeetaShlock => _geetaSholck;
}
