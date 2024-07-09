using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    [SerializeField] GameObject effect;
    public void Enable()
    {
        effect.SetActive(true);
    }
    public void Disable()
    {
        effect.SetActive(false);
    }
}
