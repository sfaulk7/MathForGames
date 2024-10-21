using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MathLibrary
{
    public struct Matrix3
    {
        //3x3 Matrix
        public float
            m00, m01, m02,
            m10, m11, m12,
            m20, m21, m22;

        public Matrix3(
            float m00, float m01, float m02,
            float m10, float m11, float m12,
            float m20, float m21, float m22)
        {
            this.m00 = m00;
            this.m01 = m01;
            this.m02 = m02;
            this.m10 = m10;
            this.m11 = m11;
            this.m12 = m12;
            this.m20 = m20;
            this.m21 = m21;
            this.m22 = m22;
        }

        public static Matrix3 Identity
        {
            get
            {
                //Return Identity Matrix
                return new Matrix3(
                    1, 0, 0,
                    0, 1, 0,
                    0, 0, 1);
            }
        }

        //Print Matrix3
        public static string PrintMatrix3(Matrix3 a)
        {
            return
                a.m00 + "," + a.m01 + "," + a.m02 + "\n" +
                a.m10 + "," + a.m11 + "," + a.m12 + "\n" +
                a.m20 + "," + a.m21 + "," + a.m22 + "\n";
        }


        //Matrix3 Addition overload
        public static Matrix3 operator +(Matrix3 a, Matrix3 b)
        {
            return new Matrix3(
                a.m00 + b.m00, a.m01 + b.m01, a.m02 + b.m02,
                a.m10 + b.m10, a.m11 + b.m11, a.m12 + b.m12,
                a.m20 + b.m20, a.m21 + b.m21, a.m22 + b.m22);
        }
        //Matrix3 Subtraction overload
        public static Matrix3 operator -(Matrix3 a, Matrix3 b)
        {
            return new Matrix3(
                a.m00 - b.m00, a.m01 - b.m01, a.m02 - b.m02,
                a.m10 - b.m10, a.m11 - b.m11, a.m12 - b.m12,
                a.m20 - b.m20, a.m21 - b.m21, a.m22 - b.m22);
        }
        //Matrix3 Multiplication overload
        public static Matrix3 operator *(Matrix3 a, Matrix3 b)
        {
            return new Matrix3(
                    a.m00 * b.m00 + a.m01 * b.m10 + a.m02 * b.m20,
                    a.m10 * b.m00 + a.m11 * b.m10 + a.m12 * b.m20,
                    a.m20 * b.m00 + a.m21 * b.m10 + a.m22 * b.m20,

                    a.m00 * b.m01 + a.m01 * b.m11 + a.m02 * b.m21,
                    a.m10 * b.m01 + a.m11 * b.m11 + a.m12 * b.m21,
                    a.m20 * b.m01 + a.m21 * b.m11 + a.m22 * b.m21,

                    a.m00 * b.m02 + a.m01 * b.m12 + a.m02 * b.m22,
                    a.m10 * b.m02 + a.m11 * b.m12 + a.m12 * b.m22,
                    a.m20 * b.m02 + a.m21 * b.m12 + a.m22 * b.m22);
        }
    }
}
