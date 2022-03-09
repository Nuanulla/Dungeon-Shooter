using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{

    private Vector3 currentPos;
    private SpriteRenderer sprite;
    public Canvas Canvas;
    private GameObject enemyHealth;
    private Slider healthBarSlider;
    public int health; //this is a default value. Set actual value from each entity's individual scripts
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void CloneStatOverlay()
    {
        enemyHealth = Instantiate(Canvas.transform.GetChild(0).gameObject);
        enemyHealth.transform.SetParent(Canvas.transform, false);
        healthBarSlider = enemyHealth.GetComponent<Slider>();
        enemyHealth.SetActive(true);
        healthBarSlider.minValue = 0;
        healthBarSlider.maxValue = health;
        currentHealth = health;
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
        StartCoroutine(FlashRed());
    }

    public void RecoverHealth(int recovery)
    {
        if (currentHealth < health)
        {
            currentHealth += recovery;
        }
    }

    public IEnumerator FlashRed()
    {
        sprite.color = new Color(0.5f, 0f, 0f, 1f);
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
