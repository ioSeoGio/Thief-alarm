using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Collider))]
public class AlarmTrigger : MonoBehaviour
{
    [SerializeField] private float _volumeChangeStep = 0.01f;
    [SerializeField] private float _volumeChangeDelay = 0.2f;

    private AudioSource _audioSource;
    private Coroutine _coroutine;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Mover>(out Mover mover))
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(SoftAudioGain());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Mover>(out Mover mover))
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            
            _coroutine = StartCoroutine(SoftAudioDecrease());
        }
    }

    private IEnumerator SoftAudioGain()
    {
        WaitForSeconds wait = new WaitForSeconds(_volumeChangeDelay);
        _audioSource.volume = 0;
        _audioSource.Play();

        while (_audioSource.volume != 1)
        {
            _audioSource.volume += _volumeChangeStep;

            yield return wait;
        }
    }

    private IEnumerator SoftAudioDecrease()
    {
        WaitForSeconds wait = new WaitForSeconds(_volumeChangeDelay);

        while (_audioSource.volume != 0)
        {
            _audioSource.volume += -1 * _volumeChangeStep;

            yield return wait;
        }

        _audioSource.Stop();
    }
}
