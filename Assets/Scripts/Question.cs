using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string question;
    public List<String> answers=new List<string>();
    public int correctAnswer;
    
}