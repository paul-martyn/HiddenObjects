using System.Threading.Tasks;

namespace CodeBase.Infrastructure.States
{
    public interface IState
    {
        public void Enter();
        public void Exit();
    }
}