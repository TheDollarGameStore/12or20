using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject[,] slots = new GameObject[4, 4];

    // Start is called before the first frame update

    public static GameManager instance = null;

    public DiceHolder diceHolder;

    [HideInInspector]
    public bool paused;

    [HideInInspector]
    public int score;

    private int drawnScoreShouldBe;

    private int drawnScore;

    private int highscore;

    [SerializeField] private Text scoreText;

    [SerializeField] private AudioClip matchSound;

    [SerializeField] private AudioClip gameOverSound;

    [SerializeField] private Text highscoreText;

    [SerializeField] private FlyIn highscoreFlyIn;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        highscoreText.text = highscore.ToString();
        PopulateGrid();
        diceHolder.ShuffleDices();
        ScoreTally();
    }

    void PopulateGrid()
    {
        GameObject[] foundSlots = GameObject.FindGameObjectsWithTag("Slot");

        for (int i = 0; i < foundSlots.Length; i++)
        {
            Cell cell = foundSlots[i].GetComponent<Cell>();

            slots[cell.y, cell.x] = foundSlots[i];
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !paused)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Slot"))
                {
                    hit.collider.GetComponent<Cell>().PlaceDice();
                }
            }
        }
    }

    private void ScoreTally()
    {
        Invoke("ScoreTally", 0.025f);
        if (drawnScore != drawnScoreShouldBe)
        {
            drawnScore += 1;
            scoreText.text = drawnScore.ToString();
        }
    }

    public void CheckMatches()
    {
        List<Cell> matchedCells = new List<Cell>();
        int matchMultiplier = 0; //Multiplies your score for every match made.

        //Check Horizontals
        for (int y = 0; y < 4; y++)
        {
            int total = 0;
            
            for (int x = 0; x < 4; x++)
            {
                total += GetCellValue(y, x);
            }

            if (total == 12 || total == 20)
            {
                matchMultiplier++;
                Debug.Log("Match in row: " + y);
                for (int x = 0; x < 4; x++)
                {
                    matchedCells.Add(slots[y, x].GetComponent<Cell>());
                }
            }
        }

        //Check Verticals
        for (int x = 0; x < 4; x++)
        {
            int total = 0;

            for (int y = 0; y < 4; y++)
            {
                total += GetCellValue(y, x);
            }

            if (total == 12 || total == 20)
            {
                matchMultiplier++;
                Debug.Log("Match in column: " + x);
                for (int y = 0; y < 4; y++)
                {
                    matchedCells.Add(slots[y, x].GetComponent<Cell>());
                }
            }
        }

        //Check Cells
        int totalTL = 0; //Top Left

        totalTL = GetCellValue(0, 0) + GetCellValue(0, 1) + GetCellValue(1, 0) + GetCellValue(1, 1);

        if (totalTL == 12 || totalTL == 20)
        {
            matchMultiplier++;
            matchedCells.Add(slots[0, 0].GetComponent<Cell>());
            matchedCells.Add(slots[0, 1].GetComponent<Cell>());
            matchedCells.Add(slots[1, 0].GetComponent<Cell>());
            matchedCells.Add(slots[1, 1].GetComponent<Cell>());
            Debug.Log("Match in cell: Top Left");
        }


        int totalTR = 0; //Top Right

        totalTR = GetCellValue(0, 2) + GetCellValue(0, 3) + GetCellValue(1, 2) + GetCellValue(1, 3);

        if (totalTR == 12 || totalTR == 20)
        {
            matchMultiplier++;
            matchedCells.Add(slots[0, 2].GetComponent<Cell>());
            matchedCells.Add(slots[0, 3].GetComponent<Cell>());
            matchedCells.Add(slots[1, 2].GetComponent<Cell>());
            matchedCells.Add(slots[1, 3].GetComponent<Cell>());
            Debug.Log("Match in cell: Top Right");
        }


        int totalBR = 0; //Bottom Right

        totalBR = GetCellValue(2, 2) + GetCellValue(2, 3) + GetCellValue(3, 2) + GetCellValue(3, 3);

        if (totalBR == 12 || totalBR == 20)
        {
            matchMultiplier++;
            matchedCells.Add(slots[2, 2].GetComponent<Cell>());
            matchedCells.Add(slots[2, 3].GetComponent<Cell>());
            matchedCells.Add(slots[3, 2].GetComponent<Cell>());
            matchedCells.Add(slots[3, 3].GetComponent<Cell>());
            Debug.Log("Match in cell: Bottom Right");
        }


        int totalBL = 0; //Bottom Left

        totalBL = GetCellValue(2, 0) + GetCellValue(2, 1) + GetCellValue(3, 0) + GetCellValue(3, 1);

        if (totalBL == 12 || totalBL == 20)
        {
            matchMultiplier++;
            matchedCells.Add(slots[2, 0].GetComponent<Cell>());
            matchedCells.Add(slots[2, 1].GetComponent<Cell>());
            matchedCells.Add(slots[3, 0].GetComponent<Cell>());
            matchedCells.Add(slots[3, 1].GetComponent<Cell>());
            Debug.Log("Match in cell: Bottom Left");
        }

        if (matchedCells.Count != 0)
        {
            SoundManager.instance.PlayRandomized(matchSound);
            paused = true;
            int points = 0;

            for (int i = 0; i < matchedCells.Count; i++)
            {
                if (matchedCells[i].dice != null) //Whenever there's a duplicate dice in a multiple match, it will already have been removed
                {
                    points += matchedCells[i].dice.GetComponent<Dice>().value;
                    matchedCells[i].dice.GetComponent<Dice>().Matched();
                    matchedCells[i].dice = null;
                }
            }

            points *= matchMultiplier;

            score += points;

            Invoke("Unpause", 1f);
            Invoke("SyncPoints", 0.75f);
        }
        else
        {
            if (CheckGameOver())
            {
                //Do gameover stuff
                SoundManager.instance.PlayNormal(gameOverSound);
                Invoke("DoHighscoreStuff", 1.5f);
            }
        }
    }

    private void DoHighscoreStuff()
    {
        if (score > highscore)
        {
            highscoreFlyIn.StartFly();
            PlayerPrefs.SetInt("Highscore", score);
        }

        Invoke("RestartRoom", 2f);
    }

    private void RestartRoom()
    {
        Transitioner.Instance.TransitionToScene(0);
    }

    private void SyncPoints()
    {
        drawnScoreShouldBe = score;
    }

    private bool CheckGameOver()
    {
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                if (slots[y, x].GetComponent<Cell>().dice == null)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void Unpause()
    {
        paused = false;
    }

    private int GetCellValue(int y, int x)
    {
        if (slots[y, x].GetComponent<Cell>().dice != null)
        {
            return slots[y, x].GetComponent<Cell>().dice.GetComponent<Dice>().value;
        }
        else
        {
            return -100;
        }
    }

}
