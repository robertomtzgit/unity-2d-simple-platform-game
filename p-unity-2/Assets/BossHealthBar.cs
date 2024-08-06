using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    private Slider slider;
    
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void ChanceMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
    }

    public void ChanceActualHealth(float actualHealth)
    {
        slider.value = actualHealth;
    }

    public void StartBossHealthBar(float healthValue)
    {
        ChanceMaxHealth(healthValue);
        ChanceActualHealth(healthValue);
    }
}
