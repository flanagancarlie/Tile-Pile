using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using TMPro;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }
    public Piece heldPiece { get; private set; }
    public TetrominoData[] tetrominos;
    public Piece[] queue;
    public Vector3Int spawnPosition = new Vector3Int(-1, 8, 0);
    public Vector2Int boardSize = new Vector2Int(10, 20);

    public int linesCleared;
    public int highScore;
    public TextMeshProUGUI cleared;
    public TextMeshProUGUI score;
    public GameObject gameover;
    public GameObject game;
    public GameObject gameboard;

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

    }

    private void Start()
    {
        gameover.SetActive(false);
        game.SetActive(true);
        gameboard.SetActive(true);
        this.linesCleared = 0;
        this.highScore = 0;
        //setQueue();
        SpawnPiece();

    }

    private void setQueue()
    {
        // gets 3 random tetrominoes and sets it to the queue before showing each in the queue on board
       for(int i = 0; i < this.queue.Length; i++) 
        {
            int random = Random.Range(0, this.tetrominos.Length);
            TetrominoData data = this.tetrominos[random];

            
            //print(queue[i].tetromino);
        }

    }

    public void SpawnPiece()
    {
        // maybe generalize this and have it take in a piece so we don't have to create a spawn queue function
        // oh wait maybe it just always takes the first piece from the queue
        // this could work it's just that we would have to uh yeah ill figure it out <3

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


    public void spawnNextInQueue()
    {
        //this.activePiece.Initialize();
        if(IsValidPosition(this.activePiece, this.spawnPosition))
        {
            Set(this.activePiece);

        }
        else
        {
            GameOver();
        }

    }

    private void addtoQueue()
    {
        // add random piece to the end of the queue
        int random = Random.Range(0, this.tetrominos.Length);
        TetrominoData data = this.tetrominos[random];
        //queue[this.queue.Length - 1] = piece; or something like that lol
    }

    private void GameOver()
    {
        //SceneManager.LoadScene("GameOver");
        if (linesCleared >= highScore)
        {
            highScore = linesCleared;
        }
        FindObjectOfType<AudioManager>().Play("gameover");
<<<<<<< Updated upstream


        //SceneManager.LoadScene("GameOver");
        gameover.SetActive(true);
        game.SetActive(false);
        gameboard.SetActive(false);
        linesCleared = 0;


=======
        linesCleared = 0;
        this.tilemap.ClearAllTiles();
        SceneManager.LoadScene("GameOver");
>>>>>>> Stashed changes
    }


    private void Update()
    {
        this.cleared.text = "Lines Cleared: " + linesCleared.ToString();
        this.score.text = "High Score: " + highScore.ToString();
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

    public void ClearLines()
    {
        RectInt bounds = this.Bounds;
        int row = bounds.yMin;

        while (row < bounds.yMax)
        {
            if (IsLineFull(row))
            {
                FindObjectOfType<AudioManager>().Play("lineremove");
                LineClear(row);
            }
            else
            {
                row++;
            }
        }
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
        SceneManager.LoadScene("Menu");
    }

<<<<<<< Updated upstream
    public void PlayAgain()
    {
        this.tilemap.ClearAllTiles();
        gameover.SetActive(false);
        game.SetActive(true);
        gameboard.SetActive(true);
    }
=======
    public void Hold()
    {
        // if heldpiece is null/empty
        // set held piece to active piece
        // active piece is set to next in queue

        // if held piece is not null/empty
        // swap held piece and active piece
    }

    public int getLinesCleared() { return linesCleared;}

    public int getHighScore() { return highScore; }

>>>>>>> Stashed changes
}
