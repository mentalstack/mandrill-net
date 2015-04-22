namespace MandrillNet.Infrastructure
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Newtonsoft.Json.Converters;

    using RestSharp;
    using RestSharp.Deserializers;
    using RestSharp.Serializers;

    using System.Collections;
    using System.Collections.Generic;

    using System;
    using System.Reflection;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Json.NET serializer.
    /// </summary>
    public class JsonNetSerializer : ISerializer, IDeserializer
    {
        #region Defines

        /// <summary>
        /// RFC1123 date time format.
        /// </summary>
        public const string RFC1123 = "R";

        #endregion

        #region Private Properties

        /// <summary>
        /// Gets or sets JSON settings instance.
        /// </summary>
        private JsonSerializerSettings Settings { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets date format.
        /// </summary>
        public string DateFormat { get; set; }

        /// <summary>
        /// Gets or sets root element.
        /// </summary>
        public string RootElement { get; set; }

        /// <summary>
        /// Gets or sets namespace.
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Gets or sets content type.
        /// </summary>
        public string ContentType { get; set; }

        #endregion

        #region Public Methods : Serialization

        /// <summary>
        /// Serialize object to JSON
        /// </summary>
        public string Serialize(object obj)
        {
            var settings = new JsonSerializerSettings // define serializer settings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = NullValueHandling.Include
            };

            settings.Converters.Add(new IsoDateTimeConverter
            {
                DateTimeFormat = DateFormat ?? RFC1123, DateTimeStyles = DateTimeStyles.None
            });

            return JsonConvert.SerializeObject(obj, settings);
        }

        /// <summary>
        /// Serialize object to JSON
        /// </summary>
        public T Deserialize<T>(IRestResponse response)
        {
            string content = response.Content;

            var settings = new JsonSerializerSettings // define de-serializer settings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = NullValueHandling.Include
            };

            settings.Converters.Add(new IsoDateTimeConverter
            {
                DateTimeFormat = DateFormat ?? RFC1123, DateTimeStyles = DateTimeStyles.None
            });

            return JsonConvert.DeserializeObject<T>(content, settings);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public JsonNetSerializer()
        {
            ContentType = "application/json";
        }

        #endregion
    }
}
