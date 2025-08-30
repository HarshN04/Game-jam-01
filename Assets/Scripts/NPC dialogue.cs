using UnityEngine;
[CreateAssetMenu(fileName = "NewNPCDialogue" , menuName ="NPC Dialogue")]
public class NPCDialogue : ScriptableObject 
{
    public string npcName;
    public string npcPortrait;
    public string[] dialoguelines;
    public float typingspeed = 0.05f;
    public AudioClip voicesound;
    public float voicepitch = 1f;
    public bool[] autoprogresslines;
    public float autoprogressdelay = 1.5f;
}
