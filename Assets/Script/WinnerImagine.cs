
using UnityEngine;
using UnityEngine.UI;

public class WinnerImagine : MonoBehaviour
{
    public Sprite O;
    public Sprite X;
    public Sprite draw;
    public Image image;
    public Board board;
    private void Awake()
    {
        image = GetComponent<Image>();   
        board= FindObjectOfType<Board>();
    }
    public void setImagine()
    {
        if (board.theWinner == "o")
        {
            image.sprite = O;
        }
        else if (board.theWinner == "x")
        {
            image.sprite = X;
        }
        else image.sprite = draw;
    }
}
