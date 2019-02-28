using HosingConsole.Config;
using HosingConsole.Model;

namespace HosingConsole.MyTask
{
    public interface IStateManager
    {
        StateModel ServiceState { get; }

        void SetServiceState(StateModel state);

        CompilerOptions ReturnConfig();
    }
}
