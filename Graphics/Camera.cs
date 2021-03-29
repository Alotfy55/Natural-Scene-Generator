using GlmNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    class Camera
    {
        float mAngleX = 0;
        float mAngleY = 0;
        vec3 mDirection;
        vec3 mPosition;
        vec3 mRight;
        vec3 mUp;
        mat4 mViewMatrix;
        mat4 mProjectionMatrix;
        public vec3 centerPos;

        public Camera()
        {
            Reset(800, 800, 800, 0, 0, 0, 0, -1, 0);

            SetProjectionMatrix(45, 4 / 3, 0.1f, 3000);

        }

        public vec3 GetLookDirection()
        {
            return mDirection;
        }

        public mat4 GetViewMatrix()
        {
            return mViewMatrix;
        }

        public mat4 GetProjectionMatrix()
        {
            return mProjectionMatrix;
        }

        public void Reset(float eyeX, float eyeY, float eyeZ, float centerX, float centerY, float centerZ, float upX, float upY, float upZ)
        {
            vec3 eyePos = new vec3(eyeX, eyeY, eyeZ);
            centerPos = new vec3(centerX, centerY, centerZ);
            vec3 upVec = new vec3(upX, upY, upZ);

            mPosition = eyePos;
            mDirection = centerPos - mPosition;
            mRight = glm.cross(mDirection, upVec);
            mUp = upVec;
            mUp = glm.normalize(mUp);
            mRight = glm.normalize(mRight);
            mDirection = glm.normalize(mDirection);

            mViewMatrix = glm.lookAt(mPosition, centerPos, mUp);
        }

        public void UpdateViewMatrix()
        {
            mDirection = new vec3((float)(-Math.Cos(mAngleY) * Math.Sin(mAngleX))
                , (float)(Math.Sin(mAngleY))
                , (float)(-Math.Cos(mAngleY) * Math.Cos(mAngleX)));
            mRight = glm.cross(mDirection, new vec3(0, 1, 0));
            mUp = glm.cross(mRight, mDirection);

            vec3 center = mPosition + mDirection;

            mViewMatrix = glm.lookAt(mPosition, center, mUp);
        }
        public void SetProjectionMatrix(float FOV, float aspectRatio, float near, float far)
        {
            mProjectionMatrix = glm.perspective(FOV, aspectRatio, near, far);
        }


        public void Yaw(float angleDegrees)
        {
            mAngleX += angleDegrees;
        }

        public void Pitch(float angleDegrees)
        {
            mAngleY += angleDegrees;
        }

        public void Walk(float dist, float height)
        {
            mPosition += dist * mDirection;
           //setHeight(height);
        }
        public void Strafe(float dist, float height)
        {
            mPosition += dist * mRight;
            //setHeight(height);
        }
        public void setHeight(float height)
        {
            
            mPosition.y = height;
        }
        public void Fly(float dist)
        {
            mPosition += dist * mUp;
        }
        public vec3 Get_mPosition() 
        {
            return mPosition;
        }
        public void set_mPosition(float x, float y, float z) 
        {

            mPosition.x = x;
            mPosition.y = y;
            mPosition.z = z;
        }
    }
}
