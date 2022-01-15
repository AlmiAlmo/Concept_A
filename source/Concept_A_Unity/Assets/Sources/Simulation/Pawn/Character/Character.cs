using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Pawn
{
    public readonly Information.Subject subject = IdentificationManager.GenerateSubject();
    public int id;
    public CharacterAI characterAI { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        characterAI = this.gameObject.AddComponent<CharacterAI>();
        id = subject.subject;
    }
}
