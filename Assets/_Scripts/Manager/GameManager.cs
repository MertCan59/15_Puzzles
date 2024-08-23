using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //added variables we needed 
    public static GameManager Instance;
    public List<Tile>  tiles;
    public int emptySpaceIndex;
    [SerializeField] private Transform emptyTransform;
    [SerializeField] private GameObject canvasObject;
    [SerializeField] private GameObject timerGameObject;
    public bool isGameBeaten;
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);  
        }
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        CheckWin();
    }

    // when NewGame is called gameObject become passive and game will not be beaten
    public void NewGame()
    {
        
        ShufflePuzzles();
        canvasObject.SetActive(false);
        isGameBeaten = false;
    }
    
    //We are shuffle the puzzle here for every new game 
    private void ShufflePuzzles()
    {
        if (emptySpaceIndex != 15)
        {
            var lastTilePos = tiles[15].targetPos;
            tiles[15].targetPos = emptyTransform.position;
            emptyTransform.position = lastTilePos;
            tiles[emptySpaceIndex] = tiles[15];
            tiles[15] = null;
            emptySpaceIndex = 15;
        }
        int inversion;
        do
        {
            for (int i = 0; i < tiles.Count-2; i++) 
            {
                int randomValue = Random.Range(0, tiles.Count-2);
            
                // Değişim sırasında null kontrolü yapıyoruz
                if (tiles[i] != null && tiles[randomValue] != null)
                {
                    Vector2 lastPos = tiles[i].targetPos;
                    tiles[i].targetPos = tiles[randomValue].targetPos;
                    tiles[randomValue].targetPos = lastPos;

                    var temp = tiles[i];
                    tiles[i] = tiles[randomValue];
                    tiles[randomValue] = temp;
                }
            }
            inversion = AdjustInversion();
            Debug.Log("Shuffled");
        } while (inversion % 2 != 0);
    }
    //giving index for list
    public int FindIndex(Tile tile)
    {
        for (int i = 0; i < tiles.Count; i++)   
        {
            if (tiles[i] != null)
            {
                if (tiles[i] == tile)
                {
                    return i;
                }
            }
        }
        return -1;
    }
    //ensure the game can be finished
    private int AdjustInversion()
    {
        int invSum = 0;
        for (int i = 0; i <= tiles.Count-1; i++)
        {
            int invertion=0;
            for (int j = i; j <= tiles.Count-1; j++)
            {
                if (tiles[j] != null)
                {
                    if (tiles[i].number > tiles[j].number)
                    {
                        invertion++;
                    }
                }
            }
            invSum += invertion;
        }
        return invSum;
    }
    
    //control the tiles if all tiles are in the right place and then it will work
    public void CheckWin()
    {
        if (!isGameBeaten)
        {
            int correcTile = 0;
            foreach (var a in tiles)
            {
                if (a != null)
                {
                    if (a.isRightPlace)
                    {
                        correcTile++;
                    }
                }
            }
            if (correcTile == tiles.Count - 1)
            {
                isGameBeaten = true;
                canvasObject.SetActive(true);
                timerGameObject.GetComponent<TimeManager>().StopTimer();
            }
        }
    }
    
    public void PlayAgain()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        SceneManager.LoadScene("GameScene");
    }
}
