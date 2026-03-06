using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour
{
    public static SceneMan instance;

    public int SceneCounter = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // keeps object alive between scenes
        }
        else
        {
            Destroy(gameObject); // prevents duplicate SceneMan
        }
    }

    public void SceneCounterIncrease()
    {
        SceneCounter += 1;
        Debug.Log("Scene Counter: " + SceneCounter);
    }
}