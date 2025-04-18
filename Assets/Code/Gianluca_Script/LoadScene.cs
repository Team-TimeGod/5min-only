using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public void Quit()
    {
        Application.Quit();
    }

    public void MainLevel()
    {
        SceneManager.LoadScene("SCN_Egypt");
    }


}
