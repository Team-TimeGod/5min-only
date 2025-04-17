using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.ProBuilder.Shapes;


public class Area_Attack : MonoBehaviour
{
    public GameObject[] cubi; // Array di cubi
    public int danno = 10;  // Quantità di danno che infliggono
    public float raggioAttivazione = 5f; // Raggio entro il quale il cubo si attiva
    public LayerMask layerBersaglio; // Layer del bersaglio (es. giocatore)


    void Update()
    {
        foreach (GameObject cubo in cubi)
        {
            // Calcola la distanza tra il cubo e questo oggetto
            float distanza = Vector3.Distance(transform.position, cubo.transform.position);


            // Se il cubo è entro il raggio di attivazione
            if (distanza <= raggioAttivazione)
            {
                // Controlla se c'è un bersaglio nel raggio
                Collider[] bersagli = Physics.OverlapSphere(cubo.transform.position, raggioAttivazione, layerBersaglio);


                if (bersagli.Length > 0)
                {
                    // Applica il danno a tutti i bersagli trovati
                    foreach (Collider bersaglio in bersagli)
                    {
                        // Ottieni il componente "Salute" del bersaglio (se presente)
                        HealthSystem salute = bersaglio.GetComponent<HealthSystem>();
                        if (salute != null)
                        {
                            salute.TakeDamage(danno);
                            Debug.Log("Cubo ha inflitto " + danno + " danni a " + bersaglio.name);
                        }
                    }
                }
            }
        }
    }
}
