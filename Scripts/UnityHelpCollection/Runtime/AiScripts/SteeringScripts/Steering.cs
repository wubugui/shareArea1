﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SteeringSys
{
    public enum ForceType
    {
        m_null = -1,
        arrive,
        flee,
        pursuit,
        avoid,
        wander,
        alignment,
        cohesion,
        evade
    }


    public abstract class Steering : MonoBehaviour
    {
        public float weight = 1;

        public virtual void Reset()
        {

        }

        public virtual Vector3 Force()
        {
            return new Vector3(0, 0, 0);
        }
    }

    public interface ITargetSteering
    {
        Transform GetTarget();
        void SetTarget(Transform target);
    }

   public interface ITargetsSteering
    {
        IList<Transform> GetTargets();
        int GetCurTargetIdx();
        void SetTargets(IList<Transform> targets);
    }

    public interface IArriveSteering
    {
        UnityEngine.Events.UnityEvent hasArrive();
    }
}


