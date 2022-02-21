using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{

    private Vector3 currentPos;

    public Canvas Canvas;
    private GameObject companionHealth;
    private GameObject companionMana;
    private Slider healthBarSlider;
    private Slider manaBarSlider;
    public int health = 100; //remove '100' value and set from each enemy type's individual scripts
    public int currentHealth = 100;
    public int mana = 100;
    public int currentMana = 100;

    void Start()
    {
        companionHealth = Instantiate(Canvas.transform.GetChild(1).gameObject);
        companionHealth.transform.SetParent(Canvas.transform, false);
        healthBarSlider = companionHealth.GetComponent<Slider>();
        companionHealth.SetActive(true);
        healthBarSlider.minValue = 0;
        healthBarSlider.maxValue = health;

        companionMana = Instantiate(Canvas.transform.GetChild(2).gameObject);
        companionMana.transform.SetParent(Canvas.transform, false);
        manaBarSlider = companionMana.GetComponent<Slider>();
        companionMana.SetActive(true);
        manaBarSlider.minValue = 0;
        manaBarSlider.maxValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = transform.position;

        // Update Health Bar with value of currentHealth at each frame
        healthBarSlider.value = currentHealth;

        // Update Mana Bar with value of currentMana at each frame
        manaBarSlider.value = currentMana;

        if (currentHealth <= 0) // If health has been entirely depleted
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Destroy(companionHealth);
        Destroy(companionMana);
    }

    private void FixedUpdate()
    {
        companionHealth.transform.position = new Vector3(currentPos.x, currentPos.y - 1f, currentPos.z);
        companionMana.transform.position = new Vector3(currentPos.x, currentPos.y - 1.25f, currentPos.z);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void SapMana(int cost)
    {
        currentMana -= cost;
    }
}
