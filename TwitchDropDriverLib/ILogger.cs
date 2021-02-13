namespace TwitchDropDriverLib
{
    public interface ILogger
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogCritical(string message);
        void LogDebug(string message);
    }
}