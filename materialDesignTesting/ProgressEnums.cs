﻿
namespace materialDesignTesting
{
  
    /// <summary>
    /// An Enum type that is used to track the progress within the wizard,
    ///used to assist in the implemenation of the Expanders 
    /// </summary>
    /// <returns>
    /// int value of blank,loaded and manual
    /// </returns>


    public enum progress:int
    {
        blank   = 0,
        settings = 1,
        user = 2,
        opponent = 3
    };
}
