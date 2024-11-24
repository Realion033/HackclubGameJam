using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneendminG : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private float secs = Mathf.Infinity;
    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(name);
    }
    private void Awake()
    {
        Invoke(nameof(OnCollisionEnter), secs);
    }

}
