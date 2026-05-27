using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TweenSample : MonoBehaviour
{
    public RectTransform UITarget;
    public Image UIImage;
    public GameObject ObjectTarget;

    public TMP_Text countText;
    public int currentValue = 0;
    public int addValue = 100;
    public Color flashColor;
    public CanvasGroup fadeTarget;
    public GameObject coinPrefab;

    private int targetValue;
    private Color originalColor;

    private void Start()
    {
        originalColor = UIImage.color;
        fadeTarget.alpha = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            PlayPunchUIScale();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            PlayPunchObjectScale();
        if (Input.GetKeyDown(KeyCode.Alpha3))
            PlayUIShake();
        if (Input.GetKeyDown(KeyCode.Alpha4))
            PlayCountUp();
        if (Input.GetKeyDown(KeyCode.Alpha5))
            PlayColorFlash();
        if (Input.GetKeyDown(KeyCode.Alpha6))
            PlayFade();
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Vector3 dropPosition = transform.position + Vector3.up;
            Instantiate(coinPrefab, dropPosition, Quaternion.identity);
        }
    }

    public void PlayPunchUIScale()
    {
        if (UITarget == null) return;
        UITarget.DOKill();                        //이전 실행 중이던 Tween 효과 있으면 정리한다.
        UITarget.localScale = Vector3.one;        //크기가 이상하게 남아 있을 수 있으므로 기본 크기로 초기화
        UITarget.DOPunchScale(Vector3.one * 0.3f, 0.25f, 8, 1.0f); //방향 * 크기 , 시간, 진동 횟수, 탄성
    }

    public void PlayPunchObjectScale()
    {
        if (ObjectTarget == null) return;
        ObjectTarget.transform.DOKill();                        //이전 실행 중이던 Tween 효과 있으면 정리한다.
        ObjectTarget.transform.localScale = Vector3.one;        //크기가 이상하게 남아 있을 수 있으므로 기본 크기로 초기화
        ObjectTarget.transform.DOPunchScale(Vector3.one * 0.3f, 0.25f, 8, 1.0f); //방향 * 크기 , 시간, 진동 횟수, 탄성
    }

    public void PlayUIShake()
    {
        if (ObjectTarget == null) return;
        ObjectTarget.transform.DOKill();                                  //이전 실행 중이던 Tween 효과 있으면 정리한다.
        ObjectTarget.transform.DOShakePosition(0.3f, 10f, 20, 90f);     //시간, 강도, 진동 횟수, 랜덤성
    }

    public void PlayCountUp()
    {
        if (countText == null) return;

        targetValue += addValue;                               //목표 숫자
        DOTween.Kill("CountTween", true);                      //기존 "CountTween" 연출을 완료 한 후 종료 처리

        DOTween.To(
            () => currentValue,                                //현재 값
            value =>                                           //중간 값이 바뀔때 마다 실행 되는 부분
            {
                currentValue = value;
                countText.text = currentValue.ToString();
            },
            targetValue,                                       //목표값
            0.5f                                               //걸리는 시간
        )
        .SetEase(Ease.OutQuad)
        .SetId("CountTween");
    }

    public void PlayColorFlash()
    {
        if (UIImage == null) return;

        UIImage.DOKill();
        UIImage.color = originalColor;
        UIImage.DOColor(flashColor, 0.1f).OnComplete(() => UIImage.DOColor(originalColor, 0.2f));
    }

    public void PlayFade()
    {
        if (fadeTarget == null) return;
        fadeTarget.DOKill();
        fadeTarget.alpha = 0;

        Sequence seq = DOTween.Sequence();           //여러 트윈을 순서대로 실행 할 때 사용 한다.

        seq.Append(fadeTarget.DOFade(1, 0.2f));      //0.2초 동안 나타난다.
        seq.AppendInterval(0.5f);                    //0.5초 유지
        seq.Append(fadeTarget.DOFade(0f, 0.3f));     //0.3초 동안 사라진다.
    }
}
