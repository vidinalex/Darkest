using UnityEngine;

public class FocusSwitcher : MonoBehaviour
{
    public string FocusedLayer = "Focused";
    public string UnfocusLayer = "Default";

    private GameObject charaFocused1, charaFocused2;

    public void SetFocused(GameObject chara1, GameObject chara2)
    {
        gameObject.SetActive(true);

        charaFocused1 = chara1;
        charaFocused2 = chara2;

        charaFocused1.layer = LayerMask.NameToLayer(FocusedLayer);
        charaFocused2.layer = LayerMask.NameToLayer(FocusedLayer);
    }

    public void ResetFocus()
    {
        charaFocused1.layer = LayerMask.NameToLayer(UnfocusLayer);
        charaFocused2.layer = LayerMask.NameToLayer(UnfocusLayer);

        gameObject.SetActive(false);
    }

    // On disable make sure to reset the current object, optional
    /*private void OnDisable()
    {
        charaFocused1.layer = LayerMask.NameToLayer(UnfocusLayer);
        charaFocused2.layer = LayerMask.NameToLayer(UnfocusLayer);
    }*/
}