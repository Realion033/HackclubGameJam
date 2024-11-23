using UnityEngine;

public class Tnt : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField] private GameObject _gameObject;
    private void OnCollisionEnter(Collision collision)
    {
        _animator.SetTrigger("trigger");
    }
    public void Ex()
    {
        Instantiate(_gameObject,transform.position,transform.rotation);
        Destroy(gameObject);
    }
}
