using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Scene.LoadScene(StringDefines.Strings.MainMenuScene);
    }
}
