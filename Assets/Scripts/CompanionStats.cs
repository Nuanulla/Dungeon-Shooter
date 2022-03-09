using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanionStats : MonoBehaviour
{
    private SpriteRenderer sprite;
    public Canvas Canvas;
    private GameObject companionStats;
    private Slider healthBarSlider;
    private Slider manaBarSlider;
    public int health = 100; //this is a default value. Set actual value from each entity's individual scripts
    public int currentHealth;
    public int mana = 100; //this is a default value. Set actual value from each entity's individual scripts
    public int currentMana;

    public int companionNumber;

    public Image image;

    void Start()
    {   
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void CloneDisplayOverlay()
    {
        float OffsetX = 107f * (companionNumber - 1) + 195f;
        float OffsetY = -92f;
        Vector2 statDisplayOffset = new Vector2(OffsetX, OffsetY);
        companionStats = Instantiate(Canvas.transform.GetChild(1).gameObject, statDisplayOffset, Quaternion.identity);
        companionStats.transform.SetParent(Canvas.transform, false);
        companionStats.SetActive(true);

        healthBarSlider = companionStats.transform.GetChild(0).GetComponent<Slider>();
        manaBarSlider = companionStats.transform.GetChild(1).GetComponent<Slider>();
        healthBarSlider.minValue = 0;
        healthBarSlider.maxValue = health;
        manaBarSlider.minValue = 0;
        manaBarSlider.maxValue = mana;
        currentHealth = health;
        currentMana = mana;

        image = companionStats.GetComponent<Image>();
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
        StartCoroutine(FlashRed());
    }

    public void SapMana(int cost)
    {
        currentMana -= cost;
    }

    public IEnumerator FlashRed()
    {
        sprite.color = new Color(0.5f, 0f, 0f, 1f);
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
