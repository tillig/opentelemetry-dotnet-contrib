// <copyright file="OneCollectorExporterTransportOptions.cs" company="OpenTelemetry Authors">
// Copyright The OpenTelemetry Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

namespace OpenTelemetry.Exporter.OneCollector;

/// <summary>
/// Contains transport options for the <see cref="OneCollectorExporter{T}"/> class.
/// </summary>
public sealed class OneCollectorExporterTransportOptions
{
    internal const string DefaultOneCollectorEndpoint = "https://mobile.events.data.microsoft.com/OneCollector/1.0/";
    internal const int DefaultMaxPayloadSizeInBytes = 1024 * 1024 * 4;
    internal const int DefaultMaxNumberOfItemsPerPayload = 1500;

    internal static readonly Func<HttpClient> DefaultHttpClientFactory = () => new HttpClient();

    internal OneCollectorExporterTransportOptions()
    {
    }

    /// <summary>
    /// Gets or sets OneCollector endpoint address. Default value:
    /// https://mobile.events.data.microsoft.com/OneCollector/1.0/.
    /// </summary>
    public Uri Endpoint { get; set; } = new Uri(DefaultOneCollectorEndpoint);

    /// <summary>
    /// Gets or sets OneCollector transport protocol. Default value: <see
    /// cref="OneCollectorExporterTransportProtocolType.HttpJsonPost"/>.
    /// </summary>
    internal OneCollectorExporterTransportProtocolType Protocol { get; set; } = OneCollectorExporterTransportProtocolType.HttpJsonPost;

    /// <summary>
    /// Gets or sets the maximum request payload size in bytes when sending data
    /// to OneCollector. Default value: 4,194,304.
    /// </summary>
    /// <remarks>
    /// Note: Set to -1 for unlimited request payload size.
    /// </remarks>
    internal int MaxPayloadSizeInBytes { get; set; } = DefaultMaxPayloadSizeInBytes;

    /// <summary>
    /// Gets or sets the maximum number of items per request payload when
    /// sending data to OneCollector. Default value: 1500.
    /// </summary>
    /// <remarks>
    /// Note: Set to -1 for unlimited number of items per request payload.
    /// </remarks>
    internal int MaxNumberOfItemsPerPayload { get; set; } = DefaultMaxNumberOfItemsPerPayload;

    /// <summary>
    /// Gets or sets the compression type to use when transmiting telemetry over
    /// HTTP. Default value: <see
    /// cref="OneCollectorExporterHttpTransportCompressionType.Deflate"/>.
    /// </summary>
    internal OneCollectorExporterHttpTransportCompressionType HttpCompression { get; set; } = OneCollectorExporterHttpTransportCompressionType.Deflate;

    /// <summary>
    /// Gets or sets the factory function called to create the <see
    /// cref="HttpClient"/> instance that will be used at runtime to transmit
    /// telemetry over HTTP. The returned instance will be reused for all export
    /// invocations.
    /// </summary>
    /// <remarks>
    /// Notes:
    /// <list type="bullet">
    /// <item>The default behavior when using the <see
    /// cref="OneCollectorLogExporterOptions"/> class is an <see
    /// cref="HttpClient"/> will be instantiated directly.</item>
    /// </list>
    /// </remarks>
    internal Func<HttpClient> HttpClientFactory { get; set; } = DefaultHttpClientFactory;

    internal void Validate()
    {
        if (this.Endpoint == null)
        {
            throw new InvalidOperationException($"{nameof(this.Endpoint)} was not specified on {this.GetType().Name} options.");
        }

        if (this.HttpClientFactory == null)
        {
            throw new InvalidOperationException($"{nameof(this.HttpClientFactory)} was not specified on {this.GetType().Name} options.");
        }

        if (this.MaxPayloadSizeInBytes <= 0 && this.MaxPayloadSizeInBytes != -1)
        {
            throw new InvalidOperationException($"{nameof(this.MaxPayloadSizeInBytes)} was invalid on {this.GetType().Name} options.");
        }

        if (this.MaxNumberOfItemsPerPayload <= 0 && this.MaxNumberOfItemsPerPayload != -1)
        {
            throw new InvalidOperationException($"{nameof(this.MaxNumberOfItemsPerPayload)} was invalid on {this.GetType().Name} options.");
        }
    }
}
