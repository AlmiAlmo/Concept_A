using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBehaviour : MonoBehaviour
{
    public Material deselectedMaterial;
    public Material selectedMaterial;
    public bool isSelected;

    private void Awake()
    {
        isSelected = false;
        SetMaterial(deselectedMaterial);
    }

    public void SetSelected(bool newState)
    {
        if(isSelected == newState) { return; }

        isSelected = newState;
        if(isSelected) { SetMaterial(selectedMaterial); }
        else { SetMaterial(deselectedMaterial); }
    }

    void SetMaterial(Material newMaterial)
    {
        if (newMaterial == null) { return; }

        transform.gameObject.GetComponent<Renderer>().material = newMaterial;
    }

    Material GetCurrentMaterial()
    {
        return transform.GetComponent<Renderer>().material;
    }


}
