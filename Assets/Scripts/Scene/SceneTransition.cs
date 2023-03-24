using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{

    [SerializeField] private SceneName sceneNameGoto = SceneName.Scene1_Main;
    public void PerformTransition()
    {
        SceneTransitionManager.Instance.LoadAndSwitchScenes(sceneNameGoto.ToString());



    }


}
