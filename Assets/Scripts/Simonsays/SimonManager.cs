using System.Collections.Generic;
using UnityEngine;

public class SimonManager : MonoBehaviour
{
    //Stores the generated Simon Says seuence
    public List<int> sequence = new List<int>();

    //Current round number
    public int currentRound = 1;

    //Is the game currently showing the sequence?
    public bool isShowingSequence = false;

    //Generate a random number and add it to the sequence
    public void AddRandomSpell()
    {
        int randomSpell = Random.Range(0, 8); //8 card
        sequence.Add(randomSpell);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        sequence.Clear();
        currentRound = 1;
        AddRandomSpell();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
