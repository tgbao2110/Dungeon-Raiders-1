using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    private int coinCount = 0;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        coinText.text = GameManager.Instance.GetCoinCount().ToString();
    }


    public void UpdateCoinUI()
    {
        coinCount++;
    }
}
