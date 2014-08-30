// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Defines a set of graphic capabilities.
    /// </summary>
	public enum GraphicsProfile
	{
        /// <summary>
        /// Use a limited set of graphic features and capabilities, allowing the game to support the widest variety of devices.
        /// </summary>
        //Reach,
        /// <summary>
        /// Use the largest available set of graphic features and capabilities to target devices, that have more enhanced graphic capabilities.        
        /// </summary>
        //HiDef

        // Reach requires 9.1 hardware. It is equal to FeatureLevel 9.1 
        // HiDef requires 10.0 hardware but has limitations enforced by DirectX9.3 API.
        // This is why we have to define a seperate Profile_10_0 and set HiDef as 0x0904.
        Reach        =  0x0901,
        Profile_09_1 =  0x0901,
        Profile_09_2 =  0x0902,
        Profile_09_3 =  0x0903,
        HiDef        =  0x0904,
        Profile_10_0 =  0x1000,
        Profile_10_1 =  0x1001,      
        Profile_11_0 =  0x1100,
        Profile_11_1 =  0x1101,
        Profile_11_2 =  0x1102,
        Profile_12_0 =  0x1200,
	}
}
