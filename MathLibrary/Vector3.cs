using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace MathLibrary
{
    public struct Vector3
    {
        public float x, y, z;

        //
        public float Magnitude
        {
            get
            {
                // c = sqrt(x^2 + y^2 + z^2)
                return (float)Math.Abs(Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2)));
            }
        }

        //
        public Vector3 Normalized
        {
            get
            {
                if (Magnitude == 0)
                {
                    return new Vector3();
                }
                return this / Magnitude;
            }
        }

        //
        public Vector3(float x = 0, float y = 0, float z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        //
        public Vector3 Normalize()
        {
            this = Normalized;
            return this;
        }

        //
        public Vector3 CrossProduct(Vector3 other)
        {
            return new Vector3((this.y * other.z) - (this.z * other.y), (this.z * other.x) - (this.x * other.z), (this.x * other.y) - (this.y * other.x));
        }

        //
        public override string ToString()
        {
            // (x, y, z)
            return "(" + x + ", " + y + ", " + z + ")";
        }

        //
        public float DotProduct(Vector3 other)
        {
            return (x * other.x) + (y * other.y) + (z * other.z);
        }

        //
        public float Distance(Vector3 other)
        {
            return (other - this).Magnitude;
        }

        //
        public float Angle(Vector3 other)
        {
            return (float)Math.Acos(other.DotProduct(this));
        }

        

        //
        public static bool operator ==(Vector3 left, Vector3 right)
        {
            return (left.x == right.x) && (left.y == right.y) && (left.z == right.z);
        }

        //
        public static bool operator !=(Vector3 left, Vector3 right)
        {
            return !(left == right);
        }

        //Operator overload for addition
        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(left.x + right.x, left.y + right.y, left.z + right.z);
        }

        //Operator overload for subtraction
        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left.x - right.x, left.y - right.y, left.z - right.z);
        }

        //Operator overload for multiplication by a scaler
        public static Vector3 operator *(Vector3 left, float scaler)
        {
            return new Vector3(left.x * scaler, left.y * scaler, left.z * scaler);
        }

        //Operator overload for multiplication of a float by a vector
        public static Vector3 operator *(float scaler, Vector3 right)
        {
            return new Vector3(right.x * scaler, right.y * scaler, right.z * scaler);
        }

        //Operator overload for division by a scaler
        public static Vector3 operator /(Vector3 left, float scaler)
        {
            return new Vector3(left.x / scaler, left.y / scaler, left.z / scaler);
        }

        //Implicit conversion from System.Numerics.Vector3 to Vector3
        public static implicit operator Vector3(System.Numerics.Vector3 vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }

        //I mplicit conversion from Vector3 to System.Numerics.Vector3
        public static implicit operator System.Numerics.Vector3(Vector3 vector)
        {
            return new System.Numerics.Vector3(vector.x, vector.y, vector.z);
        }
    }
}
