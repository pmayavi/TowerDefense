using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    static public PlayerStats stats;
    public GameObject player;
    public GameObject healthBar;
    public Slider healthBarSlider;
    public Text healthBarText;

    public Text speedText,
        dmgProyectileText,
        sizeProyectileText,
        speedProyectileText;
    public Text dmgSwordText,
        sizeSwordText,
        speedSwordText,
        pointsText;

    protected float health;
    public float maxHealth,
        speed;
    public float proyectileDamage,
        proyectileSpeed,
        proyectileSize;
    public float swordDamage,
        swordSpeed,
        swordSize,
        point;

    void Awake()
    {
        if (stats != null)
            Destroy(stats);
        else
            stats = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        point = 0;
        health = maxHealth;
        player.GetComponent<PlayerMovement>().speed = speed;
        player.GetComponent<SpellScript>().damage = proyectileDamage;
        player.GetComponent<SpellScript>().force = proyectileSpeed;
        player.GetComponent<SpellScript>().size = proyectileSize;
        player.GetComponent<PlayerMovement>().damage = swordDamage;
        //player.GetComponent<SwordScript>().attackSpeed = swordSpeed;
        //player.GetComponent<SwordScript>().attackSize = swordSize;
        Physics2D.IgnoreLayerCollision(10, 11, true);
    }

    void Update()
    {
        UIScreen();
    }

    public void Hurt(float dmg)
    {
        health -= dmg;
        Death();
    }

    public void Heal(float heal)
    {
        health += heal;
        if (health > maxHealth)
            health = maxHealth;
        Death();
    }

    public void Death()
    {
        if (health <= 0)
            Destroy(player);
    }

    private void UIScreen()
    {
        healthBarSlider.value = (health / maxHealth);
        healthBarText.text = health + " / " + maxHealth;
        speedText.text = "Velocidad: " + speed;
        dmgProyectileText.text = "Daño Proyectil: " + proyectileDamage;
        sizeProyectileText.text = "Tamaño Proyectil: " + proyectileSize;
        speedProyectileText.text = "Velocidad Proyectil: " + proyectileSpeed;
        dmgSwordText.text = "Daño Espada: " + swordDamage;
        sizeSwordText.text = "Tamaño Espada: " + swordSize;
        speedSwordText.text = "Velocidad Espada: " + swordSpeed;
        pointsText.text = point + "";
    }

    public void SetSpeed(float spd)
    {
        speed *= spd;
        //if (speed < 1) speed = 1;
        player.GetComponent<PlayerMovement>().speed = speed;
    }

    public void SetProyectileDamage(float dmg)
    {
        proyectileDamage *= dmg;
        //if (proyectileDamage < 1) proyectileDamage = 1;
        player.GetComponent<SpellScript>().damage = proyectileDamage;
    }

    public void SetProyectileSpeed(float spd)
    {
        proyectileSpeed *= spd;
        //if (proyectileSpeed < 1) proyectileSpeed = 1;
        player.GetComponent<SpellScript>().force = proyectileSpeed;
    }

    public void SetProyectileSize(float size)
    {
        proyectileSize *= size;
        //if (proyectileSize < 0.05f) proyectileSize = 0.05f;
        player.GetComponent<SpellScript>().size = proyectileSize;
    }

    public void SetSwordDamage(float dmg)
    {
        swordDamage *= dmg;
        //if (swordDamage < 1) swordDamage = 1;
        player.GetComponent<SwordScript>().damage = swordDamage;
    }

    public void SetSwordSpeed(float spd)
    {
        swordSpeed *= spd;
        //if (swordSpeed < 1) swordSpeed = 1;
        player.GetComponent<SwordScript>().attackSpeed = swordSpeed;
    }

    public void SetSwordSize(float size)
    {
        swordSize *= size;
        //if (swordSize < 0.05f) swordSize = 0.05f;
        player.GetComponent<SwordScript>().attackSize = swordSize;
    }

    public void Point(float p)
    {
        point += p;
    }
}
