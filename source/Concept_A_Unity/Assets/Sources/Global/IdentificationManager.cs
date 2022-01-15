using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentificationManager 
{
    static int count = 1; 

    public static Information.Subject GenerateSubject()
    {
        var newSubject = new Information.Subject(count);
        count++;
        return newSubject;
    }
    
}
