// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.ResourceManager;

[assembly: CodeGenSuppressType("SecurityInsightsAlertRuleTemplateCollection")]
namespace Azure.ResourceManager.SecurityInsights
{
    /// <summary>
    /// A class representing a collection of <see cref="SecurityInsightsAlertRuleTemplateResource" /> and their operations.
    /// Each <see cref="SecurityInsightsAlertRuleTemplateResource" /> in the collection will belong to the same instance of <see cref="OperationalInsightsWorkspaceSecurityInsightsResource" />.
    /// To get a <see cref="SecurityInsightsAlertRuleTemplateCollection" /> instance call the GetSecurityInsightsAlertRuleTemplates method from an instance of <see cref="OperationalInsightsWorkspaceSecurityInsightsResource" />.
    /// </summary>
    public partial class SecurityInsightsAlertRuleTemplateCollection : ArmCollection, IEnumerable<SecurityInsightsAlertRuleTemplateResource>, IAsyncEnumerable<SecurityInsightsAlertRuleTemplateResource>
    {
        private readonly ClientDiagnostics _securityInsightsAlertRuleTemplateAlertRuleTemplatesClientDiagnostics;
        private readonly AlertRuleTemplatesRestOperations _securityInsightsAlertRuleTemplateAlertRuleTemplatesRestClient;

        /// <summary> Initializes a new instance of the <see cref="SecurityInsightsAlertRuleTemplateCollection"/> class for mocking. </summary>
        protected SecurityInsightsAlertRuleTemplateCollection()
        {
        }

        /// <summary> Initializes a new instance of the <see cref="SecurityInsightsAlertRuleTemplateCollection"/> class. </summary>
        /// <param name="client"> The client parameters to use in these operations. </param>
        /// <param name="id"> The identifier of the parent resource that is the target of operations. </param>
        internal SecurityInsightsAlertRuleTemplateCollection(ArmClient client, ResourceIdentifier id) : base(client, id)
        {
            _securityInsightsAlertRuleTemplateAlertRuleTemplatesClientDiagnostics = new ClientDiagnostics("Azure.ResourceManager.SecurityInsights", SecurityInsightsAlertRuleTemplateResource.ResourceType.Namespace, Diagnostics);
            TryGetApiVersion(SecurityInsightsAlertRuleTemplateResource.ResourceType, out string securityInsightsAlertRuleTemplateAlertRuleTemplatesApiVersion);
            _securityInsightsAlertRuleTemplateAlertRuleTemplatesRestClient = new AlertRuleTemplatesRestOperations(Pipeline, Diagnostics.ApplicationId, Endpoint, securityInsightsAlertRuleTemplateAlertRuleTemplatesApiVersion);
#if DEBUG
			ValidateResourceId(Id);
#endif
        }

        internal static void ValidateResourceId(ResourceIdentifier id)
        {
            if (id.ResourceType != OperationalInsightsWorkspaceSecurityInsightsResource.ResourceType)
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Invalid resource type {0} expected {1}", id.ResourceType, OperationalInsightsWorkspaceSecurityInsightsResource.ResourceType), nameof(id));
        }

        /// <summary>
        /// Gets the alert rule template.
        /// Request Path: /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.OperationalInsights/workspaces/{workspaceName}/providers/Microsoft.SecurityInsights/alertRuleTemplates/{alertRuleTemplateId}
        /// Operation Id: AlertRuleTemplates_Get
        /// </summary>
        /// <param name="alertRuleTemplateId"> Alert rule template ID. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentException"> <paramref name="alertRuleTemplateId"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="ArgumentNullException"> <paramref name="alertRuleTemplateId"/> is null. </exception>
        public virtual async Task<Response<SecurityInsightsAlertRuleTemplateResource>> GetAsync(string alertRuleTemplateId, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(alertRuleTemplateId, nameof(alertRuleTemplateId));

            using var scope = _securityInsightsAlertRuleTemplateAlertRuleTemplatesClientDiagnostics.CreateScope("SecurityInsightsAlertRuleTemplateCollection.Get");
            scope.Start();
            try
            {
                var response = await _securityInsightsAlertRuleTemplateAlertRuleTemplatesRestClient.GetAsync(Id.SubscriptionId, Id.ResourceGroupName, Id.Name, alertRuleTemplateId, cancellationToken).ConfigureAwait(false);
                if (response.Value == null)
                    throw new RequestFailedException(response.GetRawResponse());
                return Response.FromValue(new SecurityInsightsAlertRuleTemplateResource(Client, response.Value), response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Gets the alert rule template.
        /// Request Path: /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.OperationalInsights/workspaces/{workspaceName}/providers/Microsoft.SecurityInsights/alertRuleTemplates/{alertRuleTemplateId}
        /// Operation Id: AlertRuleTemplates_Get
        /// </summary>
        /// <param name="alertRuleTemplateId"> Alert rule template ID. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentException"> <paramref name="alertRuleTemplateId"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="ArgumentNullException"> <paramref name="alertRuleTemplateId"/> is null. </exception>
        public virtual Response<SecurityInsightsAlertRuleTemplateResource> Get(string alertRuleTemplateId, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(alertRuleTemplateId, nameof(alertRuleTemplateId));

            using var scope = _securityInsightsAlertRuleTemplateAlertRuleTemplatesClientDiagnostics.CreateScope("SecurityInsightsAlertRuleTemplateCollection.Get");
            scope.Start();
            try
            {
                var response = _securityInsightsAlertRuleTemplateAlertRuleTemplatesRestClient.Get(Id.SubscriptionId, Id.ResourceGroupName, Id.Name, alertRuleTemplateId, cancellationToken);
                if (response.Value == null)
                    throw new RequestFailedException(response.GetRawResponse());
                return Response.FromValue(new SecurityInsightsAlertRuleTemplateResource(Client, response.Value), response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Gets all alert rule templates.
        /// Request Path: /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.OperationalInsights/workspaces/{workspaceName}/providers/Microsoft.SecurityInsights/alertRuleTemplates
        /// Operation Id: AlertRuleTemplates_List
        /// </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <returns> An async collection of <see cref="SecurityInsightsAlertRuleTemplateResource" /> that may take multiple service requests to iterate over. </returns>
        public virtual AsyncPageable<SecurityInsightsAlertRuleTemplateResource> GetAllAsync(CancellationToken cancellationToken = default)
        {
            async Task<Page<SecurityInsightsAlertRuleTemplateResource>> FirstPageFunc(int? pageSizeHint)
            {
                using var scope = _securityInsightsAlertRuleTemplateAlertRuleTemplatesClientDiagnostics.CreateScope("SecurityInsightsAlertRuleTemplateCollection.GetAll");
                scope.Start();
                try
                {
                    var response = await _securityInsightsAlertRuleTemplateAlertRuleTemplatesRestClient.ListAsync(Id.SubscriptionId, Id.ResourceGroupName, Id.Name, cancellationToken: cancellationToken).ConfigureAwait(false);
                    return Page.FromValues(response.Value.Value.Select(value => new SecurityInsightsAlertRuleTemplateResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }
            async Task<Page<SecurityInsightsAlertRuleTemplateResource>> NextPageFunc(string nextLink, int? pageSizeHint)
            {
                using var scope = _securityInsightsAlertRuleTemplateAlertRuleTemplatesClientDiagnostics.CreateScope("SecurityInsightsAlertRuleTemplateCollection.GetAll");
                scope.Start();
                try
                {
                    var response = await _securityInsightsAlertRuleTemplateAlertRuleTemplatesRestClient.ListNextPageAsync(nextLink, Id.SubscriptionId, Id.ResourceGroupName, Id.Name, cancellationToken: cancellationToken).ConfigureAwait(false);
                    return Page.FromValues(response.Value.Value.Select(value => new SecurityInsightsAlertRuleTemplateResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }
            return PageableHelpers.CreateAsyncEnumerable(FirstPageFunc, NextPageFunc);
        }

        /// <summary>
        /// Gets all alert rule templates.
        /// Request Path: /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.OperationalInsights/workspaces/{workspaceName}/providers/Microsoft.SecurityInsights/alertRuleTemplates
        /// Operation Id: AlertRuleTemplates_List
        /// </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <returns> A collection of <see cref="SecurityInsightsAlertRuleTemplateResource" /> that may take multiple service requests to iterate over. </returns>
        public virtual Pageable<SecurityInsightsAlertRuleTemplateResource> GetAll(CancellationToken cancellationToken = default)
        {
            Page<SecurityInsightsAlertRuleTemplateResource> FirstPageFunc(int? pageSizeHint)
            {
                using var scope = _securityInsightsAlertRuleTemplateAlertRuleTemplatesClientDiagnostics.CreateScope("SecurityInsightsAlertRuleTemplateCollection.GetAll");
                scope.Start();
                try
                {
                    var response = _securityInsightsAlertRuleTemplateAlertRuleTemplatesRestClient.List(Id.SubscriptionId, Id.ResourceGroupName, Id.Name, cancellationToken: cancellationToken);
                    return Page.FromValues(response.Value.Value.Select(value => new SecurityInsightsAlertRuleTemplateResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }
            Page<SecurityInsightsAlertRuleTemplateResource> NextPageFunc(string nextLink, int? pageSizeHint)
            {
                using var scope = _securityInsightsAlertRuleTemplateAlertRuleTemplatesClientDiagnostics.CreateScope("SecurityInsightsAlertRuleTemplateCollection.GetAll");
                scope.Start();
                try
                {
                    var response = _securityInsightsAlertRuleTemplateAlertRuleTemplatesRestClient.ListNextPage(nextLink, Id.SubscriptionId, Id.ResourceGroupName, Id.Name, cancellationToken: cancellationToken);
                    return Page.FromValues(response.Value.Value.Select(value => new SecurityInsightsAlertRuleTemplateResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }
            return PageableHelpers.CreateEnumerable(FirstPageFunc, NextPageFunc);
        }

        /// <summary>
        /// Checks to see if the resource exists in azure.
        /// Request Path: /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.OperationalInsights/workspaces/{workspaceName}/providers/Microsoft.SecurityInsights/alertRuleTemplates/{alertRuleTemplateId}
        /// Operation Id: AlertRuleTemplates_Get
        /// </summary>
        /// <param name="alertRuleTemplateId"> Alert rule template ID. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentException"> <paramref name="alertRuleTemplateId"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="ArgumentNullException"> <paramref name="alertRuleTemplateId"/> is null. </exception>
        public virtual async Task<Response<bool>> ExistsAsync(string alertRuleTemplateId, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(alertRuleTemplateId, nameof(alertRuleTemplateId));

            using var scope = _securityInsightsAlertRuleTemplateAlertRuleTemplatesClientDiagnostics.CreateScope("SecurityInsightsAlertRuleTemplateCollection.Exists");
            scope.Start();
            try
            {
                var response = await _securityInsightsAlertRuleTemplateAlertRuleTemplatesRestClient.GetAsync(Id.SubscriptionId, Id.ResourceGroupName, Id.Name, alertRuleTemplateId, cancellationToken: cancellationToken).ConfigureAwait(false);
                return Response.FromValue(response.Value != null, response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Checks to see if the resource exists in azure.
        /// Request Path: /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.OperationalInsights/workspaces/{workspaceName}/providers/Microsoft.SecurityInsights/alertRuleTemplates/{alertRuleTemplateId}
        /// Operation Id: AlertRuleTemplates_Get
        /// </summary>
        /// <param name="alertRuleTemplateId"> Alert rule template ID. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentException"> <paramref name="alertRuleTemplateId"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="ArgumentNullException"> <paramref name="alertRuleTemplateId"/> is null. </exception>
        public virtual Response<bool> Exists(string alertRuleTemplateId, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(alertRuleTemplateId, nameof(alertRuleTemplateId));

            using var scope = _securityInsightsAlertRuleTemplateAlertRuleTemplatesClientDiagnostics.CreateScope("SecurityInsightsAlertRuleTemplateCollection.Exists");
            scope.Start();
            try
            {
                var response = _securityInsightsAlertRuleTemplateAlertRuleTemplatesRestClient.Get(Id.SubscriptionId, Id.ResourceGroupName, Id.Name, alertRuleTemplateId, cancellationToken: cancellationToken);
                return Response.FromValue(response.Value != null, response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        IEnumerator<SecurityInsightsAlertRuleTemplateResource> IEnumerable<SecurityInsightsAlertRuleTemplateResource>.GetEnumerator()
        {
            return GetAll().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetAll().GetEnumerator();
        }

        IAsyncEnumerator<SecurityInsightsAlertRuleTemplateResource> IAsyncEnumerable<SecurityInsightsAlertRuleTemplateResource>.GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            return GetAllAsync(cancellationToken: cancellationToken).GetAsyncEnumerator(cancellationToken);
        }
    }
}
