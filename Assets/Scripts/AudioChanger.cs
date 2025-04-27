using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ICollisionDetection))]
public class AudioChanger : MonoBehaviour
{
    [SerializeField] private float _volumeChangeStep = 0.01f;
    [SerializeField] private float _volumeChangeDelay = 0.2f;

    private AudioSource _audioSource;
    private Coroutine _coroutine;
    private ICollisionDetection _collisionDetection;

    private void Awake()
    {
        _collisionDetection = GetComponent<ICollisionDetection>();
        _collisionDetection.CollisionEnter += SoftAudioGain;
        _collisionDetection.CollisionExit += SoftAudioDecrease;
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        _collisionDetection.CollisionEnter -= SoftAudioGain;
        _collisionDetection.CollisionExit -= SoftAudioDecrease;
        StopCoroutine();
    }

    private void SoftAudioGain()
    {
        StopCoroutine();
        WaitForSeconds wait = new WaitForSeconds(_volumeChangeDelay);

        _audioSource.volume = 0;
        _audioSource.Play();
        _coroutine = StartCoroutine(SoftAudioChange(wait, 1));
    }

    private void SoftAudioDecrease()
    {
        StopCoroutine();
        WaitForSeconds wait = new WaitForSeconds(_volumeChangeDelay);

        _coroutine = StartCoroutine(SoftAudioChange(wait, 0));
    }

    private IEnumerator SoftAudioChange(WaitForSeconds wait, float target)
    {
        int changeModifier = target > _audioSource.volume ? 1 : -1;

        while (_audioSource.volume != target)
        {
            _audioSource.volume += changeModifier * _volumeChangeStep;

            yield return wait;
        }
    }

    private void StopCoroutine()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }
}
