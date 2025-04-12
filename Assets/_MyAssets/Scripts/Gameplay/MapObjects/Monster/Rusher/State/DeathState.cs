
namespace Monster.Rusher
{
    public class DeathState : BaseState<ContextParam>
    {
        public DeathState(MonsterStateMachine<ContextParam> context, ContextParam contextParam)
            : base(context, contextParam)
        {
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void Stay()
        {
        }
    }
}