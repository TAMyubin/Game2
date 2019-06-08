using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWSEnemy : SimpleFSM
{/*远战小兵*/

    protected override void SonAttackStart() {
        GameObject go = Instantiate(bullet, transform.position, transform.rotation);
        go.GetComponent<OrdinaryEnemyBullet>().SetPos(PlayerTransform.position);
    }
}
