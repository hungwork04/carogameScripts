
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EndGameFunction : MonoBehaviour
{
    public WinnerImagine WI;
    public Board board;
    public TextMeshProUGUI move;
    public RanDomMeme RanDomMeme;
    public Image drawImage;
    private void Awake()
    {
        WI =FindAnyObjectByType<WinnerImagine>();
        board = FindObjectOfType<Board>();
        RanDomMeme=FindObjectOfType<RanDomMeme>();
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitToMN()
    {
        SceneManager.LoadScene(0);
    }
    public void updateMove(int moves)
    {
       move.text = moves.ToString();
    }
    public void tatManHinh()
    {
        this.gameObject.SetActive(false);
        board.play = false;
    }
}
