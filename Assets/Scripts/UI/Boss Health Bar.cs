using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossHealthBar : MiniHealthBar
{
    [SerializeField] TextMeshProUGUI enemyName;

    public override void SetHealth(string name, int health, int maxHealth)
    {
        base.SetHealth(name, health, maxHealth);
        enemyName.text = name;
    }
}
