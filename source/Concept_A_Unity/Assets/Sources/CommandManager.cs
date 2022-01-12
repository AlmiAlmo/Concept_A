using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    List<CommandBehaviour> selectedPawns = new List<CommandBehaviour>();


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var hit = GetClickHit();
            ClearSelected();
            TrySelectPawn(hit);
        }
        else if(Input.GetMouseButtonUp(1))
        {
            var hit = GetClickHit();
            MoveSelected(hit.point);
        }
    }

    void TrySelectPawn(RaycastHit hit)
    {
        var pawn = hit.collider.gameObject.GetComponent<CommandBehaviour>();
        if (pawn != null)
        {
            AddPawnToSelected(pawn);
        }
    }

    RaycastHit GetClickHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit;
    }

    void AddPawnToSelected(CommandBehaviour pawn)
    {
        pawn.BeginCommand();
        selectedPawns.Add(pawn);
    }

    void ClearSelected()
    {
        foreach(var pawn in selectedPawns)
        {
            pawn.EndCommand();
        }
        selectedPawns.Clear();
    }

    void MoveSelected(Vector3 movPos)
    {
        foreach(var pawn in selectedPawns)
        {
            pawn.Move(movPos);
        }
    }
}
