using System;
using System.Collections;
using System.Collections.Generic;
using Abstract;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   public float AutoDestroyTime = 5f;
   public float MoveSpeed = 2f;
   public int Damage = 25;
   public Rigidbody BulletRigidbody;

   private const string DisableMethodName = "Disable";

   private void Awake()
   {
      BulletRigidbody = GetComponent<Rigidbody>();
   }

   private void OnEnable()
   {
      CancelInvoke(DisableMethodName);
      Invoke(DisableMethodName,AutoDestroyTime);
   }

   private void OnTriggerEnter(Collider other)
   {
      IDamageable damageable;

      if (other.TryGetComponent<IDamageable>(out damageable))
      {
         damageable.TakeDamage(Damage);
      }

      Disable();
   }

   private void Disable()
   {
      CancelInvoke(DisableMethodName);
      BulletRigidbody.velocity = Vector3.zero;
      gameObject.SetActive(false);
      
   }
}
