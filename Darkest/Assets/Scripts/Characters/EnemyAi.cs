using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    private BaseCharacter baseCharacter;

    public void ChooseTarget(List<GameObject> players)
    {
        
        GameObject target = players[Random.Range(0, players.Count)];
        baseCharacter.actions.CallSetFocus(gameObject, target);
        SendMessage("RegularAttack", target.GetComponent<BaseCharacter>());
    }

    private void Awake()
    {
        baseCharacter = GetComponent<BaseCharacter>();
    }

    void OnMouseOver()
    {
        if (baseCharacter.actions.isSelectedAttack && !baseCharacter.GetIsDead())
            baseCharacter.highlighter.SetActive(true);
    }

    void OnMouseExit()
    {
        baseCharacter.highlighter.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (baseCharacter.actions.isSelectedAttack)
            baseCharacter.actions.AttackEnemy(gameObject);
    }
}
