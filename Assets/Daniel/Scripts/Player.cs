using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Player : MonoBehaviour , IDamageable
{

    [SerializeField] public int maxHealth = 100;
    [SerializeField] private float invincibilityTime = 0.05f;

    public int CurrentHealth {  get; private set; }

    private float invincibleUntil = 0;

    [SerializeField] private GameObject flashLight;
    [SerializeField] private string flashButton = "P1_Flash";

    public event Action<int, int> OnHealthChanged;
    public event Action OnDied;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        CurrentHealth = maxHealth;
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(flashButton))
        {
            flashLight.SetActive(false);
        }
        else if (Input.GetButtonUp(flashButton))
        {
            flashLight.SetActive(true);
        }
    }

    public void TakeDamage(int amount, object source)
    {
        if (Time.time < invincibleUntil) return;
        if (amount <= 0) return;
        if (CurrentHealth <= 0) return;

        invincibleUntil = Time.time + invincibilityTime;

        CurrentHealth -= amount;
        Debug.Log($"{gameObject.name} HP: {CurrentHealth}/{maxHealth}");
        if (CurrentHealth < 0) CurrentHealth = 0;

        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

        if (CurrentHealth == 0)
        {
            OnDied?.Invoke();
            Die(source);
        }
    }

    public void Die(object source)
    {
        gameObject.SetActive(false);
    }
}
