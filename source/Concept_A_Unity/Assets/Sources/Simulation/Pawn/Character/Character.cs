using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Pawn
{
    public Information.Subject subject { get; private set; }
    public int id;
    public CharacterAI characterAI { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        characterAI = this.gameObject.AddComponent<CharacterAI>();
        subject = IdentificationManager.GenerateSubject();
        id = subject.subject;
    }
}
