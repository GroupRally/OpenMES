<%@ WebHandler Language="C#" Class="FileService" %>

using System;
using System.Web;
using System.Collections.Generic;
using MES.Persistency;

public class FileService : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) 
    {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");

        string dataObjectID = context.Request.QueryString["ObjectID"];

        byte[] fileBytes = null;

        using (DBModelContainer dbContext = new DBModelContainer())
        {
            DBDataItem dataItem = dbContext.DBDataItems.Find(dataObjectID);

            if ((dataItem != null) && (dataItem.DataBytes != null))
            {
                fileBytes = dataItem.DataBytes;
            }
        }

        if (fileBytes != null)
        {
            context.Response.ContentType = "application/x-msdownload";
            
            context.Response.AppendHeader("Content-Disposition", String.Format("attachment;filename={0}", dataObjectID));

            context.Response.OutputStream.Write(fileBytes, 0, fileBytes.Length);

            context.Response.Flush();
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}