using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    // Call this method from your UI Button's OnClick event
    public void OnExitButtonPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
