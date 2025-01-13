using TMPro;
using UnityEngine;

public class PlayerName : MonoBehaviour
{
    private TextMeshProUGUI tmp;

    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (PlayerManager.Instance != null)
        {
            tmp.text = PlayerManager.Instance.PlayerName;
        }
        else
        {
            tmp.text = "No Player Name Set";
        }
    }
}
