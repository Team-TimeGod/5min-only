using UnityEngine;
using UnityEngine.SceneManagement;

public class Platform_Elevator : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float height = 10f;

    private bool goingUp = false;
    private Vector3 targetPos;
    private bool playerOnPlatform = false;

    public void StartElevator()
    {
        // Impostiamo la destinazione quando il giocatore ci sale sopra
        if (playerOnPlatform)
        {
            goingUp = true;
            targetPos = transform.position + Vector3.up * height;
        }
    }

    void Update()
    {
        // Iniziamo a muovere la piattaforma solo se il giocatore è sopra
        if (goingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            // Se la piattaforma ha raggiunto la sua destinazione
            if (transform.position == targetPos)
            {
                goingUp = false; // Fermiamo il movimento
            }
        }
    }

    // Attiva il movimento della piattaforma quando il player entra nel trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assicurati che il player abbia il tag "Player"
        {
            playerOnPlatform = true;
            StartElevator();
            SceneManager.LoadScene("SCN_Win");
        }
    }

    // Fermiamo il movimento se il player lascia la piattaforma
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false;
        }
    }
}
