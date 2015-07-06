#region Legal
/*
 * Copyright (c) 2015 The University of Pretoria.
 *
 * The following was designed for the Centre of GeoInformation
 * Science (CGIS), University of Pretoria. All code is property
 * of the University of Pretoria and is available under the 
 * Creative Commons Attribution-ShareAlike (CC BY-SA) see:
 * "https://creativecommons.org/licenses/"
 *
 * Author: Duran Cole
 * Email: u13329414@tuks.co.za
 * Author: Michael Nunes
 * Email: u12104592@tuks.co.za
 * Author: Molefe Molefe
 * Email: u12260429@tuks.co.za
 * Author: Tebogo Christopher Seshibe
 * Email: u13181442@tuks.co.za
 * Author: Timothy Snayers
 * Email: u13397134@tuks.co.za
 */
#endregion

using System;
using OpenTK;

namespace DisplayModel
{
	/// <summary>
	/// Used to define a 3D model's atrtibutes: position, rotation, scale.
	/// </summary>
	public struct Transform : IEquatable<Transform>
	{
		#region Fields
		private Vector3 position;
		private Vector3 rotation;
		private Vector3 scale;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes the transform to the vectors provided.
		/// </summary>
		/// <param name='p'> The position vector. </param>
		/// <param name='r'> The rotation vector. </param>
		/// <param name='s'> The scale vector. </param>
		public Transform(Vector3 p, Vector3 r, Vector3 s)
		{
			position = new Vector3(p.X, p.Y, p.Z);
			rotation = new Vector3(r.X, r.Y, r.Z);
			scale = new Vector3(s.X, s.Y, s.Z);
		}

		/// <summary>
		/// Initializes the transform to the arrays provided.
		/// </summary>
		/// <param name='p'> The position array. </param>
		/// <param name='r'> The rotation array. </param>
		/// <param name='s'> The scale array. </param>
		public Transform(float[] p, float[] r, float[] s)
		{
			position = new Vector3(p[0], p[1], p[2]);
			rotation = new Vector3(r[0], r[1], r[2]);
			scale = new Vector3(s[0], s[1], s[2]);
		}

		/// <summary>
		/// Initializes the transform to the arrays provided.
		/// </summary>
		/// <param name='p'> The position array. </param>
		/// <param name='r'> The rotation array. </param>
		/// <param name='s'> The scale array. </param>
		public Transform(int[] p, int[] r, int[] s)
		{
			position = new Vector3((float) p[0], (float) p[1], (float) p[2]);
			rotation = new Vector3((float) r[0], (float) r[1], (float) r[2]);
			scale = new Vector3((float) s[0], (float) s[1], (float) s[2]);
		}

		/// <summary>
		/// Initializes the transform to the values provided.
		/// </summary>
		/// <param name='px'> The x-value of the position. </param>
		/// <param name='py'> The y-value of the position. </param>
		/// <param name='pz'> The z-value of the position. </param>
		/// <param name='rx'> The x-value of the rotation. </param>
		/// <param name='ry'> The y-value of the rotation. </param>
		/// <param name='rz'> The z-value of the rotation. </param>
		/// <param name='sx'> The x-value of the scale. </param>
		/// <param name='sy'> The y-value of the scale. </param>
		/// <param name='sz'> The z-value of the scale. </param>
		public Transform(float px, float py, float pz, float rx, float ry, float rz, float sx, float sy, float sz)
		{
			position = new Vector3(px, py, pz);
			rotation = new Vector3(rx, ry, rz);
			scale = new Vector3(sx, sy, sz);
		}

		/// <summary>
		/// Initializes the transform to the values provided.
		/// </summary>
		/// <param name='px'> The x-value of the position. </param>
		/// <param name='py'> The y-value of the position. </param>
		/// <param name='pz'> The z-value of the position. </param>
		/// <param name='rx'> The x-value of the rotation. </param>
		/// <param name='ry'> The y-value of the rotation. </param>
		/// <param name='rz'> The z-value of the rotation. </param>
		/// <param name='sx'> The x-value of the scale. </param>
		/// <param name='sy'> The y-value of the scale. </param>
		/// <param name='sz'> The z-value of the scale. </param>
		public Transform(int px, int py, int pz, int rx, int ry, int rz, int sx, int sy, int sz)
		{
			position = new Vector3((float) px, (float) py, (float) pz);
			rotation = new Vector3((float) rx, (float) ry, (float) rz);
			scale = new Vector3((float) sx, (float) sy, (float) sz);
		}
		#endregion

		#region Attributes
		/// <summary>
		/// Get and sets the position.
		/// </summary>
		public Vector3 Position
		{
			get
			{
				return position;
			}
			set
			{
				position = value;
			}
		}

		/// <summary>
		/// Get and sets the rotation.
		/// </summary>
		public Vector3 Rotation
		{
			get
			{
				return rotation;
			}
			set
			{
				rotation = value;
			}
		}

		/// <summary>
		/// Get and sets the scale.
		/// </summary>
		public Vector3 Scale
		{
			get
			{
				return scale;
			}
			set
			{
				scale = value;
			}
		}
		#endregion

		#region Operators
		/// <summary>
		/// Overrides the default Equals function. Checks whether 
		/// the other object is a Transform object, after which,
		/// calls the class specific Equals function.
		/// </summary>
		/// <param name='other'> The object we are comparing this to. </param>
		public override bool Equals(object other)
		{
			if(other is Transform)
				return this.Equals((Transform) other);
			return false;
		}

		/// <summary>
		/// Checks whether this Transform object is the same as
		/// another, by comparing the corresponding vectors.
		/// </summary>
		/// <param name='other'> The object we are comparing this to. </param>
		public bool Equals(Transform other)
		{
			return
				(this.position == other.position) &&
				(this.rotation == other.rotation) &&
				(this.scale == other.scale);
		}

		/// <summary>
		/// Compares whether two Transform objects are equal by calling
		/// the class specific Equals function.
		/// </summary>
		/// <param name='left'> The left operand, the first object. </param>
		/// <param name='right'> The second operand, the second object. </param>
		public static bool operator ==(Transform left, Transform right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// Compares whether two Transform objects are not equal by
		/// calling the class specific Equals function and returns 
		/// the not of the result.
		/// </summary>
		/// <param name='left'> The left operand, the first object. </param>
		/// <param name='right'> The second operand, the second object. </param>
		public static bool operator !=(Transform left, Transform right)
		{
			return !left.Equals(right);
		}
		#endregion
	}
}