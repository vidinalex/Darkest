using UnityEngine;

public class Player : MonoBehaviour
{
    private BaseCharacter baseCharacter;

    private void Awake()
    {
        baseCharacter = GetComponent<BaseCharacter>();
    }

    void Update()
    {
        HighlightUpdate();
    }

    private void HighlightUpdate()
    {
        if (baseCharacter.GetActiveStatus())
        {
            baseCharacter.highlighter.SetActive(true);
        }
        else
        {
            baseCharacter.highlighter.SetActive(false);
        }
    }
}
