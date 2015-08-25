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
            position = p;
            rotation = r;
            scale = s;
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
		#endregion

		#region Attributes
		/// <summary>
		/// The model's position in space.
		/// </summary>
        public Vector3 Position { get { return position; } }

		/// <summary>
		/// The object's rotation in space.
		/// </summary>
        public Vector3 Rotation { get { return rotation; } }

		/// <summary>
		/// The object's scale.
		/// </summary>
        public Vector3 Scale { get { return scale; } }
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
				(this.Position == other.Position) &&
				(this.Rotation == other.Rotation) &&
				(this.Scale == other.Scale);
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

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
		#endregion
	}
}