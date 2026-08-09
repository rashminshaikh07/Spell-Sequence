
using UnityEngine;

public class ShuffleManager : MonoBehaviour
{
    public Card[] cards;

    private Vector3[] cardPositions;

    private void Start()
    {
        StoreCardPositions();
    }

    private void StoreCardPositions()
    {
        if (cards == null)
        {
            Debug.LogError("ShuffleManager: Cards array is NULL.");
            return;
        }

        cardPositions = new Vector3[cards.Length];

        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i] == null)
            {
                Debug.LogError(
                    "ShuffleManager: Card at array index "
                    + i
                    + " is missing."
                );

                continue;
            }

            cardPositions[i] = cards[i].transform.position;
        }
    }

    public void ShuffleCards()
    {
        if (cards == null || cards.Length == 0)
        {
            Debug.LogError(
                "ShuffleManager: No cards available to shuffle."
            );

            return;
        }

        if (cardPositions == null ||
            cardPositions.Length != cards.Length)
        {
            StoreCardPositions();
        }

        // Fisher-Yates shuffle.
        for (int i = 0; i < cards.Length; i++)
        {
            int randomIndex = Random.Range(i, cards.Length);

            Card temporaryCard = cards[i];

            cards[i] = cards[randomIndex];
            cards[randomIndex] = temporaryCard;
        }

        // Put each shuffled card into a stored position.
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i] == null)
            {
                Debug.LogError(
                    "ShuffleManager: Missing card at shuffled index "
                    + i
                );

                continue;
            }

            cards[i].transform.position = cardPositions[i];
        }
    }
}

