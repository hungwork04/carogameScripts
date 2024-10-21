
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class InGameFunction : MonoBehaviour
{

    public Image image;
    public Board board;
    public AudioManager audioManager;
    private void Awake()
    {
        board = FindObjectOfType<Board>();
        image= GetComponent<Image>();
        audioManager= FindObjectOfType<AudioManager>();

    }
    public void PvPbutton()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitToMenu()
    {
        audioManager.musicAudioSource.Pause();
        SceneManager.LoadScene(0);
    }

    public void UndoButton()
    {
        if (board.cellStack.Count > 0&&board.playWithHuman==true)
        {
            board.play = true;
            Cell lastCell=board.cellStack.Pop();
            if (board.currentTurn == "x")
            {
                lastCell.changeImagine("");//sửa ở đây
                board.matrix[lastCell.row, lastCell.col] = "";
                board.currentTurn = "o";
                board.sonuocCo--;
                
            }
            else
            {
                lastCell.changeImagine("");//sửa ở đây
                board.matrix[lastCell.row, lastCell.col] = "";
                board.currentTurn = "x";
                board.sonuocCo--;
            }
        }
        //chơi vs máy bỏ 2 nước cờ
        if (board.cellStack.Count > 0 && board.playWithHuman == false)
        {
            board.play = true;
            Cell lastCell2 = board.cellStack.Pop();
            if (board.currentTurn == "x")
            {
                lastCell2.changeImagine("");//sửa ở đây
                board.matrix[lastCell2.row, lastCell2.col] = "";
                board.currentTurn = "o";
                board.sonuocCo--;

            }
            else
            {
                lastCell2.changeImagine("");//sửa ở đây
                board.matrix[lastCell2.row, lastCell2.col] = "";
                board.currentTurn = "x";
                board.sonuocCo--;
            }
            Cell lastCell3 = board.cellStack.Pop();
            if (board.currentTurn == "x")
            {
                lastCell3.changeImagine("");//sửa ở đây
                board.matrix[lastCell3.row, lastCell3.col] = "";
                board.currentTurn = "o";
                board.sonuocCo--;

            }
            else
            {
                lastCell3.changeImagine("");//sửa ở đây
                board.matrix[lastCell3.row, lastCell3.col] = "";
                board.currentTurn = "x";
                board.sonuocCo--;
            }
        }
        else return;
    }
}
