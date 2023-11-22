using UnityEngine;

public class simple_shooting : MonoBehaviour
{
   static public void simple_linear_shoot(GameObject _bullet, Vector2 _bullet_position, Vector2 _target_direction)
   {
       GameObject bullet_instance;
       bullet_instance = Instantiate(_bullet, _bullet_position, Quaternion.identity);
       bullet_instance.gameObject.GetComponent<bullet_controller>().InitiateMovement(_target_direction);
   }
}