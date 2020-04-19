using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private bool keepTab = false;
    // Start is called before the first frame update
    void Start() {
#if UNITY_EDITOR
        if(keepTab) UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
#endif
    }

#if UNITY_EDITOR
    private void Update() {
        if (Input.GetKeyDown(KeyCode.K)) ScreenCapture.CaptureScreenshot("Light" + System.DateTime.Now.ToFileTime()+".png");
    }
#endif
}
