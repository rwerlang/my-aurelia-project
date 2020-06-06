using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicatonProcess.May2020.Domain.Models
{
    /// <summary>
    /// This class represents a HATEOAS link to be used with the RestEntityModel
    /// </summary>
    public class RestLink
    {
        /// <summary>
        /// The type of relationship for the link. Default = self
        /// </summary>
        public string Rel { get; set; } = "self";
        /// <summary>
        /// The url to access the rest endpoint
        /// </summary>
        public string Href { get; set; }
        /// <summary>
        /// The http method used to call the endpoint
        /// </summary>
        public string Action { get; set; } = "GET";
        /// <summary>
        /// The list of types accepted by the endpoint.
        /// i.e. application/json
        /// </summary>
        public string[] Types { get; set; } = Array.Empty<string>();
    }
}
