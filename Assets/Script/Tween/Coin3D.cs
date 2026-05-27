using DG.Tweening;
using UnityEngine;

public class Coin3D : MonoBehaviour
{
    public int moneyAmount = 10;
    bool isPicked = false;

    private void Start()
    {
        Vector3 randomPosition = transform.position + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));

        transform.DOJump(randomPosition, 1.2f, 1, 0.4f).SetLink(gameObject);      //SetLink 오브젝트가 없어질때 같이 사라짐
        transform.DORotate(new Vector3(0f, 360f, 0f), 0.4f, RotateMode.FastBeyond360).SetLink(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPicked) return;

        MoneyUI moneyUI = FindFirstObjectByType<MoneyUI>();

        if (moneyUI != null)
        {
            moneyUI.GetMoney(moneyAmount, transform.position);
        }

        transform.DOKill();
        Destroy(gameObject);
    }
}