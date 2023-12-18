using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    /// <summary>
    /// class SceneController : MonoBehavior
    /// =======================================================================
    /// Control scene transition using keyboard input with screen-fading
    /// between scenes;
    /// 
    /// Input:
    /// -----------------------------------------------------------------------
    /// Animator animator:
    /// Animation controller of the fade screen.
    /// </summary>


    public Animator animator;
    public KeyCode demoSceneKey, slideSceneKey;
    private int nextSceneId;

    void Update()
    {
        // If H-key is pressed, load Gardenia_02.
        if (Input.GetKeyDown(demoSceneKey))
        {
            FadeToScene(1);
        }
        // If G-key is pressed, load Gardenia_01.
        if (Input.GetKeyDown(slideSceneKey))
        {
            FadeToScene(0);
        }
    }

    /// <summary>
    /// void FadeToScene(): handles the triggering process of scene transition. 
    /// </summary>
    /// 
    /// <param name="sceneId">
    ///     Build index for the scene to transition to.
    /// </param>
    public void FadeToScene(int sceneId)
    {
        // Update the build index for next scene
        nextSceneId = sceneId;

        // Trigger the FadeOut parameter of the animator.
        animator.SetTrigger("FadeOut");
    }

    /// <summary>
    /// void OnFadeComplete(): load the next scene based upon its build index.
    /// </summary>
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(nextSceneId);
    }
}
