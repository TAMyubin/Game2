using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCClownEnemy : NPCjinzhan
{

    protected override void SonStart() {

        ChaseBabyState chasebaby = new ChaseBabyState();//创建追逐baby状态
        chasebaby.DeleteTransition(Transition.disshort);
       
        chasebaby.DeleteTransition(Transition.FineTurret);

        ChasePlayerState chasePlayerState = new ChasePlayerState();
        chasePlayerState.DeleteTransition(Transition.dislong);
        chasePlayerState.DeleteTransition(Transition.CanAttack);
        chasePlayerState.DeleteTransition(Transition.GetHurt);
        chasePlayerState.DeleteTransition(Transition.FineTurret);


        Attack attackState = new Attack();
        attackState.DeleteTransition(Transition.disshort);
        attackState.DeleteTransition(Transition.GetHurt);


        HurtState hurtState = new HurtState();
        hurtState.DeleteTransition(Transition.disshort);
        hurtState.AddTransition(Transition.dislong, FSMStateID.ChaseBaby);

        DeathState deathState = new DeathState();

        ChaseTurretState chaseTurretState = new ChaseTurretState();
        chaseTurretState.DeleteTransition(Transition.CanAttackTurret);
        chaseTurretState.DeleteTransition(Transition.disshort);
        chaseTurretState.DeleteTransition(Transition.GetHurt);


        AttackTurret attackTurret = new AttackTurret();
        attackTurret.DeleteTransition(Transition.FineTurret);
        attackTurret.DeleteTransition(Transition.GetHurt);

        DeleteFSMState(FSMStateID.Chasing);
        DeleteFSMState(FSMStateID.Attack);
        DeleteFSMState(FSMStateID.ChaseTurret);
        DeleteFSMState(FSMStateID.AttackTurret);

    }
}
