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

namespace DisplayModel
{
    /// <summary>
    /// This class represents a model item that is to be displayed to the viewer.
    /// </summary>
	public abstract class Model
	{
		/// <summary>
		/// Handles the start of the eye tracking recording.
		/// </summary>
		protected abstract void startRecording();

		/// <summary>
		/// Handles the halting of the eye tracking recording.
		/// </summary>
		protected abstract void stopRecording();

		/// <summary>
		/// Handles the setup of the file and the interface used to interact with the file.
		/// </summary>
		protected abstract void saveToFile();
	}	
}