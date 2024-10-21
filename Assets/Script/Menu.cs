
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public audioMenu audioMenu;
    private void Awake()
    {
        audioMenu = FindObjectOfType<audioMenu>();
    }
    private void Start()
    {
/*        audioMenu.PlayMusic(audioMenu.menuGameMusicClip);*/

    }
    public void playButton()
    {
        audioMenu.musicAudioSource.Pause();
        SceneManager.LoadScene(1);
        Screen.fullScreen = true;
    }
    public void ExitButton()
    {
/*        Application.Quit();*/
        Screen.fullScreen=false;
    }
}
