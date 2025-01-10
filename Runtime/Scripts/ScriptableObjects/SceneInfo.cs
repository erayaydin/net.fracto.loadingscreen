using System.Collections.Generic;
using UnityEngine;

namespace Fracto.LoadingScreen.ScriptableObjects
{
    /// <summary>
    /// Represents information about a scene for the loading screen, including hints, images, and descriptive details.
    /// </summary>
    [CreateAssetMenu(fileName = "SceneInfo", menuName = "Fracto/Loading Screen/Scene Info")]
    public class SceneInfo : ScriptableObject
    {
        #region Fields

        /// <summary>
        /// Name of the scene to load.
        /// </summary>
        [Tooltip("Name of the scene to load.")]
        public string sceneName;

        /// <summary>
        /// Title of the scene to display on the loading screen.
        /// </summary>
        [Tooltip("Title of the scene to display on the loading screen.")]
        public string title;

        /// <summary>
        /// Description of the scene to display on the loading screen.
        /// </summary>
        [Tooltip("Description of the scene to display on the loading screen.")]
        public string description;
        
        /// <summary>
        /// List of hints to display during the loading screen.
        /// </summary>
        [Tooltip("List of hints to display during the loading screen.")]
        public List<Hint> hints;

        /// <summary>
        /// List of background images to display during the loading screen.
        /// </summary>
        [Tooltip("List of background images to display during the loading screen.")]
        public List<Sprite> images;

        /// <summary>
        /// Default wait time (in seconds) for the "press any key" section. 
        /// A value of 0 means this section will not be shown. 
        /// This value can be overridden during the scene loading process.
        /// </summary>
        [Tooltip("Default wait time (in seconds) for the 'press any key' section. 0 means this section will not be shown. Can be overridden during scene loading.")]
        public int continueToWait;

        #endregion

        #region Methods

        /// <summary>
        /// Converts this object to a <see cref="SceneLoadContext"/> used for loading the scene.
        /// </summary>
        /// <param name="minimumWait">Minimum time to wait for loading (in seconds).</param>
        /// <param name="backgroundImageChangeSpeed">Speed at which background images change (in seconds).</param>
        /// <returns>A <see cref="SceneLoadContext"/> instance populated with this scene's data.</returns>
        public SceneLoadContext ToSceneLoadContext(int minimumWait = 0, int backgroundImageChangeSpeed = 15) =>
            new(
                sceneName,
                title,
                description,
                continueToWait,
                images,
                hints.ConvertAll(hint => hint.ToPrimitive()),
                minimumWait,
                backgroundImageChangeSpeed
            );

        #endregion
    }
}