using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariableMatchingGameController : MonoBehaviour
{
    public GameObject puzzleField;
    public GameObject map;
    [SerializeField] private Sprite bgImage;
    public AudioSource buttonClick;
    public AudioSource successSound;
    public AudioSource failedSound;
    public AudioSource winSound;

    public Sprite[] puzzles;
    public List<Sprite> gamePuzzles = new List<Sprite>();
    public List<Button> btns = new List<Button>();

    private bool firstGuess, secondGuess;
    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;
    private int firstGuessIndex, secondGuessIndex;
    private string firstGuessPuzzle, secondGuessPuzzle;

    void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("VariableMatchingUI/variableCards");
    }

    void Start()
    {
        getButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count / 2;
        buttonClick.Play();
    }

    void getButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
            btns[i].gameObject.name = i.ToString();
        }
    }

    void AddGamePuzzles()
    {
        int looper = btns.Count;
        int index = 0;
        for (int i = 0; i < looper; i++)
        {
            if (index == looper)
            {
                index = 0;
            }
            gamePuzzles.Add(puzzles[index]);
            index++;
        }
    }

    void AddListeners()
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => pickAPuzzle());
        }
    }

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
                firstGuessPuzzle = firstGuessPuzzle.Substring(underscoreIndex + 1);
            }
        }
        else if (!secondGuess)
        {
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            if (firstGuessIndex == secondGuessIndex) return;

            secondGuess = true;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

            int underscoreIndex2 = secondGuessPuzzle.LastIndexOf('_');
            if (underscoreIndex2 != -1)
            {
                secondGuessPuzzle = secondGuessPuzzle.Substring(underscoreIndex2 + 1);
            }

            countGuesses++;
            StartCoroutine(CheckIfPuzzlesMatch());
        }
    }

    IEnumerator CheckIfPuzzlesMatch()
    {
        yield return new WaitForSeconds(1f);

        int firstGuessNum = int.Parse(firstGuessPuzzle);
        int secondGuessNum = int.Parse(secondGuessPuzzle);

        bool isMatch = (firstGuessNum == 0 && secondGuessNum == 4) ||
                       (firstGuessNum == 4 && secondGuessNum == 0) ||
                       (firstGuessNum == 1 && secondGuessNum == 5) ||
                       (firstGuessNum == 5 && secondGuessNum == 1) ||
                       (firstGuessNum == 2 && secondGuessNum == 6) ||
                       (firstGuessNum == 6 && secondGuessNum == 2) ||
                       (firstGuessNum == 3 && secondGuessNum == 7) ||
                       (firstGuessNum == 7 && secondGuessNum == 3);

        if (isMatch)
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

    void CheckIfTheGameIsFinished()
    {
        countCorrectGuesses++;

        if (countCorrectGuesses == gameGuesses)
        {
            winSound.Play();
            StartCoroutine(FinishGameRoutine());
        }
    }

    IEnumerator FinishGameRoutine()
    {

        yield return new WaitForSeconds(1f);

        puzzleField.SetActive(false);
        yield return new WaitForSeconds(.5f);
        map.SetActive(true);
    }

    void Shuffle(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}