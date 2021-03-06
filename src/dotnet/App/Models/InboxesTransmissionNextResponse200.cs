/*
 * SOCOMAP
 *
 * This API is for the new Socomap Protocol
 *
 * OpenAPI spec version: 0.0.1
 * Contact: development@infotech.de
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Org.OpenAPITools.Models
{ 
    /// <summary>
    /// message with the encryption fingerprint
    /// </summary>
    [DataContract]
    public partial class InboxesTransmissionNextResponse200 : IEquatable<InboxesTransmissionNextResponse200>
    { 
        /// <summary>
        /// returns the active transmission id for the nextInbound message
        /// </summary>
        /// <value>returns the active transmission id for the nextInbound message</value>
        [DataMember(Name="tid")]
        public string Tid { get; set; }

        /// <summary>
        /// encrypted and base64 encoded wsr message data
        /// </summary>
        /// <value>encrypted and base64 encoded wsr message data</value>
        [DataMember(Name="message")]
        public byte[] Message { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class InboxesTransmissionNextResponse200 {\n");
            sb.Append("  Tid: ").Append(Tid).Append("\n");
            sb.Append("  Message: ").Append(Message).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((InboxesTransmissionNextResponse200)obj);
        }

        /// <summary>
        /// Returns true if InboxesTransmissionNextResponse200 instances are equal
        /// </summary>
        /// <param name="other">Instance of InboxesTransmissionNextResponse200 to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(InboxesTransmissionNextResponse200 other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Tid == other.Tid ||
                    Tid != null &&
                    Tid.Equals(other.Tid)
                ) && 
                (
                    Message == other.Message ||
                    Message != null &&
                    Message.Equals(other.Message)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (Tid != null)
                    hashCode = hashCode * 59 + Tid.GetHashCode();
                    if (Message != null)
                    hashCode = hashCode * 59 + Message.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(InboxesTransmissionNextResponse200 left, InboxesTransmissionNextResponse200 right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(InboxesTransmissionNextResponse200 left, InboxesTransmissionNextResponse200 right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
