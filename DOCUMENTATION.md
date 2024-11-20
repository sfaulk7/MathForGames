# MATH FOR GAMES DOCUMENTATION

#### FAST PASSES

###### Matrices
- [Matrix 3](#Matrix-3)
- [Matrix 4](#Matrix-4)

###### Vectors
- [Vector 2](#Vector-2)
- [Vector 3](#Vector-3)
- [Vector 4](#Vector-4)

---

# Matrix 3

#### Summary

```cpp
Matrix 3 is a 3x3 Matrix consisting of 9 numbers
it is mainly used to easily alter a vector 3 through
translation, rotation, and scale
or the mathimatical operations
addition, subtraction, and multiplication.

It does so by allowing storage of
rotation, scale, and translation of a game object

It helps greatly with the implementation of parent child relationships

```

#### List of members and functions
- Matrix3 Identity
    - Returns a 3x3 Identity Matrix
    - _1, 0, 0,_
      
      _0, 1, 0,_
      
      _0, 0, 1_
- Matrix3 CreateTranslation
    - Takes float x, float y to translate by
    - returns Translation Matrix
    - _1, 0, x,_
      
      _0, 1, y,_
      
      _0, 0, 1_
- Matrix3 CreateRotation
    - Takes float radians to rotate by
    - returns Roation Matrix
    - _CoSine radians, (sine radians) * -1, 0_

      _Sine radians, CoSine radians, 0_

      _0, 0, 1_
- Matrix3 CreateScale
    - Takes float x, float y to Scale by
    - returns Scale Matrix
    - _x, 0, 0,_
      
      _0, y, 0,_
      
      _0, 0, 1_
- Matrix3 ToString
    - Allows Matrix3 to be put into a writeable format
    - Returns:
    - _m00, m01, m02,_

      _m10, m11, m12,_

      _m20, m21, m22_

---

# Matrix 4

#### Summary

```cpp
Matrix 4 is a 4x4 Matrix consisting of 16 numbers
it is mainly used to easily alter a vector 4 through
translation, rotation, and scale
or the mathimatical operations
addition, subtraction, and multiplication.

It does so by allowing storage of
rotation, scale, and translation of a game object

It helps greatly with the implementation of parent child relationships

```

#### List of members and functions
- Matrix4 Identity
    - Returns a 4x4 Identity Matrix
    - _1, 0, 0, 0,_
      
      _0, 1, 0, 0,_
      
      _0, 0, 1, 0,_

      _0, 0, 0, 1_
- Matrix4 CreateTranslation
    - Takes float x, float y to translate by
    - returns Translation Matrix
    - _1, 0, 0, x,_
      
      _0, 1, 0, y,_
      
      _0, 0, 1, z,_

      _0, 0, 0, 1_
- Matrix4 CreateRotationX
    - Takes float radians to create a X rotation
    - returns Roation Matrix
    - _1, 0, 0, 0,_
      
      _0, CoSine radians, Sine radians, 0,_
      
      _0, (sine radians) * -1, CoSine radians, 0,_

      _0, 0, 0, 1_

- Matrix4 CreateRotationY
    - Takes float radians to create a Y rotation
    - returns Roation Matrix
    - _CoSine radians, 0, Sine radians, 0,_

      _0, 1, 0, 0,_

      _Sine radians * -1, 0, CoSine radians, 0,_

      _0, 0, 0, 1);_

- Matrix4 CreateRotationZ
    - Takes float radians to create a Z rotation
    - returns Roation Matrix
    - _CoSine radians, (Sine radians) * -1, 0, 0,_
      
      _Sine radians, CoSine radians, 0, 0,_
      
      _0, 0, 1, 0,_

      _0, 0, 0, 1_
- Matrix4 CreateScale
    - Takes float x, float y, and float z to Scale by
    - returns Scale Matrix
    - _x, 0, 0, 0,_
      
      _0, y, 0, 0,_
      
      _0, 0, z, 0_

      _0, 0, 0, 1_
- Matrix4 ToString
    - Allows the Matrix4 to be put into a writeable format
    - Returns:
    - _m00, m01, m02, m03,_

      _m10, m11, m12, m13,_

      _m20, m21, m22, m23,_

      _m30, m31, m32, m33,_


---

# Vector 2

#### Summary

```cpp
Vector 2 is a 2D direction from an origin to
the point (x, y) that is in the Vector

There are all mathimatical operations for Vector2
such as Addition, Subtraction, Multiplication, and Division
```

#### List of members and functions
- Vector2 Magnitude
    - Returns the Vectors magnitude as a float
    - _Absolute Value of the Square Root of (x^2 + y^2)_
- Vector2 Normalized
    - Returns a Normalized version of the Vector2 giving direction but no magnitude
    - _this / Magnitude_
- Vector2 Normalize
    - Normalized the vector2
    - _this = Normalized_
- Vector2 ToString
    - Allows the Vector2 to be put into a writeable format
    - _(x, y)_
- Vector2 DotProduct
    - Takes a Vector2 and returns the dot product of the two Vector2s
    - _(x * other.x) + (y * other.y)_
- Vector2 Distance
    - Takes a Vector2 and returns the distance between the two Vector2s
    - _(other - this).Magnitude_
- Vector2 Angle
    - Takes a Vector2 and returns the Angle between the two Vector2s
    - _ArcCoSine (other.DotProduct (this) )_
- System.Numerics.Vector2 to Vector2
    - Takes a System.Numerics.Vector2 and converts it into a Vector2
    - _returns new Vector2 ( System.Numerics.Vector2.x, System.Numerics.Vector2.y)_
- Vector2 to System.Numerics.Vector2
    - Takes a Vector2 and converts it into a System.Numerics.Vector2
    - _returns new System.Numerics.Vector2 ( Vector2.x, Vector2.y)_

---

# Vector 3

#### Summary

```cpp
Vector 3 is a 3D direction from an origin to
the point (x, y, z) that is in the Vector

There are all mathimatical operations for Vector3
such as Addition, Subtraction, Multiplication, and Division
```

#### List of members and functions
- Vector3 Magnitude
    - Returns the Vectors magnitude as a float
    - _Absolute Value of the Square Root of (x^2 + y^2 + z^2)_
- Vector3 Normalized
    - Returns a Normalized version of the Vector3 giving direction but no magnitude
    - _this / Magnitude_
- Vector3 Normalize
    - Normalized the vector3
    - _this = Normalized_
- Vector3 CrossProduct
    - Takes a Vector3 and returns the cross product of the two Vector3s
    - return new Vector3
    
      _((this.y * other.z) - (this.z * other.y),_

      _(this.z * other.x) - (this.x * other.z),_

      _(this.x * other.y) - (this.y * other.x));_
- Vector3 ToString
    - Allows the Vector3 to be put into a writeable format
    - _(x, y, z)_
- Vector3 DotProduct
    - Takes a Vector3 and returns the dot product of the two Vector3s
    - _(x * other.x) + (y * other.y) + (z * other.z)_
- Vector3 Distance
    - Takes a Vector3 and returns the distance between the two Vector3s
    - _(other - this).Magnitude_
- Vector3 Angle
    - Takes a Vector3 and returns the Angle between the two Vector3s
    - _ArcCoSine (other.DotProduct (this) )_
- System.Numerics.Vector3 to Vector3
    - Takes a System.Numerics.Vector3 and converts it into a Vector3
    - _returns new Vector3 ( System.Numerics.Vector3.x, System.Numerics.Vector3.y,_ _System.Numerics.Vector3.z)_
- Vector3 to System.Numerics.Vector3
    - Takes a Vector3 and converts it into a System.Numerics.Vector3
    - _returns new System.Numerics.Vector3 ( Vector3.x, Vector3.y, Vector3.z)_

---

# Vector 4

#### Summary

```cpp
Vector 4 is a 4D direction from an origin to
the point (x, y, z, w) that is in the Vector

There are all mathimatical operations for Vector4
such as Addition, Subtraction, Multiplication, and Division
```

#### List of members and functions
- Vector4 Magnitude
    - Returns the Vectors magnitude as a float
    - _Absolute Value of the Square Root of (x^2 + y^2 + z^2 + w^2)_
- Vector4 Normalized
    - Returns a Normalized version of the Vector4 giving direction but no magnitude
    - _this / Magnitude_
- Vector4 Normalize
    - Normalized the vector4
    - _this = Normalized_
- Vector4 CrossProduct
    - Takes a Vector4 and returns the cross product of the two Vector4s
    - return new Vector4
    
      _(this.y * other.z) - (this.z * other.y),_

      _(this.z * other.x) - (this.x * other.z),_

      _(this.x * other.y) - (this.y * other.x),_

      _(this.w = 0));_
- Vector4 ToString
    - Allows the Vector4 to be put into a writeable format
    - _(x, y, z, w)_
- Vector4 DotProduct
    - Takes a Vector4 and returns the dot product of the two Vector4s
    - _(this.y
- Vector4 Distance
    - Takes a Vector4 and returns the distance between the two Vector4s
    - _(other - this).Magnitude_
- Vector4 Angle
    - Takes a Vector4 and returns the Angle between the two Vector4s
    - _ArcCoSine (other.DotProduct (this) )_
- System.Numerics.Vector4 to Vector4
    - Takes a System.Numerics.Vector4 and converts it into a Vector4
    - _returns new Vector4 ( System.Numerics.Vector4.x, System.Numerics.Vector4.y,_ _System.Numerics.Vector4.z, System.Numerics.Vector4.w)_
- Vector4 to System.Numerics.Vector4
    - Takes a Vector4 and converts it into a System.Numerics.Vector4
    - _returns new System.Numerics.Vector3 ( Vector4.x, Vector4.y, Vector4.z, Vector4.w)_
