using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Debug.Log("QUIT");
    }
}
