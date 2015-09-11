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
        public float currentSpeed;
        public float yaw;
        public float pitch;

        public const float walkSpeed = 1.0f;
        public const float runSpeed = 5.0f;

        public Camera()
        {
            Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);
            float currentSpeed = 0.0f;
            float yaw = (float)MathHelper.DegreesToRadians(-90);
            float pitch = 0.0f;
        }
    }
}
