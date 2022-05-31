using Spine.Unity;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public GameObject highlighter;
    public Actions actions;

    [SerializeField]
    private int hp, damage;
    [SerializeField]
    private GameObject hpGui;
    [SerializeField]
    private Transform battlePos;

    private Vector3 initialPos;
    private bool active = false, isDead = false, isPlayer = false;
    private float hpBarMultiplayer; //HP bar scale uses it
    private SkeletonAnimation skeletonAnimation;

    private void Awake()
    {
        initialPos = transform.position;
        highlighter.SetActive(false);
        hpBarMultiplayer = (float)10 / hp;
        skeletonAnimation = gameObject.GetComponent<SkeletonAnimation>();
        isPlayer = GetComponent<Player>();
        actions = GameObject.FindGameObjectWithTag("Logic").GetComponent<Actions>();

        InitiateBattlePos();
    }

    public int Damage()
    {
        return damage;
    }

    public void TakeDamage(int damage)
    {
        Mathf.Abs(hp -= damage);
        UpdateHpUi();
        if (hp <= 0)
            Death();
    }

    public void SetActiveStatus(bool status)
    {
        active = status;
    }

    public bool GetActiveStatus()
    {
        return active;
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    public bool GetIsPlayer()
    {
        return isPlayer;
    }

    public void SetAnimation(string animName, bool loop)
    {
        skeletonAnimation.AnimationName = animName;
        skeletonAnimation.loop = loop;
    }

    public void GotoBattlePos()
    {
        transform.position = battlePos.position;
    }

    public void GotoInitialPos()
    {
        transform.position = initialPos;
    }

    private void Death()
    {
        highlighter.SetActive(false);
        hpGui.SetActive(false);
        GetComponent<MeshRenderer>().enabled = false;
        isDead = true;
    }

    private void UpdateHpUi()
    {
        hpGui.GetComponent<Transform>().localScale = new Vector3(hp * hpBarMultiplayer, 1, 1);
    }

    private void InitiateBattlePos()
    {
        if (isPlayer)
        {
            battlePos = GameObject.FindGameObjectWithTag("PlayerPoint").transform;
        }
        else
        {
            battlePos = GameObject.FindGameObjectWithTag("EnemyPoint").transform;
        }
    }
}
