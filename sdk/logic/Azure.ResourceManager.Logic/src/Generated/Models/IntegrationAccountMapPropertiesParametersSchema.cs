// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace Azure.ResourceManager.Logic.Models
{
    /// <summary> The parameters schema of integration account map. </summary>
    internal partial class IntegrationAccountMapPropertiesParametersSchema
    {
        /// <summary> Initializes a new instance of IntegrationAccountMapPropertiesParametersSchema. </summary>
        public IntegrationAccountMapPropertiesParametersSchema()
        {
        }

        /// <summary> Initializes a new instance of IntegrationAccountMapPropertiesParametersSchema. </summary>
        /// <param name="ref"> The reference name. </param>
        internal IntegrationAccountMapPropertiesParametersSchema(string @ref)
        {
            Ref = @ref;
        }

        /// <summary> The reference name. </summary>
        public string Ref { get; set; }
    }
}
