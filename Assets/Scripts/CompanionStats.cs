using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanionStats : MonoBehaviour
{
    public Canvas Canvas;
    private GameObject companionStats;
    private Slider healthBarSlider;
    private Slider manaBarSlider;
    public int health = 100; //remove '100' value and set from each character's individual scripts
    public int currentHealth = 100;
    public int mana = 100; //remove '100' value and set from each character's individual scripts
    public int currentMana = 100;

    public int companionNumber;

    void Start()
    {
        CloneDisplayUI();
        healthBarSlider = companionStats.transform.GetChild(0).GetComponent<Slider>();
        manaBarSlider = companionStats.transform.GetChild(1).GetComponent<Slider>();
        healthBarSlider.minValue = 0;
        healthBarSlider.maxValue = health;
        manaBarSlider.minValue = 0;
        manaBarSlider.maxValue = mana;
    }

    private void CloneDisplayUI()
    {
        float OffsetX = 100f * (companionNumber - 1) + 190f;
        float OffsetY = -134f;
        Vector2 statDisplayOffset = new Vector2(OffsetX, OffsetY);
        companionStats = Instantiate(Canvas.transform.GetChild(1).gameObject, statDisplayOffset, Quaternion.identity);
        companionStats.transform.SetParent(Canvas.transform, false);
        companionStats.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

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
