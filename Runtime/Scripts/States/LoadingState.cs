using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fracto.LoadingScreen.States
{
    /// <summary>
    /// Represents the "Loading" state of the loading screen.
    /// This state handles the UI elements during the loading process, including updating progress, status, and background elements.
    /// </summary>
    public class LoadingState : ILoadingState
    {
        /// <summary>
        /// Animator parameter for "Active" state.
        /// </summary>
        private static readonly int Active = Animator.StringToHash("Active");

        /// <summary>
        /// Enters the "Loading" state and initializes loading screen elements.
        /// This includes showing the spinner, setting up the loading context, and updating UI elements.
        /// </summary>
        /// <param name="context">The loading screen context that contains UI elements and data.</param>
        public void Enter(LoadingScreen context)
        {
            // Reset the loading time
            context.LoadingTime = 0f;

            // Activate the spinner for loading indication
            var spinner = context.GetSpinner();
            spinner.gameObject.SetActive(true);

            // Start the loading process
            context.StartLoading();

            // Set the animator to active
            var animator = context.GetAnimator();
            if (animator)
                animator.SetBool(Active, true);

            // Get the loading context from the scene load context
            var loadingContext = context.SceneLoadContext;

            // Update the title and description based on the loading context
            UpdateTitle(context.GetTitle(), loadingContext.Title);
            UpdateDescription(context.GetDescription(), loadingContext.Description);

            // Update the countdown slider value based on the loading context
            UpdateCountdownSlider(context.GetCountdownSlider(), loadingContext.ContinueToWait);

            // If background images are provided, set them using the background manager
            if (loadingContext.BackgroundImages != null)
            {
                context.GetBackgroundManager().SetImages(
                    loadingContext.BackgroundImages,
                    loadingContext.BackgroundImageChangeSpeed
                );
            }

            // If hints are provided, set them using the hint manager
            if (loadingContext.Hints != null)
                context.GetHintManager().SetHints(loadingContext.Hints);
        }

        /// <summary>
        /// Updates the "Loading" state, including progress tracking and UI updates.
        /// </summary>
        /// <param name="context">The loading screen context that contains UI elements and data.</param>
        public void Update(LoadingScreen context)
        {
            // Increase the time spent during loading
            context.LoadingTime += Time.unscaledDeltaTime;
            
            // Retrieve current loading progress
            var currentProgress = context.GetLoadingProgress().progress;
            
            // Update the status text to show the current progress percentage
            var statusText = context.GetStatusText();
            if (statusText)
                statusText.text = Mathf.Round(currentProgress * 100) + "%";

            // Update the progress bar value based on the current progress
            var progressBar = context.GetProgressBar();
            if (progressBar)
                progressBar.value = currentProgress;

            // Wait until the loading is complete
            if (currentProgress < .9f)
                return;

            context.SetState(new LoadedState());
        }

        /// <summary>
        /// Exits the "Loading" state. Currently, no additional actions are required.
        /// </summary>
        /// <param name="context">The loading screen context.</param>
        public void Exit(LoadingScreen context) { }

        #region Helper Methods

        /// <summary>
        /// Updates the countdown slider with the specified value.
        /// </summary>
        /// <param name="countdownSlider">The countdown slider UI element.</param>
        /// <param name="value">The value to set for the countdown slider.</param>
        private static void UpdateCountdownSlider(Slider countdownSlider, int value)
        {
            if (!countdownSlider)
                return;

            countdownSlider.maxValue = value;
            countdownSlider.value = value;
        }

        /// <summary>
        /// Updates the description text UI element with the specified text.
        /// </summary>
        /// <param name="description">The TMP_Text UI element for the description.</param>
        /// <param name="text">The text to display in the description.</param>
        private static void UpdateDescription(TMP_Text description, string text)
        {
            if (!description || string.IsNullOrEmpty(text))
                return;
            
            description.text = text;
        }

        /// <summary>
        /// Updates the title text UI element with the specified text.
        /// </summary>
        /// <param name="title">The TMP_Text UI element for the title.</param>
        /// <param name="text">The text to display in the title.</param>
        private static void UpdateTitle(TMP_Text title, string text)
        {
            if (!title || string.IsNullOrEmpty(text))
                return;
            
            title.text = text;
        }

        #endregion
    }
}