using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour
{
    public Move plar;
    public GameObject camera; //камера смэрти
    public int Health;
    public int wounds;
    public int maxHealth;
    int maxHealth2;
    int HealthMinus;

    public int Stamina;
    public int maxStamina;
    int maxStamina2;
    int tired;
    public int StaminaMinus;
    float sprint;
    float speedplar;
    float time;
    public bool blok;
    float blokspeed;

    public int Regen;
    int Regen2;
    public float RegrnTime;
    public float RegrnTime2;

    public Slider slider1;
    public Slider slider2;
    public Slider slider3;
    public Slider slider4;

    void Start()
    {
        maxHealth = Health;
        maxHealth2 = Health;
        HealthMinus = Health/5;
        wounds = Health * 2;

        maxStamina = Stamina;
        maxStamina2 = Stamina;
        tired = Stamina*3;
        StaminaMinus = Stamina/5;

        sprint = plar.speed*2;
        speedplar = plar.speed;
        blokspeed = plar.speed / 2;
    }

    void Update()
    {
        int e = maxHealth - HealthMinus;
        if (wounds <= 0 && e >= 20)
        {
            maxHealth -= HealthMinus;
            wounds = maxHealth2 * 2;
        }

        if (Health <= 0)
        {
            Application.LoadLevel(0);
        }
        if (Health > maxHealth)
        {
            Health = maxHealth;
        }

        if (RegrnTime > 0)
        {
            RegrnTime -= Time.deltaTime;
            if(RegrnTime < RegrnTime2)
            {
                Health += Regen;
                RegrnTime2 = RegrnTime - 1;
            }
        }
        else
        {
            RegrnTime = 0;
            RegrnTime2 = 0;  
            Regen = 0;
        }


        if (Input.GetKey(KeyCode.LeftShift) && Stamina > 0 && blok == false)
        {
            Stamina = Stamina - 2;
            tired = tired - 2;
            plar.speed = sprint;
            time = 0f;
        }
        else if (blok)
        {
            plar.speed = blokspeed;
        }
        else
        {
            time += 0.1f;
            plar.speed = speedplar;
            if (time > 30 && blok == false)
            {
                Stamina = Stamina + 1;
            }
        }

        int u = maxStamina - StaminaMinus;
        if (tired <= 0 && u >= 80)
        {
            maxStamina -= StaminaMinus;
            tired = maxStamina2 * 3;
        }

        if (Stamina >= maxStamina)
        {
            Stamina = maxStamina;
        }
        if (Stamina <= 0)
        {
            Stamina = 0;
        }


        if(maxHealth > maxHealth2)
        {
            maxHealth = maxHealth2;
        }
        if (maxStamina > maxStamina2)
        {
            maxStamina = maxStamina2;
        }

        slider1.value = Health;
        slider2.value = Stamina;
        slider3.value = maxStamina2 - maxStamina;
        slider4.value = maxHealth2 - maxHealth;

    }
}