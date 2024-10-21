
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public GameObject EndGameUI;
    public GameObject DrawGameUI;
    public Sprite xSprite;
    public Sprite oSprite;
    public Sprite NullSprite;
    private Image image;
    private Button button;
    public int row;
    public int col;
    public Transform canvas;

    public AudioManager audioManager;
    public void cellData(int row, int col)
    {
        this.row=row ;
        this.col= col;
    }
    public Cell() { }
    public Board board;
    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        board = FindObjectOfType<Board>();
        canvas= FindObjectOfType<Canvas>().transform;
        audioManager = FindObjectOfType<AudioManager>();
        button.onClick.AddListener(OnClick);
    }
    private void Start()
    {

    }

    public void changeImagine(string s)
    {
        if (s == "x")
        {
            image.sprite = xSprite;

        }
        else if (s == "o")
            image.sprite = oSprite;
        else
            image.sprite = NullSprite;// sửa ở đây
    }
    public void OnClick()
    {
        if (board.matrix[row, col] != "") return;
        if (board.play != true) return;
        if (board.playWithHuman == false && board.currentTurn=="o")
        {
            return;
        }
        board.sonuocCo++;
        changeImagine(board.currentTurn);
        audioManager.PlaySFX(audioManager.danhcoClip);
        board.cellStack.Push(this);
/*        Debug.Log(board.cellStack.Count);*/
        if (board.checkWin(this.row, this.col)&&board.drawGame==false)
        {
            board.theWinner = board.matrix[row, col];
            GameObject endWindow= Instantiate(EndGameUI,canvas);
            endWindow.GetComponent<EndGameFunction>().updateMove(board.sonuocCo);
            endWindow.GetComponent<EndGameFunction>().drawImage.enabled = false;
            endWindow.GetComponent<EndGameFunction>().WI.setImagine();
            endWindow.GetComponent<EndGameFunction>().RanDomMeme.GetRandomMEME();
            audioManager.PlaySFX(audioManager.winClip);
            Debug.Log(board.matrix[row,col]+ " player WIn!");
        }else if (board.drawGame == true)
        {
            GameObject endWindow = Instantiate(EndGameUI, canvas);
            endWindow.GetComponent<EndGameFunction>().drawImage.enabled = true;
/*            Debug.Log("hoa");*/
        }
        if (board.currentTurn == "x")
        {
            board.currentTurn = "o";
        }
        else board.currentTurn = "x";
    }

}
