using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fracto.LoadingScreen.States
{
    /// <summary>
    /// Represents the "Continue" state of the loading screen.
    /// This state manages the countdown timer and the transition to the "Ending" state when the timer expires or the user presses any key.
    /// </summary>
    public class ContinueState : ILoadingState
    {
        /// <summary>
        /// Animator parameter for controlling the "Continue" animation.
        /// </summary>
        private static readonly int Continue = Animator.StringToHash("Continue");
        
        /// <summary>
        /// The countdown slider displayed in the "press any key" section of the loading screen.
        /// </summary>
        private Slider _countdownSlider;

        /// <summary>
        /// The text component that displays the countdown time.
        /// </summary>
        private TMP_Text _countdownText;

        /// <summary>
        /// The countdown timer, in seconds.
        /// </summary>
        private float _timer;

        /// <summary>
        /// Enters the "Continue" state, initializing the countdown timer and UI elements.
        /// </summary>
        /// <param name="context">The loading screen context that contains UI elements and scene configuration.</param>
        public void Enter(LoadingScreen context)
        {
            // Retrieve countdown slider and text components from the context.
            _countdownSlider = context.GetCountdownSlider();
            _countdownText = context.GetCountdownText();
            
            // Initialize the timer with the configured countdown value.
            _timer = context.SceneLoadContext.ContinueToWait;
            
            // Enable the "Continue" animation.
            var animator = context.GetAnimator();
            if (animator)
                animator.SetBool(Continue, true);
        }

        /// <summary>
        /// Updates the "Continue" state, decreasing the timer and updating the countdown UI elements.
        /// Transitions to the "Ending" state when the countdown reaches zero or the user presses any key.
        /// </summary>
        /// <param name="context">The loading screen context that contains UI elements and data.</param>
        public void Update(LoadingScreen context)
        {
            // Decrease the countdown timer.
            if (_timer > 0)
                _timer -= Time.unscaledDeltaTime;
            
            // Update the countdown slider to reflect the current timer value.
            if (_countdownSlider)
                _countdownSlider.value = _timer;
            
            // Update the countdown text to display the remaining time
            if (_countdownText)
                _countdownText.text = Mathf.Round(_countdownSlider.value).ToString(CultureInfo.InvariantCulture);

            // Check if any key is pressed or the timer has expired, then transition to the "Ending" state.
            if (Input.anyKey || _timer <= 0)
                context.SetState(new EndingState());
        }

        /// <summary>
        /// Exits the "Continue" state, disabling the "Continue" animation.
        /// </summary>
        /// <param name="context">The loading screen context.</param>
        public void Exit(LoadingScreen context)
        {
            var animator = context.GetAnimator();
            if (animator)
                animator.SetBool(Continue, false);
        }
    }
}