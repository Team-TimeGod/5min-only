using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Variabili per la vita")]
    [SerializeField] private float MaxHealth; //player = 300(5 min)
    [SerializeField] private float CurrentHealth; //player = secondi - nemico = pf

    [Header("Tipo di character")]
    [SerializeField] private CharacterType _characterType;

    /*[Header("Main Camera")]
    [SerializeField] private Camera _mainCamera;
    */
    [Header("Script Interagibili")]
    [SerializeField] private showCanvas _SC;

    [Header("DEBUG")]
    public bool DEBUGMODE;

    enum CharacterType : byte
    {
        Player = 0,
        Enemy = 1
    }

    public void Awake() //appena il gameObject viene caricato
    {
        CurrentHealth = MaxHealth; //setto la currentHealth uguale a MaxHealth
        if (_characterType == CharacterType.Player) //se è il player
        {
            //_mainCamera = GetComponentInChildren<Camera>(); //trova la camera associata
            _SC = GameObject.Find("Manager").GetComponent<showCanvas>();
            
        }
    }

    public void Start()
    {
        _SC.hide();
    }

    public void Update()
    {
        if (_characterType == CharacterType.Player) //Se il tipo selezionato è player
        {
            CurrentHealth -= Time.deltaTime; //rimuove 1 unità ogni secondo di gioco 
        }

        if (CurrentHealth <= 0 && !DEBUGMODE) //se la vita va a 0
        {
            if (_characterType == CharacterType.Player) //Se è il player
            {
                //_mainCamera.transform.SetParent(null); //Stacca la camera dal player, altrimenti viene distrutta dalla riga di codice successiva
                _SC.show(); //Mostro il menu di gameover
            }
            Destroy(this.gameObject); //Distruggi questo gameObject
        }



        //DEBUG
        /*
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(100);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestoreLife(100);
        }
        */
    }

    public void TakeDamage(float _damage)
    {
        CurrentHealth -= _damage; //Sottrai _damage alla vita
    }

    public void RestoreLife(float _lifeToAdd)
    {
        float TMPLife; //dichiaro una variabile temporanea chiamata TMPLife
        TMPLife = CurrentHealth + _lifeToAdd; //assegno il valore di CurrentHealth + _timeToAdd alla variabile di supporto
        if (TMPLife > MaxHealth) //TMPLife super la vita massima
        {
            CurrentHealth = MaxHealth; // la vita corrente è uguale alla vita massima
        }
        else //se invece è minore
        {
            CurrentHealth = TMPLife; //Assegno TMPLife alla vita attuale. 
        }
    }

    public float getLife()
    {
        return CurrentHealth;
    }




}





//Time.deltaTime funziona con Time.timeScale
//Time.unscaledDeltaTime non funziona con Time.timeScale, funziona arbitrariamente