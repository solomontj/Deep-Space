using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariableMatchingGameController : MonoBehaviour
{
    public GameObject puzzleField; // Public GameObject references for the puzzle area 
    public GameObject map; // Public GameObject references the map to be shown after the game
    [SerializeField] private Sprite bgImage; // The default background image for puzzle cards
    public AudioSource successSound;  // Assign this in the inspector
    public AudioSource failedSound;
    public AudioSource winSound;

    // Array of all possible puzzle sprites and a dynamic list for the game's active puzzles
    public Sprite[] puzzles;
    public List<Sprite> gamePuzzles = new List<Sprite>();
    public List<Button> btns = new List<Button>(); // Dynamic list of button components for interaction

    // Game state tracking variables.
    private bool firstGuess, secondGuess;
    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;
    private int firstGuessIndex, secondGuessIndex;
    private string firstGuessPuzzle, secondGuessPuzzle;

    // Load all puzzle sprites from a resources folder at the start
    void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("VariableMatchingUI/variableCards");
    }
    
    // Find puzzle buttons, add event listeners, initialize game puzzles, shuffle them, and set game guesses count
    void Start()
    {
        getButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count / 2; 
    }

    // Finds all GameObjects tagged as "PuzzleButton", initializes them with the default background image, and assigns a unique name based on index
    void getButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
        for(int i = 0; i < objects.Length; i++){
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
            btns[i].gameObject.name = i.ToString(); // Set the button's name to its index for identification
        }
    }

    // Populates the gamePuzzles list with sprites, looping through the available puzzles to ensure all buttons have an assigned puzzle
    void AddGamePuzzles()
    {
        int looper = btns.Count;
        int index = 0;
        for(int i = 0; i < looper; i++){
            if(index == looper){
                index = 0;
            }
            gamePuzzles.Add(puzzles[index]);
            index++;
        }
    }

    // Adds a click event listener to each puzzle button to invoke the puzzle selection logic
    void AddListeners()
    {
        foreach(Button btn in btns){
            btn.onClick.AddListener(() => pickAPuzzle());
        }
    }

    // Handles logic for selecting puzzles: sets the guess states, updates button sprites, and starts the match checking coroutine
    public void pickAPuzzle()
    {
        if (!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];

            int underscoreIndex = firstGuessPuzzle.LastIndexOf('_');
            if (underscoreIndex != -1) 
            {
                firstGuessPuzzle = firstGuessPuzzle.Substring(underscoreIndex + 1); // Now firstGuezzPuzzle stores only the index
            }
        }
        else if (!secondGuess)
        {
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            if(firstGuessIndex == secondGuessIndex) return; // Avoid matching the same card

            secondGuess = true;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

            int underscoreIndex2 = secondGuessPuzzle.LastIndexOf('_');
            if (underscoreIndex2 != -1) 
            {
                secondGuessPuzzle = secondGuessPuzzle.Substring(underscoreIndex2 + 1); // Now secondGuessPuzzle stores only the index
            }

            countGuesses++;
            StartCoroutine(CheckIfPuzzlesMatch());
        }
    }

    // Coroutine to check if the selected puzzles match based on predefined rules
    IEnumerator CheckIfPuzzlesMatch()
    {
        yield return new WaitForSeconds(1f);

        int firstGuessNum = int.Parse(firstGuessPuzzle);
        int secondGuessNum = int.Parse(secondGuessPuzzle);

        // Based on the sprites actual names
        // DO NOT CHANGE SPRITE NAMES 
        bool isMatch = (firstGuessNum == 0 && secondGuessNum == 4) ||
                       (firstGuessNum == 4 && secondGuessNum == 0) ||
                       (firstGuessNum == 1 && secondGuessNum == 5) ||
                       (firstGuessNum == 5 && secondGuessNum == 1) ||
                       (firstGuessNum == 2 && secondGuessNum == 6) ||
                       (firstGuessNum == 6 && secondGuessNum == 2) ||
                       (firstGuessNum == 3 && secondGuessNum == 7) ||
                       (firstGuessNum == 7 && secondGuessNum == 3);

        // Handling match or mismatch logic, such as disabling matched buttons or resetting them
        if(isMatch)
        {
            successSound.Play();
            yield return new WaitForSeconds(.5f);
            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false; 
            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);
            CheckIfTheGameIsFinished();
        }
        else
        {
            failedSound.Play();
            yield return new WaitForSeconds(.5f);
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;
        }

        firstGuess = secondGuess = false;
    }

    // Checks if the game is finished
    void CheckIfTheGameIsFinished(){
        countCorrectGuesses++;

        if(countCorrectGuesses == gameGuesses){
            winSound.Play();
            StartCoroutine(FinishGameRoutine());
        }
    }

    // Load Map 
    IEnumerator FinishGameRoutine()
    {
        //Debug.Log("Game Finished");
        //Debug.Log("It took you " + countGuesses + " many guesses to finish the game");

        // Wait for 1 second
        yield return new WaitForSeconds(1f);

        // Perform actions after waiting
        puzzleField.SetActive(false);
        yield return new WaitForSeconds(.5f);
        map.SetActive(true);
    }

    // Shuffles the list of sprites to randomize the game puzzles
    void Shuffle(List<Sprite>list){
        for(int i = 0; i < list.Count; i++){
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
