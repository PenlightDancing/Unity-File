using UnityEngine;
using Valve.VR.InteractionSystem;
using System.Collections;
using System.Collections.Generic;

public class PhysicsTest : MonoBehaviour
{
    public ParticleSystem EffterSystem;
    private Hand Hands;
    public bool SpinData;
    public bool StartSpin;
    private SpinNote SpinNoteObject;

    private bool Effter;

    [SerializeField]
    private Vector3 PrePos = Vector3.zero;

    [SerializeField]
    private GameObject pos;
    private ParticleEffter particleeffter;
    private TestController testController;
    // Start is called before the first frame update

    private void Start()
    {
        testController = transform.GetComponentInParent<TestController>();
        transform.Rotate(90, 0, 0);
        transform.GetComponent<CapsuleCollider>().height = 6.283252f;
        transform.GetComponent<CapsuleCollider>().center = new Vector3(0, 3.220993f, 0);
        particleeffter = GameObject.Find("ParticleParent").GetComponent<ParticleEffter>();
        for (int i = 0; i < 20; i++)
        {
            particleeffter.AddParticle();
        }
        SpinNoteObject = null;
        Hands = transform.parent.GetComponent<Hand>();
        pos = transform.GetChild(0).gameObject;
        GameObject.Find("JsonCheck").GetComponent<Json>().ChangeControllerModel();
        
    }

    private void FixedUpdate()
    {
        if(pos == null)
        {
            pos = transform.GetChild(0).gameObject;
        }
        float x = Mathf.Abs(Mathf.Abs(PrePos.x) - Mathf.Abs(pos.transform.position.x));
        float y = Mathf.Abs(Mathf.Abs(PrePos.y) - Mathf.Abs(pos.transform.position.y));
        if (x * x + y * y > 0.01f && UIManager.instance.GetEffterCheck())
        {
            PrePos = pos.transform.position;
            particleeffter.PlayParticle(PrePos, transform.rotation);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (StartSpin)
        {
            RaycastHit hit;
            if (Physics.BoxCast(transform.position, new Vector3(transform.localScale.x, transform.localScale.y, transform.GetComponent<CapsuleCollider>().height / 10) / 5, -transform.forward, out hit))
            {
                if (hit.transform.GetComponent<PhysicsTest>() == null)
                {
                    return;
                }
                if (hit.transform.GetComponent<PhysicsTest>().GetHand().handType != GetHand().handType)
                {
                    switch (testController.ControllerList)
                    {
                        case Valve.VR.SteamVR_Input_Sources.LeftHand:
                            SpinNoteObject.LeftHandle = true;
                            break;

                        case Valve.VR.SteamVR_Input_Sources.RightHand:
                            SpinNoteObject.RightHandle = true;
                            break;
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawRay(ray);
        // Gizmos.DrawWireCube(ray.origin, new Vector3(transform.localScale.x, transform.localScale.y, transform.GetComponent<CapsuleCollider>().height));
    }

    public void SetData(bool Data)
    {
        SpinData = Data;
    }

    public Hand GetHand()
    {
        return Hands;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<NoteBase>() != null)
        {
            if (other.GetComponent<NoteBase>().noteKind == NoteKind.Spin)
            {
                SpinNoteObject = other.GetComponent<SpinNote>();
                StartSpin = true;
                Hands.TriggerHapticPulse(ushort.MaxValue);
            }
        }
        else if (other.GetComponentInParent<NoteBase>() != null)
        {
            if (other.GetComponentInParent<NoteBase>().noteKind == NoteKind.Long && other.GetComponentInParent<NoteBase>().noteHands == testController.ControllerList)
            {
                if(other.GetComponentInParent<LongNote>().isFristEnter == false)
                {
                    other.GetComponentInParent<LongNote>().isFristEnter = true;
                    UIManager.instance.GetCombo();
                    UIManager.instance.GetScore(150 + (UIManager.instance.nCombo - 1) * 3);
                }
                else if(other.GetComponentInParent<LongNote>().isLastEnter == false)
                {
                    other.GetComponentInParent<LongNote>().isLastEnter = true;
                    UIManager.instance.GetCombo();
                    UIManager.instance.GetScore(150 + (UIManager.instance.nCombo - 1) * 3);
                }
                other.GetComponentInParent<LongNote>().isChecking = true;
                Hands.TriggerHapticPulse(ushort.MaxValue);
            }
        }
        else if (other.GetComponentInParent<TestController>() != null)
        {
            if (other.transform.parent.GetComponent<TestController>().ControllerList != testController.ControllerList)
            {
                Hands.TriggerHapticPulse(ushort.MaxValue);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<NoteBase>() != null)
        {
            if (other.GetComponent<NoteBase>().noteKind == NoteKind.Normal && other.GetComponent<NoteBase>().noteHands == testController.ControllerList)
            {
                Destroy(other.gameObject);
                Hands.TriggerHapticPulse(ushort.MaxValue);
                UIManager.instance.GetCombo();
                UIManager.instance.GetScore(300 + (UIManager.instance.nCombo - 1) * 3);
            }
            else if (other.GetComponent<NoteBase>().noteKind == NoteKind.Yonta && other.GetComponent<NoteBase>().noteHands == testController.ControllerList)
            {
                if (other.GetComponent<YontaNote>().YonTaCount > 0)
                {
                    other.GetComponent<YontaNote>().YonTaCount--;
                }
                else
                {
                    Destroy(other.gameObject);
                    UIManager.instance.GetCombo();
                }
                UIManager.instance.GetScore(300 + (UIManager.instance.nCombo - 1) * 3);
                Hands.TriggerHapticPulse(ushort.MaxValue);
            }
        }
        else if (other.GetComponentInParent<NoteBase>() != null)
        {
            if (other.GetComponentInParent<SlideNote>() != null && other.GetComponentInParent<SlideNote>().noteHands == testController.ControllerList)
            {
                if (other.GetComponent<NoteLeftTrigger>() != null)
                {
                    other.GetComponent<NoteLeftTrigger>().colider();
                    other.GetComponentInParent<SlideNote>().Sethand(Hands);
                }
                else if (other.GetComponent<NoteRightTrigger>() != null)
                {
                    other.GetComponent<NoteRightTrigger>().colider();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<NoteBase>() != null)
        {
            if (other.GetComponent<NoteBase>().noteKind == NoteKind.Spin)
            {
                SpinNoteObject = null;
                StartSpin = false;
                //Debug.Log("Spin Start Test");
            }
        }
        else if(other.GetComponentInParent<NoteBase>()!= null)
        {
            if(other.GetComponentInParent<NoteBase>().noteKind == NoteKind.Long)
            {
                other.GetComponentInParent<LongNote>().isChecking = false;
            }
        }
    }

  
}