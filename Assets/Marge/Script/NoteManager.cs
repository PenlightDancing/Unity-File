using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public static NoteManager instance;

    private static Queue<NoteBase> notes = new Queue<NoteBase>();
    public int maxNoteCount;

    [HideInInspector]
    public int curNoteCount;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(NoteActiver());
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void EnQueue(NoteBase note)
    {
        notes.Enqueue(note);
    }

    private IEnumerator NoteActiver()
    {
        while (true)
        {
            if (notes.Count > 0)
            {
                if (SoundManager.instance.curMusic.isPlaying)
                {
                    if ((notes.Peek().noteTime) - Generator.instance.prevTime <= SoundManager.instance.curMusic.time)
                    {
                        GameObject peakNote = notes.Dequeue().gameObject;
                        peakNote.SetActive(true);
                        curNoteCount++;
                    }
                }
            }
            yield return null;
        }
    }
}