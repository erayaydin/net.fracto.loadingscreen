using System.Collections.Generic;
using UnityEngine;

namespace Fracto.LoadingScreen
{
    public struct SceneLoadContext
    {
        #region Properties

        /// <summary>
        /// Name of the scene to be loaded.
        /// </summary>
        public readonly string SceneName;

        /// <summary>
        /// Title to display on the loading screen.
        /// </summary>
        public readonly string Title;

        /// <summary>
        /// Description to display on the loading screen.
        /// </summary>
        public readonly string Description;

        /// <summary>
        /// Indicates whether to show the continue section.
        /// </summary>
        public readonly bool ShowContinue;

        /// <summary>
        /// Duration in seconds to wait in the continue section.
        /// </summary>
        public readonly int ContinueToWait;

        /// <summary>
        /// Minimum duration in seconds to display the loading screen.
        /// </summary>
        public readonly int MinimumWait;

        /// <summary>
        /// List of background images to display during loading.
        /// </summary>
        public readonly IList<Sprite> BackgroundImages;

        /// <summary>
        /// Speed of background image transitions.
        /// </summary>
        public readonly int BackgroundImageChangeSpeed;

        /// <summary>
        /// List of hint messages to display on the loading screen.
        /// </summary>
        public readonly IList<HintManager.Hint> Hints;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneLoadContext"/> struct.
        /// </summary>
        /// <param name="sceneName">Name of the scene to load.</param>
        /// <param name="title">Title to display on the loading screen (optional).</param>
        /// <param name="description">Description to display on the loading screen (optional).</param>
        /// <param name="continueToWait">Duration in seconds to wait in the continue section (default: 10 seconds).</param>
        /// <param name="backgroundImages">List of background images to display (optional).</param>
        /// <param name="hints">List of hint messages to display (optional).</param>
        /// <param name="minimumWait">Minimum duration in seconds to display the loading screen (default: 0 seconds).</param>
        /// <param name="backgroundImageChangeSpeed">Speed of background image transitions (default: 15).</param>
        public SceneLoadContext(
            string sceneName,
            string title = null,
            string description = null,
            int continueToWait = 10,
            IList<Sprite> backgroundImages = null,
            IList<HintManager.Hint> hints = null,
            int minimumWait = 0,
            int backgroundImageChangeSpeed = 15)
        {
            SceneName = sceneName;
            Title = title;
            Description = description;
            ShowContinue = continueToWait > 0;
            ContinueToWait = continueToWait;
            MinimumWait = minimumWait;
            BackgroundImages = backgroundImages;
            BackgroundImageChangeSpeed = backgroundImageChangeSpeed;
            Hints = hints;
        }
        
        #endregion
    }
}