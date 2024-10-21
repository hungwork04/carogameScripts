using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public Transform board;
    public GameObject cellPrefab;
    public GridLayoutGroup gridlayout;
    public List<Cell> listCell;
    public Stack<Cell> cellStack;
    public int boardSize;
    public string theWinner = null;
    public string currentTurn = "x";
    public string[,] matrix;
    public bool playWithHuman=true;
    public bool play = true;
    public int count = 0;
    public bool drawGame=false;
    public AudioManager audioManager;
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    protected void Start()
    {
        ranMEME = FindAnyObjectByType<RanDomMeme>();
        count = 0;
        matrix=new string[boardSize+2,boardSize+2];//luu y
        gridlayout.constraintCount = boardSize;
        createBoard();
        sonuocCo = 0;
        play = true;
        cellStack = new Stack<Cell>();
    }
    private void createBoard()
    {
        for(int i = 1; i <= boardSize; i++)
        {
            for(int j = 1; j <=boardSize; j++)
            {
                GameObject cellTransform= Instantiate(cellPrefab, board);
                Cell cell = cellTransform.GetComponent<Cell>();// de tat ca prefab deu co thong tin ve: col va row
                cell.row = i;
                cell.col = j;
                matrix[i, j]="";
                listCell.Add(cell);
            }
        }
    }
    public float countdownTime = 0.3f;
    public bool checkWin(int row, int col)//duyệt chiến thắng
    {
        matrix[row, col] = currentTurn;
        if (sonuocCo == boardSize * boardSize)
        {
            drawGame = true;
            return true;
        }
        bool result = false;
        // Check hàng dọc
        int counta = 0;
        for (int i = row - 1; i >= 0; i--) // Lên trên
        {
            if (matrix[i, col] == matrix[row, col])
            {
                counta++;
            }
            else break;
        }
        for (int i = row + 1; i <= boardSize; i++) // Xuống dưới
        {
            if (matrix[i, col] == matrix[row, col])
            {
                counta++;
            }
            else break;
        }
/*        Debug.Log("Vertical count: " + counta);*/
        if (counta + 1 >= 5) result = true;

        // Check hàng ngang
        int countb = 0;
        for (int i = col - 1; i >= 0; i--) // Trái
        {
            if (matrix[row, i] == matrix[row, col])
            {
                countb++;
            }
            else break;
        }
        for (int i = col + 1; i <= boardSize; i++) // Phải
        {
            if (matrix[row, i] == matrix[row, col])
            {
                countb++;
            }
            else break;
        }
/*        Debug.Log("Horizontal count: " + countb);*/
        if (countb + 1 >= 5) result = true;

        // Check hàng chéo chính (\)
        int countc = 0;
        for (int i = 1; i <= Math.Min(row, col); i++) // Lên trên trái
        {
            if (matrix[row - i, col - i] == matrix[row, col])
            {
                countc++;
            }
            else break;
        }
        for (int i = 1; row + i <= boardSize && col + i <= boardSize; i++) // Xuống dưới phải
        {
            if (matrix[row + i, col + i] == matrix[row, col])
            {
                countc++;
            }
            else break;
        }
/*        Debug.Log("Main diagonal count: " + countc);*/
        if (countc + 1 >= 5) result = true;

        // Check hàng chéo phụ (/)
        int countd = 0;
        for (int i = 1; i <= Math.Min(row, boardSize - col ); i++) // Lên trên phải
        {
            if (matrix[row - i, col + i] == matrix[row, col])
            {
                countd++;
            }
            else break;
        }
        for (int i = 1; row + i <= boardSize && col - i >= 0; i++) // Xuống dưới trái
        {
            if (matrix[row + i, col - i] == matrix[row, col])
            {
                countd++;
            }
            else break;
        }
/*        Debug.Log("Anti-diagonal count: " + countd);*/
        if (countd + 1 >= 5) result = true;

        return result;
/*        #region check cu
        //check hang doc
        int counta = 0;
        for (int i = row - 1; i >= 1; i--)//len tren
        {
            if (matrix[i, col] == matrix[row, col])
            {
                counta++;
            }
            else break;

        }
        for (int i = row + 1; i <= boardSize; i++)//xuong duoi
        {
            if (matrix[i, col] == matrix[row, col])
            {
                counta++;
            }
            else break;
        }
        Debug.Log(counta);
        if (counta + 1 >= 5)
        {
            result = true;
        }
        //check hang ngang
        int countb = 0;
        for (int i = col - 1; i >= 1; i--)//trai
        {
            if (matrix[row, i] == matrix[row, col])
            {
                countb++;
            }
            else break;
        }
        for (int i = col + 1; i <= boardSize; i++)//phai
        {
            if (matrix[row, i] == matrix[row, col])
            {
                countb++;
            }
            else break;
        }
        Debug.Log(countb);
        if (countb + 1 >= 5)
        {
            result = true;
        }
        //check hang cheo 1
        int countc = 0;
        for (int i = col - 1; i >= 1; i--)//len tren trai
        {
            if (matrix[row - (col - i), i] == matrix[row, col])
            {
                countc++;
            }
            else break;
        }
        for (int i = col + 1; i <= boardSize; i++)//xuong duoi phai
        {
            if (matrix[row + (col - i), i] == matrix[row, col])
            {
                countc++;
            }
            else break;
        }
        Debug.Log(countc);
        if (countc + 1 >= 5)
        {
            result = true;
        }
        //check hang cheo 2
        int countd = 0;
        for (int i = col + 1; i <= boardSize; i++)//len tren phai
        {
            if (matrix[row - (col - i), i] == matrix[row, col])
            {
                countd++;
            }
            else break;
        }
        for (int i = col - 1; i >= 1; i--)//xuong duoi trai
        {
            if (matrix[row + (col - i), i] == matrix[row, col])
            {
                countd++;
            }
            else break;
        }
        Debug.Log(countd);
        if (countd + 1 >= 5)
        {
            Debug.Log(matrix[row,col]);
            result = true;
        }
        return result;
    public bool duyetNgang(int row, int col,string currentTurn)
    {
        //duyet doc
        int dem;
        if (col > boardSize - 5) return false;
        for (dem = 1; dem < 5; dem++)
        {
            if (matrix[row, col + dem] != currentTurn) return false;
        }
        if (col == 0 || col + dem == boardSize) return true;
        if (matrix[row, col - 1] == "" || matrix[row, col + dem] == "") return true;

        Debug.Log("runhere");
        return false;
    }
    public bool duyetCheoXuoi(int row, int col, string currentTurn)
    {
        int dem;
        //duyet cheo xuoi
        if (row > boardSize - 5 || col > boardSize - 5) return false;
        for (dem = 1; dem < 5; dem++)
        {
            if (matrix[row + dem, col + dem] != currentTurn)
                return false;
        }
        if (row == 0 || row + dem == boardSize || col == 0 || col + dem == boardSize)
            return true;
        if (matrix[row - 1, col - 1] == "" || matrix[row + dem, col + dem] == "")
            return true;
        Debug.Log("runhere");
        return false;
    }
    public bool duyetCheoNguoc(int row, int col,string currentTurn)
    {
        int dem;
        //duyet cheo nguoc
        if (row < 4 || col > boardSize - 5) return false;
        for (dem = 1; dem < 5; dem++)
        {
            if (matrix[row - dem, col + dem] != currentTurn)
                return false;
        }
        if (row == 4 || row == boardSize - 1 || col == 0 || col + dem == boardSize)
            return true;
        if (matrix[row + 1, col - 1] == "" || matrix[row - dem, col + dem] == "")
            return true;
        Debug.Log("runhere");
        return false;
    }
        #endregion*/
    }
    #region check cũ
    /*    public bool checkWin(int row, int col)
        {
            *//*        int sonuocdadi = 0;
                    foreach(Cell cell in listCell)
                    {
                        if (matrix[cell.row, cell.col] != "")
                        {
                            sonuocdadi++;
                        }
                    }*//*
            if (sonuocCo == boardSize * boardSize)
            {
                Debug.Log("hoa");
                return true;
            }
            foreach (Cell cell in listCell)
            {
                if (matrix[row, col] == matrix[cell.row, cell.col] && row==cell.row&&col==cell.col)
                {
                    if(duyetDoc(row, col, this.currentTurn)) return true;
                    if (duyetNgang(row, col, this.currentTurn)) return true;
                    if (duyetCheoXuoi(row, col, this.currentTurn)) return true;
                    if( duyetCheoNguoc(row, col, this.currentTurn)) return true;

                }
            }
            Debug.Log("sai");
            return false;
        }*/
    #endregion
    public bool duyetDoc(int row, int col,string currentTurn)
    {
        //duyet doc
        if (row > boardSize - 5) return false;
        int dem;
        for (dem = 1; dem < 5; dem++)
        {
            if (matrix[row + dem, col] != currentTurn) {
            
            return false;
            } 
        }
            Debug.Log("runhere");
        if (row == 0 || row + dem == boardSize) return true;
        if (matrix[row - 1,col] == "" || matrix[row + dem, col] == "") return true;
        return false;
    }


    public void Update()
    {
        if (playWithHuman != false) return;
        if (currentTurn != "o") 
        {
            countdownTime = 0.3f;
            return;
        }
        countdownTime -= Time.deltaTime;
        // Khi hết thời gian
        if (countdownTime <= 0f)
        {
            countdownTime = 0f; // Đảm bảo thời gian không bị âm
            if (timKiemNuocDi() == null) return;
            danhCo(timKiemNuocDi().row, timKiemNuocDi().col);
        }
        

    }

    #region AI
    private long[] MangDiemTanCong = new long[8] { 0, 9, 54, 162, 1458, 13112, 118008, 1062882 };
    private long[] MangDiemPhongNgu = new long[8] { 0, 3, 27, 99, 729, 6561, 59049 , 106288 };


    private Cell timKiemNuocDi()
    {
        /*        Cell cell = new Cell();*/
        Cell bestCell = null;
        long diemMax = 0;
        for (int i = 1; i <= boardSize; i++)
        {
            for (int j = 1; j <= boardSize; j++)
            {
                if (matrix[i, j] == "")
                {
                    long DiemTanCong = DiemTC_DuyetDoc(i, j) + DiemTC_DuyetNgang(i, j) + DiemTC_DuyetCheoNguoc(i, j) + DiemTC_DuyetCheoXuoi(i, j);
                    long DiemPhongNgu = DiemPN_DuyetDoc(i, j) + DiemPN_DuyetNgang(i, j) + DiemPN_DuyetCheoNguoc(i, j) + DiemPN_DuyetCheoXuoi(i, j);
                    long diemTam = DiemTanCong > DiemPhongNgu ? DiemTanCong : DiemPhongNgu;
                    if (diemMax < diemTam)
                    {
                        diemMax = diemTam;
                        bestCell = listCell.FirstOrDefault(c => c.row == i && c.col == j); // Tìm đối tượng Cell hiện có
                        if (bestCell != null)
                        {
                            bestCell.row = i;
                            bestCell.col = j;
                        }
                    }
                }
            }
        }
        return bestCell;
    }
    #region tancong
    private long DiemTC_DuyetDoc(int currDong,int currCot)
    {
        long diemTong = 0;
        int soQuanAI = 0;
        int soQuanPlayer = 0;
        for (int i = 1; i < 6&& currDong+i<boardSize; i++)
        {
            if (matrix[currDong + i, currCot] == "o")
            {
                soQuanAI++;
            }
            else if (matrix[currDong + i, currCot] == "x")
            {
                soQuanPlayer++;
                break;
            }
            else
            {
                break;
            }
        }
        for (int i = 1; i < 6 && currDong - i > 0; i++)
        {
            if (matrix[currDong - i, currCot] == "o")
            {
                soQuanAI++;
            }
            else if (matrix[currDong - i, currCot] == "x")
            {
                soQuanPlayer++;
                break;
            }
            else
            {
                break;
            }
        }
        if (soQuanPlayer == 2) return 0;
        diemTong -= MangDiemPhongNgu[soQuanPlayer+1];
        diemTong += MangDiemTanCong[soQuanAI];
        return diemTong;
        
    }
    private long DiemTC_DuyetNgang(int currDong, int currCot)
    {
        long diemTong = 0;
        int soQuanAI = 0;
        int soQuanPlayer = 0;
        for (int i = 1; i < 6 && currCot + i <=boardSize; i++)
        {
            if (matrix[currDong , currCot+i] == "o")
            {
                soQuanAI++;
            }
            else if (matrix[currDong, currCot + i] == "x")
            {
                soQuanPlayer++;
                break;
            }
            else
            {
                break;
            }
        }
        for (int i = 1; i < 6 && currCot - i > 0; i++)
        {
            if (matrix[currDong , currCot - i] == "o")
            {
                soQuanAI++;
            }
            else if (matrix[currDong , currCot - i] == "x")
            {
                soQuanPlayer++;
                break;
            }
            else
            {
                break;
            }
        }
        if (soQuanPlayer == 2) return 0;
        diemTong -= MangDiemPhongNgu[soQuanPlayer + 1];
        diemTong += MangDiemTanCong[soQuanAI];
        return diemTong;
    }
    private long DiemTC_DuyetCheoNguoc(int currDong, int currCot)
    {
        long diemTong = 0;
        int soQuanAI = 0;
        int soQuanPlayer = 0;
        for (int i = 1; i < 6 && currCot + i <= boardSize&& currDong-i>0; i++)
        {
            if (matrix[currDong-i, currCot + i] == "o")
            {
                soQuanAI++;
            }
            else if (matrix[currDong-i, currCot + i] == "x")
            {
                soQuanPlayer++;
                break;
            }
            else
            {
                break;
            }
        }
        for (int i = 1; i < 6 && currCot - i > 0&& currDong+i<boardSize; i++)
        {
            if (matrix[currDong+i, currCot - i] == "o")
            {
                soQuanAI++;
            }
            else if (matrix[currDong+i, currCot - i] == "x")
            {
                soQuanPlayer++;
                break;
            }
            else
            {
                break;
            }
        }
        if (soQuanPlayer == 2) return 0;
        diemTong -= MangDiemPhongNgu[soQuanPlayer + 1];
        diemTong += MangDiemTanCong[soQuanAI];
        return diemTong;

    }
    private long DiemTC_DuyetCheoXuoi(int currDong, int currCot)
    {
        long diemTong = 0;
        int soQuanAI = 0;
        int soQuanPlayer = 0;
        for (int i = 1; i < 6 && currCot + i <= boardSize && currDong + i <=boardSize; i++)
        {
            if (matrix[currDong + i, currCot + i] == "o")
            {
                soQuanAI++;
            }
            else if (matrix[currDong + i, currCot + i] == "x")
            {
                soQuanPlayer++;
                break;
            }
            else
            {
                break;
            }
        }
        for (int i = 1; i < 6 && currCot - i > 0 && currDong - i>0; i++)
        {
            if (matrix[currDong - i, currCot - i] == "o")
            {
                soQuanAI++;
            }
            else if (matrix[currDong - i, currCot - i] == "x")
            {
                soQuanPlayer++;
                break;
            }
            else
            {
                break;
            }
        }
        if (soQuanPlayer == 2) return 0;
        diemTong -= MangDiemPhongNgu[soQuanPlayer + 1];
        diemTong += MangDiemTanCong[soQuanAI];
        return diemTong;

    }
    #endregion

    #region phong ngu
    private long DiemPN_DuyetDoc(int currDong, int currCot)
    {
        long diemTong = 0;
        int soQuanAI = 0;
        int soQuanPlayer = 0;
        for (int i = 1; i < 6 && currDong + i <= boardSize; i++)
        {
            if (matrix[currDong + i, currCot] == "o")
            {
                soQuanAI++;
                break;
            }
            else if (matrix[currDong + i, currCot] == "x")
            {
                soQuanPlayer++;
                
            }
            else
            {
                break;
            }
        }
        for (int i = 1; i < 6 && currDong - i > 0; i++)
        {
            if (matrix[currDong - i, currCot] == "o")
            {
                soQuanAI++;
                break;
            }
            else if (matrix[currDong - i, currCot] == "x")
            {
                soQuanPlayer++;
            }
            else
            {
                break;
            }
        }
        if (soQuanAI == 2) return 0;
        diemTong += MangDiemPhongNgu[soQuanPlayer];
        return diemTong;

    }
    private long DiemPN_DuyetNgang(int currDong, int currCot)
    {
        long diemTong = 0;
        int soQuanAI = 0;
        int soQuanPlayer = 0;
        for (int i = 1; i < 6 && currCot + i <= boardSize; i++)
        {
            if (matrix[currDong, currCot + i] == "o")
            {
                soQuanAI++;
                break;
            }
            else if (matrix[currDong, currCot + i] == "x")
            {
                soQuanPlayer++;
            }
            else
            {
                break;
            }
        }
        for (int i = 1; i < 6 && currCot - i > 0; i++)
        {
            if (matrix[currDong, currCot - i] == "o")
            {
                soQuanAI++;
                break;
            }
            else if (matrix[currDong, currCot - i] == "x")
            {
                soQuanPlayer++;
            }
            else
            {
                break;
            }
        }
        if (soQuanAI == 2) return 0;
        diemTong += MangDiemPhongNgu[soQuanPlayer];
        return diemTong;
    }
    private long DiemPN_DuyetCheoNguoc(int currDong, int currCot)
    {
        long diemTong = 0;
        int soQuanAI = 0;
        int soQuanPlayer = 0;
        for (int i = 1; i < 6 && currCot + i <= boardSize && currDong - i > 0; i++)
        {
            if (matrix[currDong - i, currCot + i] == "o")
            {
                soQuanAI++;
                break;
            }
            else if (matrix[currDong - i, currCot + i] == "x")
            {
                soQuanPlayer++;
            }
            else
            {
                break;
            }
        }
        for (int i = 1; i < 6 && currCot - i > 0 && currDong + i <=boardSize; i++)
        {
            if (matrix[currDong + i, currCot - i] == "o")
            {
                soQuanAI++;
                break;
            }
            else if (matrix[currDong + i, currCot - i] == "x")
            {
                soQuanPlayer++;
            }
            else
            {
                break;
            }
        }
        if (soQuanAI == 2) return 0;
        diemTong += MangDiemPhongNgu[soQuanPlayer];
        return diemTong;

    }
    private long DiemPN_DuyetCheoXuoi(int currDong, int currCot)
    {
        long diemTong = 0;
        int soQuanAI = 0;
        int soQuanPlayer = 0;
        for (int i = 1; i < 6 && currCot + i <= boardSize && currDong + i <= boardSize; i++)
        {
            if (matrix[currDong + i, currCot + i] == "o")
            {
                soQuanAI++;
                break;
            }
            else if (matrix[currDong + i, currCot + i] == "x")
            {
                soQuanPlayer++;
            }
            else
            {
                break;
            }
        }
        for (int i = 1; i < 6 && currCot - i > 0 && currDong - i > 0; i++)
        {
            if (matrix[currDong - i, currCot - i] == "o")
            {
                soQuanAI++;
                break;
            }
            else if (matrix[currDong - i, currCot - i] == "x")
            {
                soQuanPlayer++;
            }
            else
            {
                break;
            }
        }
        if (soQuanAI == 2) return 0;
        diemTong += MangDiemPhongNgu[soQuanPlayer];
        return diemTong;

    }
    #endregion

    public RanDomMeme ranMEME;
    public int sonuocCo = 0;
    public void danhCo(int r, int c)//AI
    {
        if (r > boardSize && c> boardSize) return;
        foreach (Cell cell in listCell)
        {
            if(cell.row==r&& cell.col == c && currentTurn == "o" && matrix[r,c] =="")
            {
                cell.changeImagine(currentTurn);
                sonuocCo++;
                cellStack.Push(cell);
                if (checkWin(cell.row, cell.col)&&drawGame==false)
                {
                    if (currentTurn == "o")
                    {
                        audioManager.PlaySFX(audioManager.loseClip);
                    }else if (currentTurn == "x")
                    {
                        audioManager.PlaySFX(audioManager.winClip);
                    }
                    theWinner = matrix[cell.row, cell.col];
                    GameObject endWindow = Instantiate(cell.EndGameUI, cell.canvas);
                    endWindow.GetComponent<EndGameFunction>().updateMove(sonuocCo);
                    endWindow.GetComponent<EndGameFunction>().drawImage.enabled = false;
                    endWindow.GetComponent<EndGameFunction>().WI.setImagine();
                    endWindow.GetComponent<EndGameFunction>().RanDomMeme.GetRandomMEME();
                    Debug.Log(matrix[cell.row, cell.col] + " player WIn!");
                }
                else if (drawGame == true)
                {
                    GameObject endWindow = Instantiate(cell.EndGameUI, cell.canvas);
                    endWindow.GetComponent<EndGameFunction>().drawImage.enabled = true;
                    Debug.Log(drawGame);
                }
                currentTurn = "x";
            }
        }
    }
    #endregion
}
