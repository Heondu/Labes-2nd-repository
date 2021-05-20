using System.Collections.Generic;
using UnityEngine;

public class DialogueInteract : MonoBehaviour
{

}

public class Dialogue
{
    public Sprite face;
    public string name;
    public string content;
    public float interval;
}

public class DialogueCollection
{
    public List<Dialogue> dialogues = new List<Dialogue>();
    public bool isRead = false;
}
