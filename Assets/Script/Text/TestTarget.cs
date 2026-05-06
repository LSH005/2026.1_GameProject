using UnityEngine;

public class TestTarget : MonoBehaviour
{
    [SerializeField] private int minDamage = 5;
    [SerializeField] private int maxDamage = 50;
    [SerializeField] private int minHeal = 10;
    [SerializeField] private int maxHeal = 60;
    [SerializeField] private float criticalChance = 0.2f;
    [SerializeField] private float statusEffectChance = 0.15f;
    [SerializeField] private float missChance = 0.15f;

    private string[] statusEffects = { "Burn", "Freeze", "Poison", "Stun" };


    private void ShowDamage(int amount, bool isCritical)
    {
        if (DamageEffectManager.Instance != null)
        {
            Vector3 position = transform.position;
            position += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(1.0f, 1.5f), 0);
            DamageEffectManager.Instance.ShowDamage(position, amount, isCritical);
        }
    }

    private void ShowHeal(int amount, bool isCritical)
    {
        if (DamageEffectManager.Instance != null)
        {
            Vector3 position = transform.position;
            position += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(1.0f, 1.5f), 0);
            DamageEffectManager.Instance.ShowHeal(position, amount, isCritical);
        }
    }

    private void ShowMiss()
    {
        if (DamageEffectManager.Instance != null)
        {
            Vector3 position = transform.position;
            position += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(1.0f, 1.5f), 0);
            DamageEffectManager.Instance.ShowMiss(position);
        }
    }

    private void ShowStatusEffect(string effectName)
    {
        if (DamageEffectManager.Instance != null)
        {
            Vector3 position = transform.position;
            position += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(1.0f, 1.5f), 0);
            DamageEffectManager.Instance.ShowStatusEffect(position, effectName);
        }
    }

    private void OnMouseDown()
    {
        float randomValue = Random.value;

        if (randomValue < missChance) ShowMiss();
        else if (randomValue < 0.5f)
        {
            bool isCritical = Random.value < criticalChance;
            int damage = Random.Range(minDamage, maxDamage + 1);

            if (isCritical) damage *= 2;

            ShowDamage(damage, isCritical);

            if (Random.value < statusEffectChance)
            {
                string effectName = statusEffects[Random.Range(0, statusEffects.Length)];
                ShowStatusEffect(effectName);
            }
        }
        else
        {
            bool isCritical = Random.value < criticalChance;
            int heal = Random.Range(minHeal, maxHeal + 1);

            if (isCritical) heal = Mathf.RoundToInt(heal * 1.5f);
            ShowDamage(heal, isCritical);
        }
    }
}
