using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _force = 10f, _distance = 5f;
    private void OnEnable()
    {
        Collider[] cols = new Collider[8]; 
        int a =Physics.OverlapSphereNonAlloc(transform.position,_distance,cols);
        for(int i = 0; i < a; i++)
        {
            if(cols[i].TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.AddExplosionForce(_force, transform.position,_distance,0.8f,ForceMode.Impulse);
            }
        }
    }
}
