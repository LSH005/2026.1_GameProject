using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStats : MonoBehaviour
{
    public string characterName;
    public int maxHealth = 100;
    public int currentHealth;

    // UI 요소
    public Slider healthBar;
    public TextMeshProUGUI healthText;

    public int maxMana = 10;
    public int currentMana;
    public Slider manaBar;
    public TextMeshProUGUI manaText;

    bool goofyAhhDeath = false;

    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        UpdateUi();
    }

    private void Update()
    {
        if (goofyAhhDeath)
        {
            transform.Rotate(Random.onUnitSphere * 180 * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Death();
        }
        UpdateUi();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        UpdateUi();
    }

    public void UseMana(int amount)
    {
        currentMana -= amount;
        currentMana = Mathf.Max(currentMana, 0);
        UpdateUi();
    }

    public void GainMana(int amount)
    {
        currentMana += amount;
        currentMana = Mathf.Min(currentMana, maxMana);
        UpdateUi();
    }

    void UpdateUi()
    {
        if (healthBar != null) healthBar.value = (float)currentHealth / maxHealth;
        if (healthText != null) healthText.text = $"{currentHealth} / {maxHealth}";
        if (manaBar != null) manaBar.value = (float)currentMana / maxMana;
        if (manaText != null) manaText.text = $"{currentMana} / {maxMana}";
    }

    void Death()
    {
        goofyAhhDeath = true;
    }
}
