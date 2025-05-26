using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float hitPoints;
    public float maxHitPoints;
    public bool hasHealthbar;
    [SerializeField] private Image healthbarSprite;
    [SerializeField] private Canvas healthbarCanvas;
    private Camera cam;
    private void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        if (hitPoints <= 0)
        {
            Die();
        } else if (hitPoints > maxHitPoints)
        {
            hitPoints = maxHitPoints;
        }

        if (hasHealthbar)
        {
            healthbarCanvas.transform.rotation = Quaternion.LookRotation(transform.position  - cam.transform.position);
            UpdateHealthBar(maxHitPoints, hitPoints);
        }
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthbarSprite.fillAmount = currentHealth / maxHealth;
    }
    void Die()
    {
        if (gameObject.GetComponent<PlayerController>() != null)
        {
            gameObject.GetComponent<PlayerController>().enabled = false;
        } else
        {
            Destroy(gameObject);
        }
    } 
}
