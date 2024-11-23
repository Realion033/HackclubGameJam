using UnityEngine;
using UnityEngine.InputSystem;

public class Change : MonoBehaviour
{
    [SerializeField]
    private GameObject _otherPlayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[Key.Tab].wasPressedThisFrame)
        {
            _otherPlayer.transform.position = transform.position + Vector3.up;
            _otherPlayer.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
