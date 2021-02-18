﻿using Gisha.SmarterTogether.Body.Robot;
using UnityEngine;

namespace Gisha.SmarterTogether.Body
{
    public class BodyManager : MonoBehaviour
    {
        private void Update()
        {
            if (BodySwapper.CurrentBody != null && BodySwapper.CurrentBody.GetType().Equals(typeof(RobotController)))
            {
                if (Input.GetKeyDown(KeyCode.R))
                    BodySwapper.ReturnToDrone();
            }
        }
    }
}