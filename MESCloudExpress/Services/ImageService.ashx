<%@ WebHandler Language="C#" Class="ImageService" %>

using System;
using System.Web;
using System.Collections.Generic;
using MES.Persistency;

public class ImageService : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) 
    {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");

        string dataObjectID = context.Request.QueryString["ObjectID"];

        string imageFormat = "image/jpeg";

        byte[] imageBytes = null;

        //using (System.Data.Objects.ObjectContext objContext = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)(new DBModelContainer())).ObjectContext)
        //{
        //    dataItem = objContext.GetObjectByKey(new System.Data.EntityKey("DBDataItem", "DataItemID", dataObjectID)) as DBDataItem;
        //}

        using (DBModelContainer dbContext = new DBModelContainer())
        {
           DBDataItem dataItem = dbContext.DBDataItems.Find(dataObjectID);

           if ((dataItem != null) && (dataItem.DataBytes != null))
           {
               imageBytes = dataItem.DataBytes;
               
               foreach (var dataParam in dataItem.DataParameters)
               {
                   if ((dataParam.Name.ToLower() == "imageoutputformat") && (!String.IsNullOrEmpty(dataParam.Value)))
                   {
                       imageFormat = String.Format("image/{0}", dataParam.Value);
                   }
               }
           }
        }

        if (imageBytes != null)
        {   
            context.Response.ContentType = imageFormat;

            context.Response.OutputStream.Write(imageBytes, 0, imageBytes.Length);

            context.Response.Flush();
        }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}