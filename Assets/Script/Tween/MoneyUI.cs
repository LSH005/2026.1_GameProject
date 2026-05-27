using DG.Tweening;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    public Canvas canvas;

    public RectTransform coinIconPrefab;
    public RectTransform coinTarget;
    public TMP_Text moneyText;

    public Color flashColor = Color.yellow;
    public float flyTime = 0.5f;
    private int money = 0;

    private Color originalColor;

    private void Start()
    {
        moneyText.text = money.ToString();
        originalColor = moneyText.color;
    }

    public void GetMoney(int amount, Vector3 worldPosition)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        RectTransform coinIcon = Instantiate(coinIconPrefab, canvas.transform);
        coinIcon.position = screenPosition;
        coinIcon.localScale = Vector3.one;

        coinIcon.DOMove(coinTarget.position, flyTime).SetEase(Ease.InBack).OnComplete(() =>
            {
                Destroy(coinIcon.gameObject);
                money += amount;
                moneyText.text = money.ToString();
                PlayMoneyEffect();
            });
    }

    public void PlayMoneyEffect()
    {
        moneyText.transform.DOKill();
        moneyText.DOKill();
        moneyText.transform.localScale = Vector3.one;
        moneyText.transform.DOPunchScale(Vector3.one * 0.3f, 0.2f);

        moneyText.DOColor(flashColor, 0.1f).OnComplete(() => moneyText.DOColor(originalColor, 0.2f));
    }
}
