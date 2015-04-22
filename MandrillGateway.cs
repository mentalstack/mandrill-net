namespace MandrillNet
{
    using RestSharp;

    using MandrillNet.Entities;
    using MandrillNet.Infrastructure;

    using System.Collections;
    using System.Collections.Generic;

    using System;
    using System.Configuration;
    using System.Text;

    /// <summary>
    /// Mandrill gateway.
    /// </summary>
    public class MandrillGateway : IMandrillGateway
    {
        #region Defines

        private const string BASE_URL = "https://mandrillapp.com/api/1.0/";

        #endregion

        #region Private Fields

        /// <summary>
        /// Mandrill api key.
        /// </summary>
        private string _apiKey = null;

        #endregion

        #region Private Fields : Client

        /// <summary>
        /// Rest client.
        /// </summary>
        private RestClient _client = null;

        #endregion

        #region Private Methods

        #endregion

        #region Private Methods : Event Handlers

        /// <summary>
        /// Processes response errors.
        /// </summary>
        private void OnBeforeDeserialization(IRestResponse response)
        {
            if (!CheckResponse((int)response.StatusCode))
            {
                throw new InvalidOperationException(response.ErrorMessage);
            }
        }

        #endregion

        #region Private Methods : Request

        /// <summary>
        /// Checks response status.
        /// </summary>
        private bool CheckResponse(int statusCode)
        {
            return (statusCode >= 200 && statusCode <= 299);
        }

        /// <summary>
        /// Gets new request.
        /// </summary>
        private RestRequest BuildRequest(Method method, string[] segments, Dictionary<string, string> parameters = null, object body = null)
        {
            RestRequest result = null;

            var resource = new StringBuilder(segments[0]);
            {
                for (int i = 1; i < segments.Length; i++) resource.AppendFormat("/{0}", segments[i]);
            }

            result = new RestRequest(resource.ToString(), method)
            {
                RequestFormat = DataFormat.Json, JsonSerializer = new JsonNetSerializer(), Timeout = 60000
            };

            result.OnBeforeDeserialization = OnBeforeDeserialization;

            if (parameters != null && parameters.Count > 0)
            {
                foreach (var p in parameters) result.AddParameter(p.Key, p.Value);
            }

            if (body != null) result.AddBody(body);
            {
                return result;
            }
        }

        /// <summary>
        /// Gets new request.
        /// </summary>
        private RestRequest BuildRequest(Method method, string resource, object body = null)
        {
            RestRequest result = null;

            result = new RestRequest(resource, method)
            {
                RequestFormat = DataFormat.Json, JsonSerializer = new JsonNetSerializer(), Timeout = 60000
            };

            result.OnBeforeDeserialization = OnBeforeDeserialization;

            if (body != null) result.AddBody(body);
            {
                return result;
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Executes request.
        /// </summary>
        protected void Execute(Method method, string[] segments, Dictionary<string, string> parameters = null, object body = null)
        {
            _client.Execute(BuildRequest(method, segments, parameters, body));
        }

        /// <summary>
        /// Executes request.
        /// </summary>
        protected T Execute<T>(Method method, string[] segments, Dictionary<string, string> parameters = null, object body = null) where T : new()
        {
            return _client.Execute<T>(BuildRequest(method, segments, parameters, body)).Data;
        }

        /// <summary>
        /// Executes request.
        /// </summary>
        protected T Execute<T>(Method method, string resource, object body = null) where T : new()
        {
            return _client.Execute<T>(BuildRequest(method, resource, body)).Data;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Binds gateway.
        /// </summary>
        public IMandrillGateway Bind()
        {
            _apiKey = ConfigurationManager.AppSettings["MandrillApiKey"];
            {
                return this;
            }
        }

        /// <summary>
        /// Sends transactional message.
        /// </summary>
        public List<SendResult> Send(Envelope envelope)
        {
            object body = new { Key = _apiKey, Message = envelope };
            {
                return Execute<List<SendResult>>(Method.POST, new[] { "/messages/send.json" }, body: body);
            }
        }

        #endregion

        #region Public Methods : IDisposible

        /// <summary>
        /// Disposes current instance.
        /// </summary>
        public void Dispose()
        {
            _client = null; GC.SuppressFinalize(this);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MandrillGateway()
        {
            _client = new RestClient(BASE_URL);
            {
                _client.AddHandler("application/json", new JsonNetSerializer());
            }
        }

        #endregion
    }
}
