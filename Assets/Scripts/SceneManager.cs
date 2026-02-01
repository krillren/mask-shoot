using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScene
{
    Menu,
    Game,
    Loose,
    Intro,
    WinMechantDied,
    WinCitizensDied
}

public class MiniSceneManager : MonoBehaviour
{

    public void LoadScene(GameScene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    // raccourcis pratiques
    public void LoadMenu() => LoadScene(GameScene.Menu);
    public void LoadIntro() => LoadScene(GameScene.Intro);
    public void LoadGame() => LoadScene(GameScene.Game);
    public void LoadLoose() => LoadScene(GameScene.Loose);
    public void LoadWinMechantDied() => LoadScene(GameScene.WinMechantDied);
    public void LoadWinCitizensDied() => LoadScene(GameScene.WinCitizensDied);
}
