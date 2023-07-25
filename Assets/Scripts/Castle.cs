using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    public float maxHealth = 100f;
    private float castleHealth;
    public Slider healthBar;
    public Vector3 healthOffset = new Vector3(0f, 0.5f, 0f);
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log(castleHealth.ToString());
        castleHealth = maxHealth;
        Debug.Log(castleHealth.ToString());
        if (healthBar != null)
        {
            UpdateHealthBar();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            castleHealth -= 10f;
            UpdateHealthBar();
        }
    }

    private void UpdateHealthBar()
    {
        float healthPercentage = castleHealth / maxHealth;
        Debug.Log(healthPercentage
            .ToString());
        healthBar.value = healthPercentage;
        healthBar.transform.position = transform.position + healthOffset;
    }
}
