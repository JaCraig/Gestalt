namespace Gestalt.Core.Interfaces
{
    /// <summary>
    /// Application Interface
    /// </summary>
    public interface IApplication
    {
        /// <summary>
        /// Called when [started].
        /// </summary>
        void OnStarted();

        /// <summary>
        /// Called when [stopped].
        /// </summary>
        void OnStopped();

        /// <summary>
        /// Called when [stopping].
        /// </summary>
        void OnStopping();
    }
}