// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

#nullable enable

namespace Azure.Core
{
    /// <summary>
    /// Implementation of a <see cref="OperationPollingStrategy"/> with constant polling interval.
    /// </summary>
    /// <remarks>Polling interval is always equal to the given polling interval.</remarks>
    internal class ConstantPollingStrategy : OperationPollingStrategy
    {
        internal static readonly TimeSpan DefaultPollingInterval = TimeSpan.FromSeconds(1);

        /// <summary>
        /// Create a <see cref="ConstantPollingStrategy"/> with 1 second polling interval.
        /// </summary>
        public ConstantPollingStrategy()
        {
        }

        /// <summary>
        /// Get the polling interval from the max value of <see cref="DefaultPollingInterval"/> and <paramref name="suggestedInterval"/>.
        /// </summary>
        /// <param name="response">Service response.</param>
        /// <param name="suggestedInterval">Suggested pollingInterval.</param>
        /// <returns>Max value of <see cref="DefaultPollingInterval"/> and <paramref name="suggestedInterval"/>.</returns>
        public override TimeSpan GetNextWait(Response response, TimeSpan? suggestedInterval)
            => suggestedInterval.HasValue ? Max(DefaultPollingInterval, suggestedInterval.Value) : DefaultPollingInterval;
    }
}
