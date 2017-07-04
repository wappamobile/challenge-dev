﻿using System;
using Wappa.Framework.Google.Geocoding.Core;

namespace Wappa.Framework.Google.Geocoding.Exceptions
{
    public class GoogleGeocodingException : Exception
    {
        const string defaultMessage = "There was an error processing the geocoding request. See Status or InnerException for more information.";

        public GoogleStatus Status { get; private set; }

        public GoogleGeocodingException(GoogleStatus status)
            : base(defaultMessage)
        {
            this.Status = status;
        }

        public GoogleGeocodingException(Exception innerException)
            : base(defaultMessage, innerException)
        {
            this.Status = GoogleStatus.Error;
        }
    }
}