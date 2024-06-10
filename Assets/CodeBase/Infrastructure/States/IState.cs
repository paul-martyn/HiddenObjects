﻿namespace CodeBase.Infrastructure.States
{
    public interface IState: IExitableState
    {
        void Enter();
    }

    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload levelPayload);
    }
  
    public interface IExitableState
    {
        void Exit();
    }
}