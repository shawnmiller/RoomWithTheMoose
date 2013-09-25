/*****************************************************************************
 *                                IMPORTANT
 * Because of the nature of how inheritance works for MonoBehaviours, the base
 * class' void Start() and void Awake() functions are never called. In order 
 * to guarantee their execution, you must manually call it in every class that
 * inherits from them, making them protected in the base class. It is advised
 * to do so within their respected child functions, i.e. calling base.Start()
 * from within the child class' void Start().
 ****************************************************************************/

using UnityEngine;

public class StateComponent : GameComponent
{
  protected GameState _state;

  protected void Start()
  {
    _state = GameState.Get();
  }
}