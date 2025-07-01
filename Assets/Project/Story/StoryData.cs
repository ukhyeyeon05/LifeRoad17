using System;

[Serializable]
public class StoryData
{
    public string question;
    public string[] choices = new string[3];
    public string[] results = new string[3];
    public ChoiceEffect[] choiceEffects = new ChoiceEffect[3];
}
