using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisplayModel
{
    public class Camera
    {
        public Vector3 position;
        
        public float yaw;
        public float pitch;
        
        public float currentSpeed;
        public float walkSpeed;
        public float runSpeed;
        public float flySpeed;

        public Camera()
        {
            Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);
            
            yaw = (float)MathHelper.DegreesToRadians(-90);
            pitch = 0.0f;

            currentSpeed = 0.0f;
            walkSpeed = 1.0f;
            runSpeed = 10.0f;
            flySpeed = 50.0f;
        }
    }
}
