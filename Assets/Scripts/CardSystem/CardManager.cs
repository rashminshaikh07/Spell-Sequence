using UnityEngine;
using System.Collections;

public class CardManager : MonoBehaviour
{
    public Card[] cards;
    public PairManager pairManager;
    public ShuffleManager shuffleManager;

    private bool canSelectCards = true;

    public int CardCount
    {
        get
        {
            return cards != null ? cards.Length : 0;
        }
    }

    private void Start()
    {
        Debug.Log("CardManager initialized with " + CardCount + " cards.");
    }

    public void ShuffleCards()
    {
        if (shuffleManager != null)
        {
            shuffleManager.ShuffleCards();
        }
    }

    public void SelectCard(Card selectedCard)
    {
        if (!canSelectCards)
        {
            return;
        }

        if (selectedCard == null)
        {
            return;
        }

        if (pairManager == null)
        {
            Debug.LogError("PairManager is not assigned.");
            return;
        }

        // Do not allow the same card to be selected twice.
        if (selectedCard == pairManager.firstCard)
        {
            return;
        }

        // Store the selected card.
        pairManager.SelectCard(selectedCard);

        // Flip the selected card.
        selectedCard.FlipCard();

        // When two cards are selected, stop further clicks.
        if (pairManager.HasTwoCards())
        {
            canSelectCards = false;

            StartCoroutine(ProcessPair());
        }
    }

    private IEnumerator ProcessPair()
    {
        // Wait until the second card finishes flipping.
        yield return new WaitForSeconds(0.6f);

        bool isMatch = pairManager.CheckMatch();

        Debug.Log(
            "Pair processed. First ID: "
            + pairManager.firstCard.cardID
            + " Second ID: "
            + pairManager.secondCard.cardID
            + " Match: "
            + isMatch
        );

        if (isMatch)
        {
            // Matching cards stay face-up.
            pairManager.ClearSelection();
            canSelectCards = true;
        }
        else
        {
            // Save references before clearing the selection.
            Card firstCard = pairManager.firstCard;
            Card secondCard = pairManager.secondCard;

            // Flip both cards back.
            if (firstCard != null)
            {
                firstCard.FlipCard();
            }

            if (secondCard != null)
            {
                secondCard.FlipCard();
            }

            // Wait for flip-back animation.
            yield return new WaitForSeconds(0.6f);

            pairManager.ClearSelection();
            canSelectCards = true;
        }
    }

    public void SetCardSelection(bool canSelect)
    {
        canSelectCards = canSelect;
    }

    public void EnableCardSelection()
    {
        canSelectCards = true;
    }

    public bool IsCurrentPairMatch()
    {
        if (pairManager == null)
        {
            return false;
        }

        return pairManager.CheckMatch();
    }

    public void ProcessSelectedPair()
    {
        if (pairManager == null)
        {
            return;
        }

        if (!pairManager.HasTwoCards())
        {
            return;
        }

        bool isMatch = pairManager.CheckMatch();

        Debug.Log("Pair processed. Match: " + isMatch);
    }
}