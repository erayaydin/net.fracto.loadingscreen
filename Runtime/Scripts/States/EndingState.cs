using UnityEngine;

namespace Fracto.LoadingScreen.States
{
    /// <summary>
    /// Represents the "Ending" state of the loading screen.
    /// This state is responsible for deactivating the loading screen and transitioning to the null state once loading is completed.
    /// </summary>
    public class EndingState : ILoadingState
    {
        /// <summary>
        /// Animator parameter for controlling the "Active" animation.
        /// </summary>
        private static readonly int Active = Animator.StringToHash("Active");

        /// <summary>
        /// Enters the "Ending" state, deactivating the loading screen's animation and triggering scene activation.
        /// </summary>
        /// <param name="context">The loading screen context containing the UI elements and scene state.</param>
        public void Enter(LoadingScreen context)
        {
            // Disable the "Active" animation to indicate the loading screen has finished.
            var animator = context.GetAnimator();
            if (animator)
                animator.SetBool(Active, false);

            // Trigger scene activation (enable scene interaction).
            context.SetSceneActivation(true);
        }

        /// <summary>
        /// Updates the "Ending" state, checking if the canvas group is fully faded out.
        /// Once the fade is complete, transitions to null state.
        /// </summary>
        /// <param name="context">The loading screen context that controls the scene flow.</param>
        public void Update(LoadingScreen context)
        {
            // Check if the canvas group has fully faded out, indicating that the loading screen is ready to be deactivated.
            if (context.GetCanvasGroup().alpha != 0)
                return;

            // Set the state to null, signaling the end of the loading sequence.
            context.SetState(null);
        }

        /// <summary>
        /// Exits the "Ending" state, finalizing the loading process.
        /// </summary>
        /// <param name="context">The loading screen context.</param>
        public void Exit(LoadingScreen context)
        {
            context.EndLoading();
        }
    }
}