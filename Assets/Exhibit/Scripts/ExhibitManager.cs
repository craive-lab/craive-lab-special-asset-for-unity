using UnityEngine;
using UnityEngine.UI;


public class ExhibitManager : MonoBehaviour
{
    /// <summary>
    /// class ExhibitManager : MonoBehaviour
    /// =======================================================================
    /// Simple controller for handling panoramic images in immersive rooms.
    /// 
    /// User Inputs:
    /// -----------------------------------------------------------------------
    /// 1)  Image exhibitItemCountImageContainer:
    ///     Image component where the exhibit items are displayed in.
    /// 2)  Sprites[] exhibitItems:
    ///     Panoramic images as a deck of exhibit items.
    /// 3)  KeyCode nextSlideKey, lastSlideKey:
    ///     Key bindings for the navigation of the exhibit.
    ///     
    /// Private Inputs:
    /// -----------------------------------------------------------------------
    /// 1)  Animator transitionAnimator:
    ///     Animator that handles the fading between transitions in exhibit.
    /// 2)  int id:
    ///     Tracking parameters for the index of the current exhibit item.
    /// 3)  int exhibitItemCount:
    ///     Number of exhibit items.
    /// </summary>

    [SerializeField]
    Image exhibitContainer;

    [SerializeField]
    Sprite[] exhibitItems;

    [SerializeField]
    KeyCode nextSlideKey = KeyCode.X,
            lastSlideKey = KeyCode.Z;

    Animator transitionAnimator;

    int id = 0, exhibitItemCount;

    private void Awake()
    {
        /* Getting the number of slides from user input */
        exhibitItemCount = exhibitItems.Length;

        /* Engage the transition animator */
        transitionAnimator = GetComponent<Animator>();

        /* Set the initial slide to be the first of user input */
        exhibitContainer.sprite = exhibitItems[id];
    }

    void Update()
    {
        /* If the key to the next slide is pressed, go to the next slide;
         * If the key to the last slide is pressed, go to the last slide. */
        if (Input.GetKeyDown(nextSlideKey)) LoadSlide(++id);
        if (Input.GetKeyDown(lastSlideKey)) LoadSlide(--id);

    }

    /// <summary>
    /// public void LoadSlide(int id)
    /// =======================================================================
    /// Load the slide associated with where it is located in the deck 
    /// (its index).
    /// 
    /// Input:
    /// -----------------------------------------------------------------------
    /// 1)  int id:
    ///     Index of the slide to load.
    /// </summary>
    /// <param name="slideId"></param>
    public void LoadSlide(int slideId)
    {
        /* Manipulate the index so that it stays within (0, exhibitItemCount) */
        if (slideId < 0) slideId += exhibitItemCount;
        slideId %= exhibitItemCount;

        /* Update slide index */
        id = slideId;

        /* Fade out */
        transitionAnimator.SetTrigger("SlideFadeOut");
    }

    /// <summary>
    /// public void OnSlideUnloaded()
    /// =======================================================================
    /// A public function used by the animator for loading the upcoming slide.
    /// </summary>
    public void OnSlideUnloaded()
    {
        /* Set the slide to one with a new index */
        exhibitContainer.sprite = exhibitItems[id];

        /* Fade in */
        transitionAnimator.SetTrigger("SlideFadeIn");
    }
}
