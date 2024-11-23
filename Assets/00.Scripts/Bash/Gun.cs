using bash;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private int _ammo = 30, _maxAmmo=30;
    [SerializeField]
    private ParticleSystem _particle;
    [SerializeField]
    AudioSource _source;
    [SerializeField]
    AudioClip _clip,_noAMmo;
    [SerializeField]
    float _volume = 0.5f;
    [SerializeField]
    Animator _anime;

    private void Update()
    {
        _anime.SetBool("Fire", RPlayerMana.Instance.playerInput.isFire);
        _anime.SetBool("Reload", Keyboard.current[Key.R].isPressed);
    }



    public void ShootAmmo()
    {
        if(_ammo >0)
        {
            _particle.Play();
            _source.PlayOneShot(_clip, Random.Range(_volume - 0.1f, _volume + 0.1f));

        }
        else
        {
            _source.PlayOneShot(_noAMmo, Random.Range(_volume - 0.1f, _volume + 0.1f));
            DoRealod();
        }

    }

    private void DoRealod()
    {
        _anime.SetBool("Realod", true);
    }

    public void Reload()
    {
        _ammo = _maxAmmo;
        _anime.SetBool("Realod", false);
    }
}
