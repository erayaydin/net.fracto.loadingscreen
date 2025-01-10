namespace Fracto.LoadingScreen.States
{
    /// <summary>
    /// Represents the "WaitingForLoad" state of the loading screen.
    /// In this state, various UI elements are reset and hidden.
    /// </summary>
    public class WaitingForLoadState : ILoadingState
    {
        /// <summary>
        /// Enters the "WaitingForLoad" state and prepares the loading screen context.
        /// Hides elements like the spinner, sets the canvas group to transparent and non-interactive,
        /// and clears any text or progress indicators.
        /// </summary>
        /// <param name="context">The loading screen context, which contains UI elements to manipulate.</param>
        public void Enter(LoadingScreen context)
        {
            // Deactivate the loading spinner
            var spinner = context.GetSpinner();
            spinner.gameObject.SetActive(false);

            // Reset the canvas group to be transparent and non-interactive
            var canvasGroup = context.GetCanvasGroup();
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;

            // Clear the status text
            var statusText = context.GetStatusText();
            if (statusText)
            {
                statusText.text = "";
            }

            // Reset the progress bar to its initial state
            var progressBar = context.GetProgressBar();
            if (progressBar)
            {
                progressBar.value = 0;
            }

            // Clear the title text
            var title = context.GetTitle();
            if (title)
            {
                title.text = "";
            }

            // Clear the description text
            var description = context.GetDescription();
            if (description)
            {
                description.text = "";
            }
        }

        /// <summary>
        /// Updates the "WaitingForLoad" state. 
        /// Currently, no actions are performed in this state during the update.
        /// </summary>
        /// <param name="context">The loading screen context.</param>
        public void Update(LoadingScreen context) { }

        /// <summary>
        /// Exits the "WaitingForLoad" state.
        /// Currently, no actions are performed when exiting this state.
        /// </summary>
        /// <param name="context">The loading screen context.</param>
        public void Exit(LoadingScreen context) { }
    }
}