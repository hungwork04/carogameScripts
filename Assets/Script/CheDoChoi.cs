
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheDoChoi : MonoBehaviour
{
    private Button button;
    private Image image;
    private Board board;
    public Sprite pvP;
    public Sprite pvE;
    public static bool coDinh=true;

    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        board = FindAnyObjectByType<Board>();
        button.onClick.AddListener(OnClickCDC);
        board.playWithHuman = coDinh;
        if (coDinh == false)
        {
            changeImagineButton(coDinh);
        }
    }
    void OnClickCDC()
    {
        SceneManager.LoadScene(1);
        if (coDinh == true) 
        {
            board.playWithHuman = false;
            coDinh= board.playWithHuman;
            changeImagineButton(coDinh);
        }
        else
        {
            board.playWithHuman = true;
            coDinh = board.playWithHuman;
            changeImagineButton(coDinh);
        }
    }
    public void changeImagineButton(bool PvP)
    {
        if (PvP)
        {
            image.sprite = pvP;
        }
        else
            image.sprite = pvE;

    }
}
