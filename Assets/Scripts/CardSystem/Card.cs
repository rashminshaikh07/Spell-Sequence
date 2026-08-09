
using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour
{
    public int cardID;
    public CardManager cardManager;

    private Renderer cardRenderer;

    public Material frontMaterial;
    public Material backMaterial;

    private bool isFaceUp = false;
    private bool isFlipping = false;

    private static bool shuffleStarted = false;

    private void Awake()
    {
        // =====================================================
        // CARD MANAGER
        // =====================================================

        if (cardManager == null)
        {
            cardManager = FindFirstObjectByType<CardManager>();
        }

        // =====================================================
        // FIND THIS CARD'S RENDERER
        // =====================================================

        cardRenderer = GetComponent<Renderer>();

        if (cardRenderer == null)
        {
            cardRenderer =
                GetComponentInChildren<Renderer>(true);
        }

        if (cardRenderer == null)
        {
            Debug.LogError(
                "Card " + cardID +
                ": No Renderer found. Card cannot display."
            );

            return;
        }

        // =====================================================
        // CRITICAL:
        // KEEP THE CARD RENDERER ENABLED.
        // =====================================================

        cardRenderer.enabled = true;

        // =====================================================
        // START WITH ACTUAL BACK MATERIAL
        // =====================================================

        ShowBack();
    }

    private void Start()
    {
        // Make absolutely sure card is visible.
        if (cardRenderer != null)
        {
            cardRenderer.enabled = true;
        }

        ShowBack();

        // =====================================================
        // AUTOMATIC SHUFFLE
        // =====================================================

        if (!shuffleStarted)
        {
            shuffleStarted = true;

            StartCoroutine(StartShuffle());
        }
    }

    private IEnumerator StartShuffle()
    {
        // Wait one frame so ShuffleManager has stored
        // the original card positions first.
        yield return null;

        ShuffleManager shuffleManager =
            FindFirstObjectByType<ShuffleManager>();

        if (shuffleManager != null)
        {
            shuffleManager.ShuffleCards();

            Debug.Log("Cards shuffled automatically.");
        }
        else
        {
            Debug.LogWarning(
                "ShuffleManager not found."
            );
        }
    }

    // =====================================================
    // CLICK
    // =====================================================

    private void OnMouseDown()
    {
        if (isFlipping)
        {
            return;
        }

        if (isFaceUp)
        {
            return;
        }

        if (cardManager == null)
        {
            cardManager =
                FindFirstObjectByType<CardManager>();
        }

        if (cardManager == null)
        {
            Debug.LogError(
                "CardManager not found for Card ID "
                + cardID
            );

            return;
        }

        Debug.Log(
            "CARD CLICKED - ID: "
            + cardID
        );

        // Existing flow remains unchanged.
        cardManager.SelectCard(this);
    }

    // =====================================================
    // FLIP CARD
    // =====================================================

    public void FlipCard()
    {
        if (isFlipping)
        {
            return;
        }

        StartCoroutine(FlipAnimation());
    }

    private IEnumerator FlipAnimation()
    {
        isFlipping = true;

        float duration = 0.30f;

        // -----------------------------------------------------
        // VISUAL FLIP
        // -----------------------------------------------------

        Transform visual =
            cardRenderer.transform;

        Quaternion startRotation =
            visual.localRotation;

        Quaternion endRotation =
            startRotation *
            Quaternion.Euler(0f, 180f, 0f);

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t =
                Mathf.Clamp01(
                    elapsed / duration
                );

            t = Mathf.SmoothStep(
                0f,
                1f,
                t
            );

            visual.localRotation =
                Quaternion.Slerp(
                    startRotation,
                    endRotation,
                    t
                );

            yield return null;
        }

        // -----------------------------------------------------
        // CHANGE ACTUAL MATERIAL
        // -----------------------------------------------------

        if (isFaceUp)
        {
            ShowBack();
        }
        else
        {
            ShowFront();
        }

        // Reset rotation.
        visual.localRotation =
            startRotation;

        isFlipping = false;
    }

    // =====================================================
    // SHOW FRONT MATERIAL
    // =====================================================

    private void ShowFront()
    {
        isFaceUp = true;

        if (cardRenderer == null)
        {
            return;
        }

        // IMPORTANT:
        // Make renderer visible.
        cardRenderer.enabled = true;

        if (frontMaterial != null)
        {
            cardRenderer.material =
                frontMaterial;
        }
    }

    // =====================================================
    // SHOW BACK MATERIAL
    // =====================================================

    private void ShowBack()
    {
        isFaceUp = false;

        if (cardRenderer == null)
        {
            return;
        }

        // IMPORTANT:
        // NEVER disable the renderer.
        cardRenderer.enabled = true;

        if (backMaterial != null)
        {
            cardRenderer.material =
                backMaterial;
        }
    }
    // =====================================================
    // STATE
    // =====================================================

    public bool IsFaceUp()
    {
        return isFaceUp;
    }

    public bool IsFlipping()
    {
        return isFlipping;
    }
}
