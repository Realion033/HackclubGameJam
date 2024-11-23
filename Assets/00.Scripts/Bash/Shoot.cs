using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] float _distance,_force;
    [SerializeField] LayerMask _layerMask;
    void Update()
    {
        if (Keyboard.current[Key.F].wasPressedThisFrame)
            {
            Debug.Log("d");
            RaycastHit hit;
            if(Physics.Raycast(transform.position,transform.forward,out hit,_distance,_layerMask))
            {
                if(hit.transform.TryGetComponent<Rigidbody>(out Rigidbody ri))
                {
                    ri.AddForce(transform.forward*_force,ForceMode.Impulse);
                }
            }
        }
    }
}
