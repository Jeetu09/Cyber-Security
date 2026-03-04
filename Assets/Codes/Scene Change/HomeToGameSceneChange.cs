using UnityEngine;
using UnityEngine.SceneManagement;
public class HomeToGameSceneChange : MonoBehaviour
{
    public void ChangeScene()
    { 
        SceneManager.LoadScene("Gaming");
    }
}
