﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Azure.Monitor.OpenTelemetry.Exporter.Models;
using Xunit;

namespace Azure.Monitor.OpenTelemetry.Exporter.Tests.CommonTestFramework
{
    internal static class TelemetryItemValidationHelper
    {
        public static void AssertCounter_As_MetricTelemetry(
            TelemetryItem telemetryItem,
            string expectedMetricsName,
            string expectedMetricsNamespace,
            double expectedMetricsValue,
            IDictionary<string, string> expectedMetricsProperties)
        {
            Assert.Equal("Metric", telemetryItem.Name); // telemetry type
            Assert.Equal("MetricData", telemetryItem.Data.BaseType); // telemetry data type
            Assert.Equal(2, telemetryItem.Data.BaseData.Version); // telemetry api version
            Assert.Equal("00000000-0000-0000-0000-000000000000", telemetryItem.InstrumentationKey);

            Assert.Equal(3, telemetryItem.Tags.Count);
            Assert.Contains("ai.cloud.role", telemetryItem.Tags.Keys);
            Assert.Contains("ai.cloud.roleInstance", telemetryItem.Tags.Keys);
            Assert.Contains("ai.internal.sdkVersion", telemetryItem.Tags.Keys);

            var metricsData = (MetricsData)telemetryItem.Data.BaseData;
            Assert.Equal(expectedMetricsName, metricsData.Metrics[0].Name);
            Assert.Equal(expectedMetricsNamespace, metricsData.Metrics[0].Namespace);
            Assert.Equal(expectedMetricsValue, metricsData.Metrics[0].Value);

            foreach (var prop in expectedMetricsProperties)
            {
                Assert.Equal(prop.Value, metricsData.Properties[prop.Key]);
            }
        }

        public static void AssertHistogram_As_MetricTelemetry(
            TelemetryItem telemetryItem,
            string expectedMetricsName,
            string expectedMetricsNamespace,
            int expectedMetricsCount,
            double expectedMetricsValue,
            IDictionary<string, string> expectedMetricsProperties)
        {
            Assert.Equal("Metric", telemetryItem.Name); // telemetry type
            Assert.Equal("MetricData", telemetryItem.Data.BaseType); // telemetry data type
            Assert.Equal(2, telemetryItem.Data.BaseData.Version); // telemetry api version
            Assert.Equal("00000000-0000-0000-0000-000000000000", telemetryItem.InstrumentationKey);

            Assert.Equal(3, telemetryItem.Tags.Count);
            Assert.Contains("ai.cloud.role", telemetryItem.Tags.Keys);
            Assert.Contains("ai.cloud.roleInstance", telemetryItem.Tags.Keys);
            Assert.Contains("ai.internal.sdkVersion", telemetryItem.Tags.Keys);

            var metricsData = (MetricsData)telemetryItem.Data.BaseData;
            Assert.Equal(expectedMetricsName, metricsData.Metrics[0].Name);
            Assert.Equal(expectedMetricsNamespace, metricsData.Metrics[0].Namespace);
            Assert.Equal(expectedMetricsCount, metricsData.Metrics[0].Count);
            Assert.Equal(expectedMetricsValue, metricsData.Metrics[0].Value);

            foreach (var prop in expectedMetricsProperties)
            {
                Assert.Equal(prop.Value, metricsData.Properties[prop.Key]);
            }
        }

        public static void AssertLog_As_MessageTelemetry(
            TelemetryItem telemetryItem,
            string expectedSeverityLevel,
            string expectedMessage,
            IDictionary<string, string> expectedMeessageProperties,
            string expectedSpanId,
            string expectedTraceId)
        {
            Assert.Equal("Message", telemetryItem.Name); // telemetry type
            Assert.Equal("MessageData", telemetryItem.Data.BaseType); // telemetry data type
            Assert.Equal(2, telemetryItem.Data.BaseData.Version); // telemetry api version
            Assert.Equal("00000000-0000-0000-0000-000000000000", telemetryItem.InstrumentationKey);

            var expectedTagsCount = 3;

            if (expectedSpanId != null && expectedTraceId != null)
            {
                expectedTagsCount = 5;

                Assert.Equal(expectedSpanId, telemetryItem.Tags["ai.operation.parentId"]);
                Assert.Equal(expectedTraceId, telemetryItem.Tags["ai.operation.id"]);
            }

            Assert.Equal(expectedTagsCount, telemetryItem.Tags.Count);
            Assert.Contains("ai.cloud.role", telemetryItem.Tags.Keys);
            Assert.Contains("ai.cloud.roleInstance", telemetryItem.Tags.Keys);
            Assert.Contains("ai.internal.sdkVersion", telemetryItem.Tags.Keys);

            var messageData = (MessageData)telemetryItem.Data.BaseData;
            Assert.Equal(expectedSeverityLevel, messageData.SeverityLevel);
            Assert.Equal(expectedMessage, messageData.Message);

            foreach (var prop in expectedMeessageProperties)
            {
                Assert.Equal(prop.Value, messageData.Properties[prop.Key]);
            }
        }

        public static void AssertLog_As_ExceptionTelemetry(
            TelemetryItem telemetryItem,
            string expectedSeverityLevel,
            string expectedMessage,
            string expectedTypeName)
        {
            Assert.Equal("Exception", telemetryItem.Name); // telemetry type
            Assert.Equal("ExceptionData", telemetryItem.Data.BaseType); // telemetry data type
            Assert.Equal(2, telemetryItem.Data.BaseData.Version); // telemetry api version
            Assert.Equal("00000000-0000-0000-0000-000000000000", telemetryItem.InstrumentationKey);

            Assert.Equal(3, telemetryItem.Tags.Count);
            Assert.Contains("ai.cloud.role", telemetryItem.Tags.Keys);
            Assert.Contains("ai.cloud.roleInstance", telemetryItem.Tags.Keys);
            Assert.Contains("ai.internal.sdkVersion", telemetryItem.Tags.Keys);

            var telemetryExceptionData = (TelemetryExceptionData)telemetryItem.Data.BaseData;
            Assert.Equal(expectedSeverityLevel, telemetryExceptionData.SeverityLevel);

            Assert.Equal(1, telemetryExceptionData.Exceptions.Count);

            var telemetryExceptionDetails = (TelemetryExceptionDetails)telemetryExceptionData.Exceptions[0];
            Assert.Equal(expectedMessage, telemetryExceptionDetails.Message);
            Assert.Equal(expectedTypeName, telemetryExceptionDetails.TypeName);
            Assert.True(telemetryExceptionDetails.ParsedStack.Any());
        }

        public static void AssertActivity_As_DependencyTelemetry(
            TelemetryItem telemetryItem,
            string expectedName,
            string expectedTraceId,
            IDictionary<string, string> expectedProperties)
        {
            Assert.Equal("RemoteDependency", telemetryItem.Name); // telemetry type
            Assert.Equal("RemoteDependencyData", telemetryItem.Data.BaseType); // telemetry data type
            Assert.Equal(2, telemetryItem.Data.BaseData.Version); // telemetry api version
            Assert.Equal("00000000-0000-0000-0000-000000000000", telemetryItem.InstrumentationKey);

            Assert.Equal(4, telemetryItem.Tags.Count);
            Assert.Equal(expectedTraceId, telemetryItem.Tags["ai.operation.id"]);
            Assert.Contains("ai.cloud.role", telemetryItem.Tags.Keys);
            Assert.Contains("ai.cloud.roleInstance", telemetryItem.Tags.Keys);
            Assert.Contains("ai.internal.sdkVersion", telemetryItem.Tags.Keys);

            var remoteDependencyData = (RemoteDependencyData)telemetryItem.Data.BaseData;
            Assert.Equal(expectedName, remoteDependencyData.Name);

            if (expectedProperties == null)
            {
                Assert.Empty(remoteDependencyData.Properties);
            }
            else
            {
                foreach (var prop in expectedProperties)
                {
                    Assert.Equal(prop.Value, remoteDependencyData.Properties[prop.Key]);
                }
            }
        }

        public static void AssertActivity_As_RequestTelemetry(
            TelemetryItem telemetryItem,
            ActivityKind activityKind,
            string expectedName,
            string expectedTraceId,
            IDictionary<string, string> expectedProperties)
        {
            Assert.Equal("Request", telemetryItem.Name); // telemetry type
            Assert.Equal("RequestData", telemetryItem.Data.BaseType); // telemetry data type
            Assert.Equal(2, telemetryItem.Data.BaseData.Version); // telemetry api version
            Assert.Equal("00000000-0000-0000-0000-000000000000", telemetryItem.InstrumentationKey);

            var expectedTagsCount = 4;

            if (activityKind == ActivityKind.Server)
            {
                expectedTagsCount = 6;

                Assert.Contains("ai.operation.name", telemetryItem.Tags.Keys);
                Assert.Contains("ai.location.ip", telemetryItem.Tags.Keys);
            }

            Assert.Equal(expectedTagsCount, telemetryItem.Tags.Count);
            Assert.Equal(expectedTraceId, telemetryItem.Tags["ai.operation.id"]);
            Assert.Contains("ai.cloud.role", telemetryItem.Tags.Keys);
            Assert.Contains("ai.cloud.roleInstance", telemetryItem.Tags.Keys);
            Assert.Contains("ai.internal.sdkVersion", telemetryItem.Tags.Keys);

            var requestData = (RequestData)telemetryItem.Data.BaseData;
            Assert.Equal(expectedName, requestData.Name);

            if (expectedProperties == null)
            {
                Assert.Empty(requestData.Properties);
            }
            else
            {
                foreach (var prop in expectedProperties)
                {
                    Assert.Equal(prop.Value, requestData.Properties[prop.Key]);
                }
            }
        }
    }
}
