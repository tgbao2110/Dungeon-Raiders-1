using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPanel : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI energy;
    
    public void SetWeapon(WeaponData weaponData)
    {
        Utility.SetSprite(image, weaponData.sprite);
        image.color = Color.white;
        energy.text = weaponData.energy.ToString();
    }

    
}
