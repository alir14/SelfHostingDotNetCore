using HosingConsole.Config;
using HosingConsole.Model;
using Microsoft.Extensions.Options;

namespace HosingConsole.MyTask
{
    public class StateManager: IStateManager
    {
        private readonly IOptions<CompilerOptions> _config;

        private StateModel _serviceState;
        public StateManager(IOptions<CompilerOptions> config)
        {
            _config = config;
            //_serviceState = new StateModel();
        }
        public StateModel ServiceState => _serviceState;

        public void SetServiceState(StateModel state)
        {
            _serviceState = state;
        }

        public CompilerOptions ReturnConfig()
        {
            return _config.Value;
        }
    }
}
