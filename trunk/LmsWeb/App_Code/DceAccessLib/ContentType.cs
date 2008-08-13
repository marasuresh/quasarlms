using System;
using System.Data;
using System.Data.SqlClient;

namespace DCEAccessLib
{
    public enum ContentType
    {
        /// <summary>
        /// Plain text nvarchar255
        /// </summary>
        _string = 1,
        /// <summary>
        /// Xml content max length 4000, saved in TData
        /// </summary>
        _xml = 2,
        /// <summary>
        /// Ambigious object nvarchar255
        /// </summary>
        _object = 3,
        /// <summary>
        /// image data
        /// </summary>
        _image = 4,        // binary image 
        /// <summary>
        /// Url locator nvarchar255
        /// </summary>
        _url = 5,
        /// <summary>
        /// html content max length 4000, saved in TData
        /// </summary>
        _html = 6,
        /// <summary>
        /// Email template (DataStr - template name,  TData - template xsl
        /// </summary>
        _emailTemplate = 7,
        /// <summary>
        /// Long string max length 4000, saved in TData
        /// </summary>
        _longString = 8
    }
}