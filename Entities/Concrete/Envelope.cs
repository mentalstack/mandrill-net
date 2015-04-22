namespace MandrillNet.Entities
{
    using Newtonsoft.Json;

    using System;

    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Email address.
    /// </summary>
    public class EmailAddress
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }

        #endregion
    }

    /// <summary>
    /// Email attachment.
    /// </summary>
    public class EnvelopeAttachment
    {
        #region Private Fields

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets base64 ontent.
        /// </summary>
        public string Content { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EnvelopeAttachment() { }

        /// <summary>
        /// Constructor with specified parameter.
        /// </summary>
        public EnvelopeAttachment(string name)
        {
            Name = name;
        }

        #endregion
    }

    /// <summary>
    /// Email message.
    /// </summary>
    public class Envelope
    {
        #region Private Fields

        /// <summary>
        /// List of TOs.
        /// </summary>
        private List<EmailAddress> _to = null;

        /// <summary>
        /// Attachments list.
        /// </summary>
        private List<EnvelopeAttachment> _attachments = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets from email address and name.
        /// </summary>
        [JsonProperty(PropertyName = "from_email")]
        public string FromEmail { get; set; }

        /// <summary>
        /// Gets or sets from name.
        /// </summary>
        [JsonProperty(PropertyName = "from_name")]
        public string FromName { get; set; }

        /// <summary>
        /// Gets or sets TOs list.
        /// </summary>
        public List<EmailAddress> To
        {
            get { return _to ?? (_to = new List<EmailAddress>()); }

            set { _to = new List<EmailAddress>(); }
        }

        /// <summary>
        /// Gets or sets attachments.
        /// </summary>
        public List<EnvelopeAttachment> Attachments
        {
            get { return _attachments ?? (_attachments = new List<EnvelopeAttachment>()); }

            set { _attachments = value; }
        }

        /// <summary>
        /// Gets or sets html.
        /// </summary>
        public string Html { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Envelope() { }

        /// <summary>
        /// Constructor with specified parameters.
        /// </summary>
        public Envelope(string subject, string body) : this()
        {
            Subject = subject; Html = body;
        }

        /// <summary>
        /// Constructor with specified parameters.
        /// </summary>
        public Envelope(string subject) : this()
        {
            Subject = subject;
        }

        #endregion
    }
}
