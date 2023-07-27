using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Castle : MonoBehaviour
{
    public float maxHealth = 100f;
    private float castleHealth;
    public Slider healthBar;
    public Vector3 healthOffset = new Vector3(0f, 0.5f, 0f);
    // Start is called before the first frame update
    void Awake()
    {
        castleHealth = maxHealth;
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
            castleHealth -= 50f;
            UpdateHealthBar();
        }
    }

    private void UpdateHealthBar()
    {
        float healthPercentage = castleHealth / maxHealth;
        healthBar.value = healthPercentage;
        healthBar.transform.position = transform.position + healthOffset;
        if (castleHealth <= 0f)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene("GameOver");
            PlayerPrefs.SetString("RetryScene", currentSceneName);
        }
    }
}