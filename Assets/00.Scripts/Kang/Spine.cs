using DG.Tweening;
using UnityEngine;

public class Spine : MonoBehaviour
{
    void Start()
    {
        Hit();
    }
    void Hit()
    {
        
        transform.DOLocalMove(Vector3.up * -214.21f, 1f).OnComplete(() => transform.DOLocalMove(Vector3.up * -1.9f, 0.3f).SetDelay(2f).OnComplete(() => Hit()));
    }
}
