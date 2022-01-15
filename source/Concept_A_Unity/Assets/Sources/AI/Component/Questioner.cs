using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questioner<T>
{
    public bool isAnswerNeeded { get; private set; }

    public object question { get; private set; }

    public T answer { get; private set; }

    public void AnswerAs(T answer)
    {
        isAnswerNeeded = false;
        this.answer = answer;
    }

    public void SetQuestion(object question)
    {
        isAnswerNeeded = true;
        this.question = question;
    }
}
