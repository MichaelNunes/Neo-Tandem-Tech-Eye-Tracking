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
using DisplayModel;

namespace DisplayModel
{
	/// <summary>
	/// Used to define a frame where a change occurs in the 3D model's transform attributes.
	/// </summary>
	public class Keyframe
	{
		#region Fields
		private uint frame;
		private Transform transform;
		#endregion

		#region Constructors
		/// <summary>
		/// Creates an empty keyframe.
		/// </summary>
		public Keyframe()
		{

		}

		/// <summary>
		/// Creates a keyframe.
		/// </summary>
		/// <param name='_frame'> The frame where the change will occur. </param>
		/// <param name='_transform'> The new transform values the 3D model will change to. </param>
		public Keyframe(uint _frame, Transform _transform)
		{
			frame = _frame;
			transform = _transform;
		}
		#endregion

		#region Attributes
		/// <summary>
		/// 
		/// </summary>
		public uint Frame
		{
			get
			{
				return frame;
			}
			set
			{
				frame = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public Transform Transform
		{
			get
			{
				return transform;
			}
			set
			{
				transform = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool Valid
		{
			get
			{
				return frame != null && transform != null;
			}
		}
		#endregion
	}
}