using UnityEngine;

namespace Fracto.LoadingScreen.States
{
    /// <summary>
    /// Represents the "Loaded" state of the loading screen.
    /// This state handles the final wait time before transitioning to the next state, which could be either the "Continue" or "Ending" state.
    /// </summary>
    public class LoadedState : ILoadingState
    {
        /// <summary>
        /// A fake wait time counter to simulate a minimum wait time before transitioning to the next state.
        /// </summary>
        private float _fakeWait;

        /// <summary>
        /// The scene load context containing configuration data for the loading process.
        /// </summary>
        private SceneLoadContext _sceneLoadContext;
        
        /// <summary>
        /// Enters the "Loaded" state, initializing necessary variables for the state.
        /// </summary>
        /// <param name="context">The loading screen context that contains the current scene's configuration.</param>
        public void Enter(LoadingScreen context)
        {
            // Initialize the fake wait timer with the current loading time.
            _fakeWait = context.LoadingTime;

            // Store the scene load context to access its settings later.
            _sceneLoadContext = context.SceneLoadContext;
        }

        /// <summary>
        /// Updates the "Loaded" state by simulating a minimum wait time.
        /// Once the minimum wait time has passed, it transitions to either the "Continue" or "Ending" state.
        /// </summary>
        /// <param name="context">The loading screen context that contains UI elements and data.</param>
        public void Update(LoadingScreen context)
        {
            // Increment the fake wait time using unscaled delta time.
            _fakeWait += Time.unscaledDeltaTime;
            
            // Retrieve the minimum wait time from the scene load context.
            var minimumWait = _sceneLoadContext.MinimumWait;
            
            // If the fake wait time is still below the minimum, do nothing and wait.
            if (_fakeWait < minimumWait)
                return;

            // Transition to the "Continue" state if enabled, otherwise transition to the "Ending" state.
            context.SetState(_sceneLoadContext.ShowContinue ? new ContinueState() : new EndingState());
        }

        /// <summary>
        /// Exits the "Loaded" state, performing any necessary cleanup.
        /// </summary>
        /// <param name="context">The loading screen context.</param>
        public void Exit(LoadingScreen context)
        {
            // Deactivate the spinner as the loading is complete.
            var spinner = context.GetSpinner();
            spinner.gameObject.SetActive(false);
        }
    }
}