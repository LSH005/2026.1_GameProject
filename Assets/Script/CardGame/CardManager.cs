using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;

    public List<CardData> deckCards = new List<CardData>();
    public List<CardData> handCards = new List<CardData>();
    public List<CardData> discardCards = new List<CardData>();

    public GameObject cardPrefabs;
    public Transform deckPosition;
    public Transform handPosition;
    public Transform discardPosition;

    public List<GameObject> cardObjects = new List<GameObject>();

    public CharacterStats playerStats;

    private void Awake()
    {
        if (cardPrefabs != null) Instance = this;
        else
        {
            Debug.LogWarning($"{this.gameObject.name} : 다른 오브젝트가 이미 CardManager를 가지고 있음. ({CardManager.Instance.gameObject.name})");
            this.enabled = false;
        }
    }

    private void Start()
    {
        ShuffleDeck();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            DrawCard();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ReturnDiscardToDeck();
        }
        
        ArrangeHand();
    }

    public void ShuffleDeck()
    {
        List<CardData> tempDeck = new List<CardData>(deckCards);
        deckCards.Clear();

        while (tempDeck.Count > 0)
        {
            int randomIndex = Random.Range(0, tempDeck.Count);
            deckCards.Add(tempDeck[randomIndex]);
            tempDeck.RemoveAt(randomIndex);
        }
    }

    public void DrawCard()
    {
        if (handCards.Count >= 6 || deckCards.Count == 0) return;
        
        CardData cardData = deckCards[0];
        deckCards.RemoveAt(0);

        handCards.Add(cardData);
        GameObject cardObject = Instantiate(cardPrefabs, deckPosition.position, Quaternion.identity);

        if (cardObject.TryGetComponent<CardDisplay>(out CardDisplay cardDisplay))
        {
            cardDisplay.SetupCard(cardData);
            cardDisplay.cardIndex = handCards.Count - 1;
            cardObjects.Add(cardObject);
        }
    }

    public void ArrangeHand()
    {
        if (handCards.Count == 0) return;

        float cardWidth = 1.2f;
        float spaceing = cardWidth + 1.8f;
        float totalWidth = (handCards.Count - 1) * spaceing;
        float startX = -totalWidth / 2f;

        for (int i = 0; i < handCards.Count; i++)
        {
            if (cardObjects[i] != null)
            {
                CardDisplay display = cardObjects[i].GetComponent<CardDisplay>();
                if (display != null && display.isDragging) continue;

                Vector3 targetPosition = handPosition.position + new Vector3(startX + (i * spaceing), 0, 0);
                cardObjects[i].transform.position = Vector3.Lerp(cardObjects[i].transform.position, targetPosition, Time.deltaTime * 10f);
            }
        }
    }

    public void DiscardCard(int handIndex)
    {
        if (handIndex < 0 || handIndex >= handCards.Count) return;

        CardData cardData = handCards[handIndex];
        handCards.RemoveAt(handIndex);
        discardCards.Add(cardData);

        if (cardObjects.Count > handIndex)
        {
            Destroy(cardObjects[handIndex]);
            cardObjects.RemoveAt(handIndex);
        }

        for (int i = 0; i < cardObjects.Count; i++)
        {
            CardDisplay display = cardObjects[i].GetComponent<CardDisplay>();
            if (display != null) display.cardIndex = i;
        }

        ArrangeHand();
    }

    public void ReturnDiscardToDeck()
    {
        if (discardCards.Count == 0) return;

        deckCards.AddRange(discardCards);
        discardCards.Clear();
        ShuffleDeck();
    }
}
