using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : MonoBehaviour
{
    Vector3 _out = Vector3.zero;
    Vector3 _save = Vector3.zero;
    public GameObject ui;
    bool active = false;
    private void Update()
    {
        if (active)
        {
            if (Keyboard.current[Key.Y].isPressed)
            {
                transform.position = _save;
                GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                active = false;
                ui.SetActive(false);
            }
            else if (Keyboard.current[Key.N].isPressed)
            {
                active = false;
                ui.SetActive(false);
            }
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_out == Vector3.zero)
            {
                _out = transform.position;
                return;
            }
            if (_out.y - transform.position.y > 5f)
            {
                ui.SetActive(true);
                active = true;
                _save = _out;
            }
            _out = transform.position;
        }
    }
}
