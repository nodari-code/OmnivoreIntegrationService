using OmnivoreIntegration.Communicator;
using OmnivoreIntegration.Logging;

namespace OmnivoreIntegration.Service
{
    public class BaseRepository
    {
        private RestSharpCommunicator communicator;
        private INLogManager logger;

        public BaseRepository(INLogManager nLogManager)
        {
            logger = nLogManager;
            communicator = new RestSharpCommunicator
            {
                Logger = nLogManager
            };
            if (logger != null) logger.LogDebug("RestSharpCommunicator created.");
        }

        protected RestSharpCommunicator Communicator
        {
            get { return communicator; }
        }

        protected INLogManager Logger
        {
            get { return logger; }
        }
    }
}