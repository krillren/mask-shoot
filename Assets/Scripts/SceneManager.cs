using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScene
{
    Menu,
    Game,
    Loose
}

public class MiniSceneManager : MonoBehaviour
{

    public void LoadScene(GameScene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    // raccourcis pratiques
    public void LoadMenu() => LoadScene(GameScene.Menu);
    public void LoadGame() => LoadScene(GameScene.Game);
    public void LoadLoose() => LoadScene(GameScene.Loose);
}
