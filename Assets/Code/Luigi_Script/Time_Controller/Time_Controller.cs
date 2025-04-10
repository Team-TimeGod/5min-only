using UnityEngine;

public class Time_Controller : MonoBehaviour
{
    public static Time_Controller Instance { get; private set; }

    [Header("Time Settings")]
    [SerializeField] private float timeStopDuration = 5f;
    [SerializeField] private float timeSlowDuration = 5f;  // Duration to slow down the time
    [SerializeField] private float timeFastDuration = 5f;  // Duration to speed up the time
    [SerializeField] private float cooldownDuration = 10f;  // Cooldown for all actions
    [SerializeField] private float _FFMult;  // Time x mult
    [SerializeField] private float _SlowMult;  // Slow Mult

    private bool isOnCooldown = false;
    private float timeStopTimer = 0f;
    private float timeSlowTimer = 0f;
    private float timeFastTimer = 0f;
    private float cooldownTimer = 0f;

    public bool IsTimeStopped { get; private set; } = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        // Check if the player is trying to stop time
        if (Input.GetKeyDown(KeyCode.T) && !IsTimeStopped && !isOnCooldown)
        {
            ActivateTimeStop();
        }

        // Check if the player is trying to slow down the time
        if (Input.GetKeyDown(KeyCode.R) && !isOnCooldown)
        {
            SetTimeSpeed(_SlowMult, timeSlowDuration);  // Slow down the time
        }

        // Check if the player is trying to speed up the time
        if (Input.GetKeyDown(KeyCode.F) && !isOnCooldown)
        {
            SetTimeSpeed(_FFMult, timeFastDuration);  // Speed up the time
        }

        // Manage the timer for stopping time
        if (IsTimeStopped)
        {
            timeStopTimer -= Time.unscaledDeltaTime;
            if (timeStopTimer <= 0f)
            {
                ResumeTime();
                StartCooldown();
            }
        }

        // Manage the timer for slow speed
        if (timeSlowTimer > 0f)
        {
            timeSlowTimer -= Time.unscaledDeltaTime;
            if (timeSlowTimer <= 0f)
            {
                ResumeTime();
                StartCooldown();
            }
        }

        // Manage the timer for fast speed
        if (timeFastTimer > 0f)
        {
            timeFastTimer -= Time.unscaledDeltaTime;
            if (timeFastTimer <= 0f)
            {
                ResumeTime();
                StartCooldown();
            }
        }

        // Manage the cooldown for all actions
        if (isOnCooldown)
        {
            cooldownTimer -= Time.unscaledDeltaTime;
            if (cooldownTimer <= 0f)
            {
                isOnCooldown = false;
                Debug.Log("Time recharged, you can use it again!");
            }
        }
    }

    private void ActivateTimeStop()
    {
        Time.timeScale = 0f;  // Stop the time
        IsTimeStopped = true;
        timeStopTimer = timeStopDuration;  // Set the duration for stopping time
        Debug.Log("Time stopped!");
    }

    private void SetTimeSpeed(float scale, float duration)
    {
        Time.timeScale = scale;  // Set the time speed (0.5 to slow it down, 2 to speed it up)
        timeSlowTimer = duration;  // Slow down the time for the specified duration
       

        Debug.Log($"Time speed set to {scale}x for {duration} seconds.");
    }

    private void ResumeTime()
    {
        Time.timeScale = 1f;  // Restore the normal time speed
        IsTimeStopped = false;
        Debug.Log("Time resumed.");
    }

    private void StartCooldown()
    {
        isOnCooldown = true;
        cooldownTimer = cooldownDuration;  // Set the cooldown for all actions
    }
}
