// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.MachineLearning.Models
{
    public partial class MachineLearningDeploymentLogsContent : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (Optional.IsDefined(ContainerType))
            {
                writer.WritePropertyName("containerType");
                writer.WriteStringValue(ContainerType.Value.ToString());
            }
            if (Optional.IsDefined(Tail))
            {
                if (Tail != null)
                {
                    writer.WritePropertyName("tail");
                    writer.WriteNumberValue(Tail.Value);
                }
                else
                {
                    writer.WriteNull("tail");
                }
            }
            writer.WriteEndObject();
        }
    }
}
