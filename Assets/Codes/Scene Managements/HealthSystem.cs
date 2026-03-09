using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth = 80;

    [Header("UI")]
    public Image healthFill;
    public TextMeshProUGUI healthText;

    [Header("Gain and Loss Warning")]
    public Animator GainUIAnim;
    public Animator LossUIAnim;

    void Start()
    {
        UpdateHealthUI();
    }

    // Increase Health
    public void IncreaseHealth(int amount)
    {
        GainUIAnim.gameObject.SetActive(true);
        GainUIAnim.Play("Green Frame Animation", 0, 0f);
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UpdateHealthUI();
    }

    // Decrease Health
    public void DecreaseHealth(int amount)
    {
        LossUIAnim.gameObject.SetActive(true);
        LossUIAnim.Play("Red Frame Animation", 0, 0f);
        currentHealth -= amount;

        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHealthUI();
    }

    // Update UI
    void UpdateHealthUI()
    {
        healthFill.fillAmount = (float)currentHealth / maxHealth;
        healthText.text = currentHealth + "/" + maxHealth;
    }
}