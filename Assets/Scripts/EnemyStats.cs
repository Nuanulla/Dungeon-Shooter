using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{

    private Vector3 currentPos;

    public Canvas Canvas;
    private GameObject enemyHealth;
    private Slider healthBarSlider;
    public int health = 100; //remove '100' value and set from each enemy type's individual scripts
    public int currentHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = Instantiate(Canvas.transform.GetChild(0).gameObject);
        enemyHealth.transform.SetParent(Canvas.transform, false);
        healthBarSlider = enemyHealth.GetComponent<Slider>();
        enemyHealth.SetActive(true);
        healthBarSlider.minValue = 0;
        healthBarSlider.maxValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = transform.position;

        // Update Health Bar with value of currentHealth at each frame
        healthBarSlider.value = currentHealth;

        if (currentHealth <= 0) // If health has been entirely depleted
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Destroy(enemyHealth);
    }

    private void FixedUpdate()
    {
        enemyHealth.transform.position = new Vector3(currentPos.x, currentPos.y - 1f, currentPos.z);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
