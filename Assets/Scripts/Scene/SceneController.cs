using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static void OnClickMainMenu()
    {
        SceneManager.LoadScene(GameParameters.SceneName.MAIN_MENU);
    }

    public static void OnClickLevel1()
    {
        SceneManager.LoadScene(GameParameters.SceneName.LEVEL_1);
    }

    public static void OnClickLevel2()
    {
        SceneManager.LoadScene(GameParameters.SceneName.LEVEL_2);
    }

    public static void OnClickLevel3()
    {
        SceneManager.LoadScene(GameParameters.SceneName.LEVEL_3);
    }


}
