/**  © 2014, Olga K. All Rights Reserved.
  *
  *  This file is part of the ASPUtilities Library.
  *  ASPUtilities is free software: you can redistribute it and/or modify
  *  it under the terms of the GNU General Public License as published by
  *  the Free Software Foundation, either version 3 of the License, or
  *  (at your option) any later version.
  *
  *  ASPUtilities is distributed in the hope that it will be useful,
  *  but WITHOUT ANY WARRANTY; without even the implied warranty of
  *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  *  GNU General Public License for more details.
  *
  *  You should have received a copy of the GNU General Public License
  *  along with ASPUtilities.  If not, see <http://www.gnu.org/licenses/>.
  */

<%@ WebHandler Language="C#" Class="ImageHandler" %>

using System;
using System.Web;

public class ImageHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context)
    {
        string Info = context.Request.QueryString["ID"];
		string StoredProcedure = context.Request.QueryString["SProc"];
		string StoredParameter = context.Request.QueryString["SParam"];
		
		if (string.IsNullOrEmpty(Info))
		{
			Info = context.Request.QueryString["Name"];
		}
		
        if (!string.IsNullOrEmpty(Info) && !string.IsNullOrEmpty(StoredProcedure) && !string.IsNullOrEmpty(StoredParameter))
        {
            string ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            System.Data.SqlClient.SqlConnection Connect = new System.Data.SqlClient.SqlConnection(ConnectionString);
            System.Data.SqlClient.SqlCommand Command = new System.Data.SqlClient.SqlCommand(StoredProcedure, Connect);
            Command.CommandType = System.Data.CommandType.StoredProcedure;
            Command.Parameters.AddWithValue(StoredParameter, Info);

            byte[] ImageBuffer = null;
            try
            {
                Connect.Open();
                ImageBuffer = (byte[])Command.ExecuteScalar();
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
			finally
			{
				Connect.Close();
			}

            context.Response.ContentType = "Image/Png";
            context.Response.OutputStream.Write(ImageBuffer, 0, ImageBuffer.Length);
        }
    }
 
    public bool IsReusable {
        get {
            return true;
        }
    }

}