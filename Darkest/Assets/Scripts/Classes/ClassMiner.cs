using System.Collections;
using UnityEngine;

public class ClassMiner : MonoBehaviour
{
    [SerializeField]
    private string chargeAnimName = "PickaxeCharge", recieveAnimName = "Damage", idleAnimName = "Idle";
    private BaseCharacter attacker;
    private Actions actions;

    private void Awake()
    {
        attacker = GetComponent<BaseCharacter>();
        actions = GameObject.FindGameObjectWithTag("Logic").GetComponent<Actions>();
    }

    public void RegularAttack(BaseCharacter receiver)
    {
        StartCoroutine(RegularAttackRoutine(receiver));
    }

    private IEnumerator RegularAttackRoutine(BaseCharacter receiver)
    {
        float duration = 1f;
        float normalizedTime = 0;

        //Debug.Log(attacker);
        attacker.GotoBattlePos();
        receiver.GotoBattlePos();

        attacker.SetAnimation(chargeAnimName, false);
        receiver.SetAnimation(recieveAnimName, false);

        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }

        receiver.TakeDamage(attacker.Damage());

        attacker.SetAnimation(idleAnimName, true);
        receiver.SetAnimation(idleAnimName, true);

        attacker.GotoInitialPos();
        receiver.GotoInitialPos();

        actions.CallResetFocus();
        actions.NextTurn();
    }
}
