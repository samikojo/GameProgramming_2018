using TankGame.Systems;
using UnityEngine;

namespace TankGame.AI
{
    class ShootState : AIStateBase
    {
        public ShootState(EnemyUnit owner)
			: base( owner, AIStateType.Shoot )
		{
            AddTransition(AIStateType.Patrol);
            AddTransition(AIStateType.FollowTarget);
        }

        public override void StateActivated()
        {
            base.StateActivated();
        }

        public override void StateDeactivating()
        {
            base.StateDeactivating();
        }

        public override void Update()
        {
            if (!ChangeState())
            {
                Owner.Mover.Turn(Owner.Target.transform.position);
                Owner.Weapon.Shoot();
            }
        }

        private bool ChangeState()
        {
            if (Owner.Target.Health.CurrentHealth <= 0)
            {
                return Owner.PerformTransition(AIStateType.Patrol);
            }
            
            int playerLayer = LayerMask.NameToLayer("Player");
            int mask = Flags.CreateMask(playerLayer);

            Collider[] players = Physics.OverlapSphere(Owner.transform.position,
                Owner.ShootingDistance, mask);
            if (players.Length > 0)
            {
                PlayerUnit player =
                    players[0].gameObject.GetComponentInHierarchy<PlayerUnit>();
                Owner.Target = player;

                return false;
            }

            return Owner.PerformTransition(AIStateType.FollowTarget);
        }
    }
}
