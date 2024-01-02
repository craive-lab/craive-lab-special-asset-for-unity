using UnityEngine;
using UnityEngine.UI;


public class SlideDeckManager : MonoBehaviour
{
    /// <summary>
    /// class SlideDeckManager : MonoBehaviour
    /// =======================================================================
    /// Simple controller for handling panoramic images in immersive rooms.
    /// 
    /// User Inputs:
    /// -----------------------------------------------------------------------
    /// 1)  Image slideImageContainer:
    ///     Image component where the slides are displayed in.
    /// 2)  Sprites[] slides:
    ///     Panoramic images as a deck of slides.
    /// 3)  KeyCode nextSlideKey, lastSlideKey:
    ///     Key bindings for the navigation of the slide deck.
    ///     
    /// Private Inputs:
    /// -----------------------------------------------------------------------
    /// 1)  Animator transitionAnimator:
    ///     Animator that handles the fading between slide transitions.
    /// 2)  int id:
    ///     Tracking parameters for the index of the current slide.
    /// 3)  int slideCount:
    ///     Number of slides.
    /// </summary>

    [SerializeField]
    Image slideImageContainer;

    [SerializeField]
    Sprite[] slides;

    [SerializeField]
    KeyCode nextSlideKey = KeyCode.X,
            lastSlideKey = KeyCode.Z;

    Animator transitionAnimator;

    int id = 0, slideCount;

    private void Awake()
    {
        /* Getting the number of slides from user input */
        slideCount = slides.Length;

        /* Engage the transition animator */
        transitionAnimator = GetComponent<Animator>();

        /* Set the initial slide to be the first of user input */
        slideImageContainer.sprite = slides[id];
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
        /* Manipulate the index so that it stays within (0, slideCount) */
        if (slideId < 0) slideId += slideCount;
        slideId %= slideCount;

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
        slideImageContainer.sprite = slides[id];

        /* Fade in */
        transitionAnimator.SetTrigger("SlideFadeIn");
    }
}
