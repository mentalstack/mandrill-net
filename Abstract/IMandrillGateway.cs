namespace MandrillNet
{
    using RestSharp;

    using MandrillNet;
    using MandrillNet.Entities;

    using System.Collections;
    using System.Collections.Generic;

    using System;

    /// <summary>
    /// Mandrill gateway interface.
    /// </summary>
    public interface IMandrillGateway : IDisposable
    {
        #region Public Methods

        /// <summary>
        /// Binds gateway.
        /// </summary>
        IMandrillGateway Bind();

        /// <summary>
        /// Sends transactional message.
        /// </summary>
        List<SendResult> Send(Envelope envelope);

        #endregion
    }
}
