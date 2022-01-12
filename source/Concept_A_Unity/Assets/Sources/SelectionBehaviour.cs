using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBehaviour : MonoBehaviour
{
    public Material deselectedMaterial;
    public Material selectedMaterial;
    public bool isSelected;

    public void SetSelected(bool newState)
    {
        if(isSelected == newState) { return; }

        isSelected = newState;
        if(isSelected) { SetPawnMaterial(selectedMaterial); }
        else { SetPawnMaterial(deselectedMaterial); }
    }

    private void SetPawnMaterial(Material newMaterial)
    {
        if (newMaterial == null) { return; }

        transform.gameObject.GetComponent<Renderer>().material = newMaterial;
    }

    private Material GetCurrentPawnMaterial()
    {
        return transform.GetComponent<Renderer>().material;
    }
}
