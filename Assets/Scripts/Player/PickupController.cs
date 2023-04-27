using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.EventSystems;

public class PickupController : MonoBehaviour
{
    [Header("Particle References")]
    [SerializeField] GameObject pullParticle;
    [SerializeField] GameObject pushParticle;

    [SerializeField] TMP_Text interactionPromptText;
    [SerializeField] float interactionDistance;
    [SerializeField] LayerMask interactible;
    [SerializeField] public float pushForce;
    [SerializeField] public float pullSpeed;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] GenerateUpgrade generateUpgrade;
    public Highlight highlight;
    public Transform currentFocus;

    [SerializeField] bool ispickupFocus;

    public Transform handPosition;

    [Header("Sound")]
    public AudioSource DoorOpen;

    [Header("UI References")]
    public GameObject LookingAtDoorUI;
    public GameObject WinScreen;
    
    

    private void FixedUpdate()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionDistance, interactible);

        

        if (hit.collider == null && ispickupFocus != true)
        {
            interactionPromptText.SetText(" ");
            currentFocus = null;
            ispickupFocus = false;
            highlight.ToggleHighlight(false);

            
        }
        else
        {
            currentFocus = hit.collider.transform;
            interactionPromptText.SetText(currentFocus.gameObject.name);
            highlight = currentFocus.GetComponent<Highlight>();
            highlight.ToggleHighlight(true);

            


        }

        

        if (ispickupFocus == true)
        {
            MoveFocus();

        }
        else
        {
            return;
        }

        if (hit.collider.tag == "Door" && playerInput.actions["Interact"].WasPressedThisFrame())
        {
            DoorOpen.Play();
            Destroy(hit.collider.gameObject);
            Debug.Log(hit.collider.gameObject);
        }

        if (hit.collider.tag == "BossShrine" && playerInput.actions["Interact"].WasPressedThisFrame())
        {
            Destroy(hit.collider.gameObject);
            Time.timeScale = 1;
            WinScreen.SetActive(true);
            

        }



    }


    private void Update()
    {
        PickUpHandler();

    }



    public void PickUpHandler()
    {
        RaycastHit ray;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out ray, interactionDistance, interactible);


        if (playerInput.actions["Pull"].WasPressedThisFrame())
        {
            pullParticle.SetActive(true);
            if (currentFocus == null)
            {
                return;
                
            }
            else
            {
                ispickupFocus = true;
                currentFocus.GetComponent<Rigidbody>().useGravity = false;

            }
        }

        if (playerInput.actions["Pull"].WasReleasedThisFrame())
        {
            pullParticle.SetActive(false);
            Drop();
        }

        // If Chest is in range, generate upgrade
        if (playerInput.actions["Interact"].WasPerformedThisFrame() && ray.collider.gameObject.tag == "Chest")
        { 
            AbilityCard newAbility = generateUpgrade.PickUpgrade();
            
            PlayerStats.Instance.AddNewAbilityToInv(newAbility);

            generateUpgrade.RemoveUpgrade(newAbility);

            Destroy(ray.collider.gameObject);
        }

        if (playerInput.actions["Interact"].WasPerformedThisFrame() && ray.collider.gameObject.tag == "Door")
        {
            Debug.Log("is hitting door");

            Destroy(ray.collider.gameObject);
        }

        if (playerInput.actions["Interact"].WasPerformedThisFrame() && ray.collider.gameObject.tag == "BossShrine")
        {
            Destroy(ray.collider.gameObject);
            //SPAWN THREE ENEMIES
        }



    }

    private void MoveFocus()
    {
        Rigidbody focusRB = currentFocus.GetComponent<Rigidbody>();
        Vector3 movedir = (handPosition.position - currentFocus.position).normalized;
        float distohand = Vector3.Distance(handPosition.position, currentFocus.position);

        if (distohand > 0.25f)
        {
            focusRB.velocity = movedir * pullSpeed * Time.deltaTime;
        }
        else
        {
            focusRB.velocity = Vector3.zero;
            focusRB.transform.parent = handPosition.parent.transform;
        }

        
        
    }

    public void PushObject()
    {
  
       pushParticle.SetActive(true);

        if (currentFocus == null)
        {
            return;
        }

        Rigidbody focusRB = currentFocus.GetComponent<Rigidbody>();
        Drop();
        focusRB.AddForce(Camera.main.transform.forward * pushForce, ForceMode.Impulse);


        pushParticle.SetActive(false);


    }

    public void Drop()
    {
        if (currentFocus == null)
        { return; }
        ispickupFocus = false;
        currentFocus.GetComponent<Rigidbody>().useGravity = true;
        currentFocus.transform.parent = null;
        
    }

}
