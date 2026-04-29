using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card/Card Data")]
public class CardData : ScriptableObject
{
    public enum CardType
    {
        Attack,     //공격 카드
        Heal,       //회복 카드
        Buff,       //버프 카드
        Utility     //유틸리티 카드
    }

    public string cardName;        //카드 이름
    public string description;     //카드 설명
    public Sprite artwork;         //카드 이미지
    public int manaCost;           //마나 비용
    public int effectAmount;       //효과 값 (공격력)
    public CardType cardType;      //카드 타입

    public List<AdditionalEffect> additionalEffects = new List<AdditionalEffect>();

    public enum AdditionalEffectType
    {
        None,
        DrawCard,
        DiscardCard,
        GainMana,
        ReduceEnemyMana,
        ReduceCardCost
    }

    public Color GetCardColor()    //타입에 따른 카드 색상
    {
        switch (cardType)
        {
            case CardType.Attack:
                return new Color(0.9f, 0.3f, 0.3f);     //빨강

            case CardType.Heal:
                return new Color(0.3f, 0.9f, 0.3f);     //녹색

            case CardType.Buff:
                return new Color(0.9f, 0.3f, 0.9f);     //파랑

            case CardType.Utility:
                return new Color(0.9f, 0.9f, 0.3f);     //노랑

            default:
                return Color.white;
        }
    }

    public string GetAdditionalEffectDescription()
    {
        if (additionalEffects.Count == 0)
            return "";

        string result = "\n";

        foreach (var effect in additionalEffects)
        {
            result += effect.GetDescription() + "\n";
        }

        return result;
    }
}