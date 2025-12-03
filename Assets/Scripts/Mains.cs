using UnityEngine;
using UnityEngine.SceneManagement;

public class Mains : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ...existing code...
    }

    // Update is called once per frame
    void Update()
    {
        // ...existing code...
    }

    // Call this from a UI button to load the Mains scene
    public void LoadMainsScene()
    {
        SceneManager.LoadScene("Mains");
    }
}
