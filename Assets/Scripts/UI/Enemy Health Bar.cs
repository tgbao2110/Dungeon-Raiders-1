using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MiniHealthBar
{
    [SerializeField] Vector3 offset;
    private void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }

}
