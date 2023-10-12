using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private DamageUI damageUI;

    private void Start()
    {
        currentHealth = maxHealth;
        damageUI = FindObjectOfType<DamageUI>(); // Find the DamageUI in the scene.
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            // Health has reached zero, transition to StartScreen scene
            SceneManager.LoadScene("StartScreen"); // Make sure "StartScreen" is the correct scene name.
        }
        else
        {
            if (damageUI != null)
            {
                damageUI.ShowNextDamageUI();
            }
        }
    }
}
