<%@ Application Language="C#" %>
<%@ Import Namespace="System.ComponentModel.DataAnnotations" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.Web.DynamicData" %>
<%@ Import Namespace="System.Web.UI" %>

<script RunAt="server">
    private static MetaModel s_defaultModel = new MetaModel();
    public static MetaModel DefaultModel {
        get {
            return s_defaultModel;
        }
    }

    public static void RegisterRoutes(RouteCollection routes) {
        //                    IMPORTANT: DATA MODEL REGISTRATION 
        // Uncomment this line to register an ADO.NET Entity Framework model for ASP.NET Dynamic Data.
        // Set ScaffoldAllTables = true only if you are sure that you want all tables in the
        // data model to support a scaffold (i.e. templates) view. To control scaffolding for
        // individual tables, create a partial class for the table and apply the
        // [ScaffoldTable(true)] attribute to the partial class.
        // Note: Make sure that you change "YourDataContextType" to the name of the data context
        // class in your application.
        // See http://go.microsoft.com/fwlink/?LinkId=257395 for more information on how to register Entity Data Model with Dynamic Data            
        // DefaultModel.RegisterContext(() =>
        // {
        //    return ((IObjectContextAdapter)new YourDataContextType()).ObjectContext;
        // }, new ContextConfiguration() { ScaffoldAllTables = false });

        DefaultModel.RegisterContext
        (
            () =>
            {
                return ((System.Data.Entity.Infrastructure.IObjectContextAdapter)(new MES.Persistency.DBModelContainer())).ObjectContext;
            }, 
            new ContextConfiguration() { ScaffoldAllTables = true }
            //new ContextConfiguration() { ScaffoldAllTables = false }
        );
        
        // The following registration should be used if YourDataContextType does not derive from DbContext
        // DefaultModel.RegisterContext(typeof(YourDataContextType), new ContextConfiguration() { ScaffoldAllTables = false });

        // The following statement supports separate-page mode, where the List, Detail, Insert, and 
        // Update tasks are performed by using separate pages. To enable this mode, uncomment the following 
        // route definition, and comment out the route definitions in the combined-page mode section that follows.
        routes.Add(new DynamicDataRoute("{table}/{action}.aspx") {
            //Constraints = new RouteValueDictionary(new { action = "List|Details|Edit|Insert" }),
            Constraints = new RouteValueDictionary(new { action = "List|Details" }),
            Model = DefaultModel
        });

        // The following statements support combined-page mode, where the List, Detail, Insert, and
        // Update tasks are performed by using the same page. To enable this mode, uncomment the
        // following routes and comment out the route definition in the separate-page mode section above.
        //routes.Add(new DynamicDataRoute("{table}/ListDetails.aspx") {
        //    Action = PageAction.List,
        //    ViewName = "ListDetails",
        //    Model = DefaultModel
        //});

        //routes.Add(new DynamicDataRoute("{table}/ListDetails.aspx") {
        //    Action = PageAction.Details,
        //    ViewName = "ListDetails",
        //    Model = DefaultModel
        //});
    }

    private static void RegisterScripts() {
        ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
        {
            Path = "~/Scripts/jquery-1.7.1.min.js",
            DebugPath = "~/Scripts/jquery-1.7.1.js",
            CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.1.min.js",
            CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.1.js",
            CdnSupportsSecureConnection = true,
            LoadSuccessExpression = "window.jQuery"
        });
    }

    private void LoadBarcodePrinterConfigurations() 
    {
        string configFilePath = Server.MapPath("printer-config.xml");

        if (System.IO.File.Exists(configFilePath))
        {
            string configXml = "<BarcodePrintingParameter/>";

            MES.Core.Parameter.BarcodePrintingParameter printingParam = null;
            
            using (System.IO.FileStream stream = new System.IO.FileStream(configFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
                {
                    configXml = reader.ReadToEnd();
                }
            }
            
            printingParam = new MES.Utility.XmlUtility().XmlDeserialize(configXml, typeof(MES.Core.Parameter.BarcodePrintingParameter), new Type[] { typeof(MES.Core.Parameter.BarcodeType) }, "utf-8") as MES.Core.Parameter.BarcodePrintingParameter;

            if (printingParam != null)
            {
                MES.Processor.ModuleConfiguration.Default_BarcodeFontName = printingParam.BarcodeFontName;
                MES.Processor.ModuleConfiguration.Default_BarcodeFontSize = printingParam.BarcodeFontSize;
                MES.Processor.ModuleConfiguration.Default_BarcodeImageHeight = printingParam.BarcodeImageHeight;
                MES.Processor.ModuleConfiguration.Default_BarcodeImageWidth = printingParam.BarcodeImageWidth;
                MES.Processor.ModuleConfiguration.Default_BarcodeType = printingParam.BarcodeType;
                MES.Processor.ModuleConfiguration.Default_BarcodeXPosition = printingParam.BarcodeXPosition;
                MES.Processor.ModuleConfiguration.Default_BarcodeYPosition = printingParam.BarcodeYPosition;
                MES.Processor.ModuleConfiguration.Default_IsPrintingBarcodeCaption = printingParam.IsPrintingCaption;
                MES.Processor.ModuleConfiguration.Default_PrinterName = printingParam.PrinterName;
                MES.Processor.ModuleConfiguration.Default_CaptionFontName = printingParam.CaptionFontName;
                MES.Processor.ModuleConfiguration.Default_CaptionFontSize = printingParam.CaptionFontSize;
                MES.Processor.ModuleConfiguration.Default_CaptionXPosition = printingParam.CaptionXPosition;
                MES.Processor.ModuleConfiguration.Default_CaptionYPosition = printingParam.CaptionYPosition;   
            }
        }
    }
    
    void Application_Start(object sender, EventArgs e) 
    {
        RegisterRoutes(RouteTable.Routes);
        RegisterScripts();

        this.LoadBarcodePrinterConfigurations();

        MES.Utility.TracingUtility.DefaultTraceSourceName = "MESCloudTraceSource";
    }
    
</script>
