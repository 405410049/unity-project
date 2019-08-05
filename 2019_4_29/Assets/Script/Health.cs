using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{
    public const int maxHealth = 100;
    [SyncVar]public int currentHealth = maxHealth;
    public RectTransform healthbar;

    private void Start()
    {
        healthbar = healthbar.GetComponent<RectTransform>();
        if (healthbar != null)
        {
            print("found bar");
        }
        else
        {
            print("first not found");
        }
    }
    private void Update()
    {
        if (isLocalPlayer)
        {
            onChangeHealth(currentHealth);
        }
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public void setCurrentHealth(int tmp)
    {
        currentHealth = tmp;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Dead!");
        }
        onChangeHealth(currentHealth);
    }

    public void setBarSize(int tmp)
    {
        healthbar.sizeDelta = new Vector2(tmp * 2, healthbar.sizeDelta.y);
    }

    void onChangeHealth(int health)
    {
        healthbar.sizeDelta = new Vector2(currentHealth * 2, healthbar.sizeDelta.y);
    }
}
