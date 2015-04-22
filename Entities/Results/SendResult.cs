namespace MandrillNet.Entities
{
    /// <summary>
    /// Send result.
    /// </summary>
    public class SendResult
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets reject reason.
        /// </summary>
        public string RejectReason { get; set; }

        /// <summary>
        /// Gets or sets email.
        /// </summary>
        public string Email { get; set; }

        #endregion
    }
}
