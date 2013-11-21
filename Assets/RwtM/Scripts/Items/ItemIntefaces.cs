interface IUseable
{
  void Use();
  void StopUsing();
}

interface IEquippable
{
  void Take();
  void Discard();
  void Destroy();
}

/*********************************************************************
 * The core difference between IInteractible and IEquippable is that
 * an IInteractible Item is one that exists only in the world and can
 * be interacted with without picking them up. A good example of this
 * would be a door which can be opened and closed or a physics object.
 ********************************************************************/
interface IInteractible
{
  void Interact();
  void Cancel();
}