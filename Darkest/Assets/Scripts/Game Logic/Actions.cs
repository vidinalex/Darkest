using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    public bool isPlayerTurn = false;
    public bool isSelectedAttack = false;
    [SerializeField]
    private GameObject Gui, plPoint, enPoint, endGameImage;
    [SerializeField]
    private FocusSwitcher focusSwitcher;
    private List<GameObject> characters = new List<GameObject>(), charactersQueue = new List<GameObject>();
    private GameObject selectedCharacter;


    public void AttackButtonSetActive(bool state)
    {
        Gui.SetActive(state);
        isPlayerTurn = true;
    }

    public void AttackButtonSelected()
    {
        Gui.SetActive(false);
        isSelectedAttack = true;
    }

    public void AttackEnemy(GameObject underAttackChara)
    {
        isSelectedAttack = false;

        CallSetFocus(selectedCharacter, underAttackChara);

        selectedCharacter.SendMessage("RegularAttack", underAttackChara.GetComponent<BaseCharacter>());
    }

    public void CallSetFocus(GameObject selectedCharacter, GameObject underAttackChara)
    {
        focusSwitcher.SetFocused(selectedCharacter, underAttackChara);
    }

    public void CallResetFocus()
    {
        focusSwitcher.ResetFocus();
    }

    public void NextTurn()
    {
        if (selectedCharacter)
            selectedCharacter.SendMessage("SetActiveStatus", false);

        if (charactersQueue.Count == 0)
        {
            charactersQueue = new List<GameObject>(characters);
        }

        if (CheckEndGame())
        {
            endGameImage.SetActive(true);
            return;
        }

        Gui.SetActive(false);
        isPlayerTurn = false;

        StartCoroutine(WaitRoutine());
    }

    public bool CheckEndGame()
    {
        int players = 0, enemy = 0;
        foreach (GameObject item in characters)
        {
            if (!item.GetComponent<BaseCharacter>().GetIsDead())
            {
                if(item.GetComponent<BaseCharacter>().GetIsPlayer())
                    players++; else
                    enemy++;
            }
        }
        if(players == 0 || enemy == 0)
        {
            return true;
        }
        return false;
    }

    IEnumerator WaitRoutine()
    {
        //Debug.Log("Now waiting 1 sec before next turn");

        yield return new WaitForSeconds(1);

        SetRandomCharaActive();
    }

    private void Awake()
    {
        SetTeams();
    }

    private void Start()
    {
        NextTurn();
    }

    private void SetTeams()
    {
        characters.AddRange(GameObject.FindGameObjectsWithTag("Character"));
    }

    private void SetRandomCharaActive()
    {
        int index = Random.Range(0, charactersQueue.Count);
        if (charactersQueue.Count == 0)
        {
            NextTurn();
            return;
        }
        selectedCharacter = charactersQueue[index];

        if (!selectedCharacter)
        {
            //charactersQueue.RemoveAt(index);
            SetRandomCharaActive();
            return;
        }

        charactersQueue.RemoveAt(index);
       
        if (selectedCharacter.GetComponent<BaseCharacter>().GetIsDead())
        {
            SetRandomCharaActive();
            return;
        }

        selectedCharacter.SendMessage("SetActiveStatus", true);

        Debug.Log(selectedCharacter);

        if (selectedCharacter.GetComponent<Player>() != null)
        {
            AttackButtonSetActive(true);
        }
        else
        {
            selectedCharacter.SendMessage("ChooseTarget", GenerateListOfPlayers());
        }
    }

    private List<GameObject> GenerateListOfPlayers()
    {
        List<GameObject> temp = new List<GameObject>();
        foreach (GameObject player in characters)
        {
            if (player.GetComponent<Player>() && !player.GetComponent<BaseCharacter>().GetIsDead())
            {
                temp.Add(player);
            }
        }
        return temp;
    }
}
