using UnityEngine;

namespace Fracto.LoadingScreen.ScriptableObjects
{
    /// <summary>
    /// Represents a hint shown on the loading screen.
    /// This ScriptableObject stores the hint text and the estimated reading time.
    /// </summary>
    [CreateAssetMenu(fileName = "Hint", menuName = "Fracto/Loading Screen/Hint")]
    public class Hint : ScriptableObject
    {
        /// <summary>
        /// The hint text displayed on the loading screen.
        /// </summary>
        [Tooltip("The hint text displayed on the loading screen.")]
        public string hint;

        /// <summary>
        /// The estimated reading time for the hint in seconds.
        /// This value helps manage the loading screen's pacing.
        /// </summary>
        [Tooltip("The estimated reading time for the hint in seconds.")]
        public int readTime;

        /// <summary>
        /// Converts this ScriptableObject to a Hint struct for further use.
        /// </summary>
        /// <returns>A new Hint struct containing the message and read time.</returns>
        public HintManager.Hint ToPrimitive() => new()
        {
            message = hint,
            readTime = readTime
        };
    }
}