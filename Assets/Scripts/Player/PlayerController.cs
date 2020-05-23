using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask movementMask;

    [SerializeField] private Interactable focus;

    private Camera cam;
    private PlayerMotor playerMotor;
    private CharacterAnimator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        playerAnimator = GetComponent<CharacterAnimator>();
        playerMotor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            DoMovement();

            DoInteraction();
        }

        DoWithdrawingWeaponAndSheath();
    }

    public void DoWithdrawingWeaponAndSheath()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            if (playerAnimator.IsCovered())
                playerAnimator.DrawWeapon();
            else
                playerAnimator.SheathWeapon();
        }
    }

    public void DoMovement()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100, movementMask))
        {
            playerMotor.MoveToPoint(hit.point);

            RemoveFocus();
        }
    }

    public void DoInteraction()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
                SetFocus(interactable);
        }
    }

    public void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;
            playerMotor.FollowTarget(newFocus);
        }
        newFocus.OnFocused(transform);
    }

    public void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();
        focus = null;
        playerMotor.StopFollowingTarget();
    }
}
