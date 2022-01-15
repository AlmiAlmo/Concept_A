using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalUI : MonoBehaviour
{
    List<InterractableObject> selected = new List<InterractableObject>();
    UserCommander userCommander;

    static int count = 0;
    int myNum = 0;

    private void Start()
    {
        userCommander = this.gameObject.AddComponent<UserCommander>();
        myNum = count;
        count++;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var hit = GetClickHit();
            ClearSelected();
            TrySelect(hit);
        }
        else if(Input.GetMouseButtonUp(1))
        {
            var hit = GetClickHit();
            MoveSelected(hit.point);
        }
    }

    void TrySelect(RaycastHit hit)
    {
        var interractable = hit.collider.gameObject.GetComponent<InterractableObject>();
        if (interractable != null)
        {
            AddInterractableToSelected(interractable);
        }
    }

    RaycastHit GetClickHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit;
    }

    void AddInterractableToSelected(InterractableObject interractable)
    {
        interractable.selection.SetSelected(true);
        selected.Add(interractable);
    }

    void ClearSelected()
    {
        foreach(var interractable in selected)
        {
            interractable.selection.SetSelected(false);
        }
        selected.Clear();
    }

    void MoveSelected(Vector3 movPos)
    {
        foreach(var interractable in selected)
        {
            var character = GetCharacter(interractable);
            userCommander.CreateMoveOrder(character, movPos);
        }
    }

    Character GetCharacter(InterractableObject interractabel)
    {
        var character = interractabel.gameObject.GetComponent<Character>();
        return character;
    }
}
