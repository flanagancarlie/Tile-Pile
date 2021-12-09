using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }
    public TetrominoData[] tetrominos;
    public Vector3Int spawnPosition
    {
        get
        {
            if (OptionsMenu.isEasy)
            {
                return new Vector3Int(-1, 8, 0);
            }
            else
            {
                return new Vector3Int(-1, 6, 0);
            }
        }
    }
    public Vector2Int boardSize {
        get
        {
            if (OptionsMenu.isEasy)
            {
                return new Vector2Int(10, 20);
            }
            else
            {
                return new Vector2Int(10, 16);
            }
        }
    }


    public int linesCleared;
    public int highScore;
    public int level;

    public TextMeshProUGUI cleared;
    public TextMeshProUGUI score;
    public TextMeshProUGUI currentLevel;

    public GameObject levelUp;
    public GameObject gameover;
    public GameObject game;
    public GameObject gameboard;
    public GameObject gridEasy;
    public GameObject gridHard;
    public GameObject borderEasy;
    public GameObject borderHard;

    public GameObject currentScoreObject;
    public GameObject highScoreObject;

    public Timer timer;

    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
        }
    }



    private void Awake()
    {
        FindObjectOfType<AudioManager>().Play("start");
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();
        for (int i = 0; i < this.tetrominos.Length; i++)
        {
            this.tetrominos[i].Initialize();
        }
        //gameover = GameObject.Find("GameOver");
        //game = GameObject.Find("Game");
        //gameboard = GameObject.Find("Board");
        //FindObjectOfType<AudioManager>().sounds[0].volume = (.25f * FindObjectOfType<AudioManager>().sounds[0].volume);
        this.linesCleared = 0;
    }

    private void Start()
    {
            
            gameover.SetActive(false);
            game.SetActive(true);
            gameboard.SetActive(true);
            levelUp.SetActive(false);
            this.linesCleared = 0;
            this.highScore = 0;
            this.level = 1;
            timer.Start();
            SpawnPiece();
            if (OptionsMenu.isEasy)
            {
                gridEasy.SetActive(true);
                borderEasy.SetActive(true);
                gridHard.SetActive(false);
                borderHard.SetActive(false);
                Piece.stepDelay = 1f;
            }
   
            else
            {
                gridEasy.SetActive(false);
                borderEasy.SetActive(false);
                gridHard.SetActive(true);
                borderHard.SetActive(true);
                Piece.stepDelay = .5f;

        }

    }

    public void SpawnPiece()
    {
        int random = Random.Range(0, this.tetrominos.Length);
        TetrominoData data = this.tetrominos[random];

        this.activePiece.Initialize(this, this.spawnPosition, data);

        if (IsValidPosition(this.activePiece, this.spawnPosition))
        {
            Set(this.activePiece);

        }
        else
        {
            GameOver();
        }


    }

    private void GameOver()
    {
        Time.timeScale = 0;
        if (linesCleared >= highScore)
        {
            highScore = linesCleared;
        }
        if (OptionsMenu.isEasy)
        {
            Piece.stepDelay = 1f;
        }
        else
        {
            Piece.stepDelay = .5f;

        }
        Piece.linesProgress = 0;
        timer.GameOver();
        

        FindObjectOfType<AudioManager>().Pause("BackgroundMusic");
        FindObjectOfType<AudioManager>().Play("gameover");

        gameover.SetActive(true);
    }


    private void Update()
    {   
        this.cleared.text = "Lines Cleared: " + linesCleared.ToString();
        this.score.text = "High Score: " + highScore.ToString();
        this.currentLevel.text = "Current Level: " + level.ToString();
        CheckForQuit();
    }


    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);


        }

    }

    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = this.Bounds;

        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;

            if (!bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }


            if (this.tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }

        return true;
    }

    public int ClearLines()
    {
        RectInt bounds = this.Bounds;
        int row = bounds.yMin;
        int lines = 0;

        while (row < bounds.yMax)
        {
            if (IsLineFull(row))
            {
                LineClear(row);
                lines++;
                if (lines == 4) {
                                    FindObjectOfType<AudioManager>().Play("tetris-removal");
                }
                else {
                                    FindObjectOfType<AudioManager>().Play("lineremove");
                }
            }
            else
            {
                row++;
            }
        }
        Piece.linesProgress += lines;
        return lines;
    }

    public bool IsLineFull(int row)
    {
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);

            if (!this.tilemap.HasTile(position))
            {
                return false;
            }
        }

        return true;
    }

    public void LineClear(int row)
    {
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            this.tilemap.SetTile(position, null);
        }

        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase above = this.tilemap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                this.tilemap.SetTile(position, above);
            }
            row++;
        }
        linesCleared++;

    }

    private void CheckForQuit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void PlayAgain()
    {
        FindObjectOfType<AudioManager>().Play("BackgroundMusic");
        this.linesCleared = 0;

        Time.timeScale = 1;
        this.level = 1;
        this.tilemap.ClearAllTiles();
        gameover.SetActive(false);
        game.SetActive(true);
        gameboard.SetActive(true);
        timer.Start();
    }

    public void Pause(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0;
            FindObjectOfType<AudioManager>().Pause("BackgroundMusic");
            FindObjectOfType<AudioManager>().Play("pause");
        }
        else
        {
            Time.timeScale = 1;
            FindObjectOfType<AudioManager>().Play("BackgroundMusic");

        }
    }

    public IEnumerator IncreaseLevel()
    {
        this.level++;
        Debug.Log(this.level);
        levelUp.SetActive(true);

        yield return new WaitForSeconds(1);
        levelUp.SetActive(false);

    }

}
