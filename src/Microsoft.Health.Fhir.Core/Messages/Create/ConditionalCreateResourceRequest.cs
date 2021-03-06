﻿// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using EnsureThat;
using MediatR;
using Microsoft.Health.Fhir.Core.Features.Conformance;
using Microsoft.Health.Fhir.Core.Messages.Upsert;
using Microsoft.Health.Fhir.Core.Models;

namespace Microsoft.Health.Fhir.Core.Messages.Create
{
    public class ConditionalCreateResourceRequest : IRequest<UpsertResourceResponse>, IRequest, IRequireCapability
    {
        public ConditionalCreateResourceRequest(ResourceElement resource, IReadOnlyList<Tuple<string, string>> conditionalParameters)
        {
            EnsureArg.IsNotNull(resource, nameof(resource));
            EnsureArg.IsNotNull(conditionalParameters, nameof(conditionalParameters));

            Resource = resource;
            ConditionalParameters = conditionalParameters;
        }

        public ResourceElement Resource { get; }

        public IReadOnlyList<Tuple<string, string>> ConditionalParameters { get; }

        public IEnumerable<CapabilityQuery> RequiredCapabilities()
        {
            yield return new CapabilityQuery($"CapabilityStatement.rest.resource.where(type = '{Resource.InstanceType}').conditionalCreate = true");
        }
    }
}
