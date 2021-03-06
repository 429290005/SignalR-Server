// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.


using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace Microsoft.AspNet.SignalR.Http
{
    public class ServerResponse : IResponse
    {
        private readonly CancellationToken _callCancelled;
        private readonly HttpResponse _response;
        private readonly Stream _responseBody;

        public ServerResponse(HttpContext context)
        {
            _response = context.Response;

            _callCancelled = context.Request.CallCanceled;
            _responseBody = _response.Body;
        }

        public CancellationToken CancellationToken
        {
            get { return _callCancelled; }
        }

        public int StatusCode
        {
            get { return _response.StatusCode; }
            set { _response.StatusCode = value; }
        }

        public string ContentType
        {
            get { return _response.ContentType; }
            set { _response.ContentType = value; }
        }

        public void Write(ArraySegment<byte> data)
        {
            _responseBody.Write(data.Array, data.Offset, data.Count);
        }

        public Task Flush()
        {
            return _responseBody.FlushAsync();
        }
    }
}