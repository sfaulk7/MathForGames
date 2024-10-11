using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLibrary
{
    public class Vector2
    {
        float x, y;

        public Vector2()
        {
            x = 0;
            y = 0;
        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        //Operator overload for addition
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x + right.x, left.y + right.y);
        }

        //Operator overload for subtraction
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x - right.x, left.y - right.y);
        }

        //Operator overload for multipllication by a vector
        public static Vector2 operator *(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x * right.x, left.y * right.y);
        }

        //Operator overload for multipllication by a scaler
        public static Vector2 operator /(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x / right.x, left.y / right.y);
        }
    }
}
