using UnityEngine;

public class PairManager : MonoBehaviour
{
    public Card firstCard;
    public Card secondCard;

    public bool CheckMatch()
    {
        if (firstCard == null || secondCard == null)
        {
            return false;
        }

        return firstCard.cardID == secondCard.cardID;
    }

    public void SelectCard(Card selectedCard)
    {
        if (selectedCard == null)
        {
            return;
        }

        if (selectedCard == firstCard)
        {
            return;
        }

        if (firstCard == null)
        {
            firstCard = selectedCard;
        }
        else if (secondCard == null)
        {
            secondCard = selectedCard;
        }
    }
    public void ClearSelection()
    {
        firstCard = null;
        secondCard = null;
    }
    public bool HasTwoCards()
    {
        return firstCard != null && secondCard != null;
    }
}