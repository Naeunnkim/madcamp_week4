using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image healthBar;
    public Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
        enemy = transform.parent.GetComponent<Enemy>();
        updateHealthBar();
    }
    public void updateHealthBar()
    {
        if (enemy != null)
        {
            float fillAmount = enemy.currentHealth / enemy.maxHealth;
            healthBar.fillAmount = fillAmount;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
