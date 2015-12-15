using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Text;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ResourceIntegrator.Helpers.Tracing;

namespace ResourceIntegrator
{
    public class ResourceRouter : IResourceRouter
    {
        static bool IsServicePointManagerParamsSet = false;

        public ResourceRouter() 
        {
            if (!ResourceRouter.IsServicePointManagerParamsSet)
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ResourceRouter.remoteCertificateValidationCallback);
                System.Net.ServicePointManager.Expect100Continue = false;

                ResourceRouter.IsServicePointManagerParamsSet = true;
            }
        }

        public ResourceRouter(bool enableTracing, string traceSourceName) 
        {
            this.EnableTracing = enableTracing;
            this.TraceSourceName = traceSourceName;

            if (!ResourceRouter.IsServicePointManagerParamsSet)
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ResourceRouter.remoteCertificateValidationCallback);
                System.Net.ServicePointManager.Expect100Continue = false;

                ResourceRouter.IsServicePointManagerParamsSet = true;
            }
        }

        public bool EnableTracing { get; set; }

        public string TraceSourceName { get; set; }

        public string[] TrustedServerCertificateSNs { get; set; }

        private static bool remoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain certificateChain, System.Net.Security.SslPolicyErrors sslPolicyErrors) 
        {
            return true;
        }

        private X509Certificate getCertificate(string certificatePath, string password)
        {
            X509Certificate returnValue = new X509Certificate();

            returnValue.Import(certificatePath, password, X509KeyStorageFlags.DefaultKeySet);

            if (this.EnableTracing)
            {
                TracingHelper.Trace(new object[] { "Certificate information:", certificatePath, returnValue.Issuer, returnValue.Subject, returnValue.GetSerialNumberString(), returnValue.GetRawCertDataString(), returnValue.GetPublicKeyString() }, this.TraceSourceName);
            }      

            return returnValue;
        }

        private X509Certificate getCertificate(byte[] certificateData, string password)
        {
            X509Certificate returnValue = new X509Certificate();

            returnValue.Import(certificateData, password, X509KeyStorageFlags.DefaultKeySet);

            if (this.EnableTracing)
            {
                TracingHelper.Trace(new object[] { "Certificate information:", certificateData, returnValue.Issuer, returnValue.Subject, returnValue.GetSerialNumberString(), returnValue.GetRawCertDataString(), returnValue.GetPublicKeyString() }, this.TraceSourceName);
            }
     
            return returnValue;
        }

        private string createSyncRequest(string uri, string message, string method, Authentication authentication, Dictionary<string, string> headers)
        {
            string returnValue = String.Empty;

            #region Reads configured values from configuration file
            ////Server address
            //string servicePoint = ConfigurationManager.AppSettings.Get("ServicePoint");
            ////User name
            //string userName = ConfigurationManager.AppSettings.Get("ServiceUserName");
            //// Password
            //string password = ConfigurationManager.AppSettings.Get("ServicePassword");
            ////X.509 certificate file location
            //string certificateLocation = ConfigurationManager.AppSettings.Get("CertificateLocation");
            ////X.509 certificate password
            //string certificatePassword = ConfigurationManager.AppSettings.Get("CertificatePassword");

            #endregion

            #region Prepares request uri

            //if (servicePoint.EndsWith("/"))
            //{
            //    servicePoint = servicePoint.Substring(0, (servicePoint.Length - 1));
            //}

            //if ((!uri.StartsWith(servicePoint)) && (!uri.StartsWith("/")))
            //{
            //    uri = "/" + uri;
            //}

            //if (!uri.StartsWith(servicePoint))
            //{
            //    uri = servicePoint + uri;
            //}

            Uri uriAddress = new Uri(uri);

            #endregion

            #region Prepares request

            HttpWebRequest request = WebRequest.Create(uriAddress) as HttpWebRequest;
            request.Method = method;//Sets http request method(GET or POST)
            request.Accept = "application/xml";
            request.ContentType = "application/xml;charset=utf-8";
            request.CookieContainer = new CookieContainer();

            switch (authentication.Type)
            {
                case AuthenticationType.PlainText:
                    //Attaches User name and password
                    string servicePoint = request.Address.Host;
                    request.Credentials = new NetworkCredential(authentication.Identifier, authentication.Password, servicePoint);
                    break;
                case AuthenticationType.X509Certificate:
                    //Attaches X.509 certificate
                    request.ClientCertificates.Add(this.getCertificate(authentication.Identifier, authentication.Password));
                    break;
                case AuthenticationType.Kerberos:
                    break;
                case AuthenticationType.NTLM:
                    break;
                case AuthenticationType.Negociate:
                    break;
                default:
                    break;
            }

            ////Attaches X.509 certificate
            //request.ClientCertificates.Add(this.getCertificate(certificateLocation, certificatePassword));
            ////Attaches User name and password
            //request.Credentials = new NetworkCredential(userName, password, servicePoint);

            if ((headers !=null) && (headers.Count > 0))
            {
                foreach (string key in headers.Keys)
                {
                    request.Headers.Add(key, headers[key]);
                }
            }

            Stream stream = null;

            //Writes XML message to request stream
            if (!String.IsNullOrEmpty(message))
            {
                stream = request.GetRequestStream();

                StreamWriter writer = new StreamWriter(stream);

                writer.Write(message);

                writer.Flush();
                writer.Close();
            }

            #endregion

            #region Processes response

            WebResponse response = null;

            try
            {
                response = request.GetResponse();

                //this.trace(new object[] { "Request information:", request.Headers, request.UserAgent, request.RequestUri, message, request.Method, request.Host, request.Date });

                if (this.EnableTracing)
                {
                    TracingHelper.Trace(new object[] { "Request information:", request.Headers, request.UserAgent, request.RequestUri, message, request.Method, request.Host, request.Date }, this.TraceSourceName);
                }
            }
            catch (Exception ex)
            {
                //this.trace(new object[] { "Exception information", ex.ToString(), message, uri, method });

                if (this.EnableTracing)
                {
                    TracingHelper.Trace(new object[] { "Exception information:", ex.ToString(), message, uri, method }, this.TraceSourceName);
                }

                return ex.ToString();
            }

            if (stream != null)
            {
                stream.Close();
            }

            //Extracts response XML message from response stream
            if (response != null)
            {
                stream = response.GetResponseStream();

                if (stream != null)
                {
                    StreamReader reader = new StreamReader(stream);

                    returnValue = reader.ReadToEnd();
                }

                //this.trace(new object[] { "Response information:", response.Headers, response.ResponseUri, returnValue });

                if (this.EnableTracing)
                {
                    TracingHelper.Trace(new object[] { "Response information:", response.Headers, response.ResponseUri, returnValue }, this.TraceSourceName);
                }
            }

            #endregion

            return returnValue;
        }

        private void createAsyncRequest(string uri, byte[] data, string method, Dictionary<string, string> headers)
        {
            #region Prepares request uri
            Uri uriAddress = new Uri(uri);
            #endregion

            #region Prepares request

            HttpWebRequest request = WebRequest.Create(uriAddress) as HttpWebRequest;
            request.Method = method;//Sets http request method(GET or POST)

            if (method.ToLower() == "post")
            {
                request.Accept = "application/xml";
                request.ContentType = "application/xml;charset=utf-8";
            }

            request.CookieContainer = new CookieContainer();

            //Writes bytes to request stream
            if ((method.ToLower() == "post") && (data != null) && (data.Length > 0))
            {
                request.BeginGetRequestStream(new AsyncCallback(this.getRequestStreamAsyncCallback), new object[] { request, data});
            }
            else if (method.ToLower() == "get")
            {
                request.BeginGetResponse(new AsyncCallback(this.getResponseAsyncCallback), new object[] { request});
            }

            Task.WaitAll();

            #endregion
        }

        private void createAsyncRequest(string uri, byte[] data, string method, Authentication authentication, Dictionary<string, string> headers)
        {
            //#region Reads configured values from configuration file
            ////Server address
            //string servicePoint = ConfigurationManager.AppSettings.Get("ServicePoint");

            //#endregion

            //#region Prepares request uri

            //if (servicePoint.EndsWith("/"))
            //{
            //    servicePoint = servicePoint.Substring(0, (servicePoint.Length - 1));
            //}

            //if ((!uri.StartsWith(servicePoint)) && (!uri.StartsWith("/")))
            //{
            //    uri = "/" + uri;
            //}

            //if (!uri.StartsWith(servicePoint))
            //{
            //    uri = servicePoint + uri;
            //}

            Uri uriAddress = new Uri(uri);

            //#endregion

            #region Prepares request

            HttpWebRequest request = WebRequest.Create(uriAddress) as HttpWebRequest;
            request.Method = method;//Sets http request method(GET or POST)

            if (method.ToLower() == "post")
            {
                request.Accept = "application/xml";
                request.ContentType = "application/xml;charset=utf-8";
            }

            request.CookieContainer = new CookieContainer();

            switch (authentication.Type)
            {
                case AuthenticationType.PlainText:
                    {
                        //Attaches User name and password
                        string servicePoint = request.Address.Host;
                        request.Credentials = new NetworkCredential(authentication.Identifier, authentication.Password, servicePoint);
                        break;
                    }
                case AuthenticationType.X509Certificate:
                    {
                        //Attaches X.509 certificate
                        request.ClientCertificates.Add(this.getCertificate(authentication.Identifier, authentication.Password));
                        break;
                    }
                case AuthenticationType.NTLM:
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            if ((headers != null) && (headers.Count > 0))
            {
                foreach (string key in headers.Keys)
                {
                    request.Headers.Add(key, headers[key]);
                }
            }

            //Writes bytes to request stream
            if ((method.ToLower() == "post") && (data != null) && (data.Length > 0))
            {
                request.BeginGetRequestStream(new AsyncCallback(this.getRequestStreamAsyncCallback), new object[] { request, data});
            }
            else if (method.ToLower() == "get")
            {

            }

            Task.WaitAll();

            #endregion
        }

        private void getRequestStreamAsyncCallback(IAsyncResult result)
        {
            object[] states = result.AsyncState as object[];

            if ((states != null) && (states.Length > 0))
            {
                HttpWebRequest request = states[0] as HttpWebRequest;
                byte[] data = states[1] as byte[];
                //bool enableTracing = (bool)(states[2]);

                Stream stream = request.EndGetRequestStream(result);

                if ((data != null) && (data.Length > 0))
                {
                    stream.Write(data, 0, data.Length);
                    stream.Flush();
                }

                stream.Close();

                try
                {
                    request.BeginGetResponse(new AsyncCallback(this.getResponseAsyncCallback), new object[] { request});

                    if (this.EnableTracing)
                    {
                        TracingHelper.Trace(new object[] { "Request information:", request.Headers, request.UserAgent, request.RequestUri, data, request.Method }, this.TraceSourceName);
                    }
                }
                catch (Exception ex)
                {
                    if (this.EnableTracing)
                    {
                        TracingHelper.Trace(new object[] { "Exception information", ex.ToString(), request.Headers, request.UserAgent, request.RequestUri, data, request.Method }, this.TraceSourceName);
                    }
                }
            }
        }

        private void getResponseAsyncCallback(IAsyncResult result)
        {
            object[] states = result.AsyncState as object[];

            AsyncCompletedEventArgs args = new AsyncCompletedEventArgs(null, ((result.IsCompleted) && (result.CompletedSynchronously)), states);

            if ((states != null) && (states.Length > 0))
            {
                HttpWebRequest request = states[0] as HttpWebRequest;
                //bool enableTracing = (bool)(states[1]);

                #region Processes response

                WebResponse response = null;

                string returnValue = null;

                try
                {
                    response = request.EndGetResponse(result);

                    if (this.EnableTracing)
                    {
                        TracingHelper.Trace(new object[] { "Request information:", request.Headers, request.UserAgent, request.RequestUri, request.Method }, this.TraceSourceName);
                    }
                }
                catch (Exception ex)
                {
                    if (this.EnableTracing)
                    {
                        TracingHelper.Trace(new object[] { "Exception information", ex.ToString(), request.Headers, request.UserAgent, request.RequestUri, request.Method }, this.TraceSourceName);
                    }

                    args = new AsyncCompletedEventArgs(ex, true, states);
                }

                //Extracts response XML message from response stream
                if (response != null)
                {
                    Stream stream = response.GetResponseStream();

                    if (stream != null)
                    {
                        StreamReader reader = new StreamReader(stream);

                        returnValue = reader.ReadToEnd();
                    }

                    args = new AsyncCompletedEventArgs(null, ((result.IsCompleted) && (result.CompletedSynchronously)), returnValue);

                    if (this.EnableTracing)
                    {
                        TracingHelper.Trace(new object[] { "Response information:", response.Headers, response.ResponseUri, returnValue }, this.TraceSourceName);
                    }
                }

                if ((request.Method.ToLower() == "post") && (this.PostCompleted != null))
                {
                    this.PostCompleted.Invoke(this, args);
                }
                else if ((request.Method.ToLower() == "get") && (this.GetCompleted != null))
                {
                    this.GetCompleted.Invoke(this, args);
                }

                #endregion
            }
        }

        //private void trace(object[] data)
        //{
        //    try
        //    {
        //        System.Diagnostics.TraceSource trace = new System.Diagnostics.TraceSource("DataIntegratorTraceSource");

        //        trace.TraceData(System.Diagnostics.TraceEventType.Information, new Random().Next(), data);

        //        trace.Flush();
        //    }
        //    catch (Exception)
        //    {
        //        //If you want to handle this exception, add your exception handling code here, else you may uncomment the following line to throw this exception out.
        //        throw;
        //    }
        //}

        public event AsyncCompletedEventHandler PostCompleted;
        public event AsyncCompletedEventHandler GetCompleted;


        public virtual object Post(string uri, string data, Authentication authentication, Dictionary<string, string> headers)
        {
           return this.createSyncRequest(uri, data, "POST", authentication, headers);
        }

        public virtual object Get(string uri, Authentication authentication, Dictionary<string, string> headers)
        {
            return this.createSyncRequest(uri, null, "GET", authentication, headers);
        }

        public virtual void PostAsync(string uri, byte[] data, Authentication authentication, Dictionary<string, string> headers)
        {
            this.createAsyncRequest(uri, data, "POST", authentication, headers);
        }

        public virtual void GetAsync(string uri, Authentication authentication, Dictionary<string, string> headers)
        {
            this.createAsyncRequest(uri, null, "GET", authentication, headers);
        }
    }
}
