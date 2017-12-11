// AcceptNegotiationResponse.cs
//
// Created on 4/4/2017 by Scott Morgan
//
// Description:
//
using System.Runtime.Serialization;

namespace Refapp.Model.Response
{
	[DataContract]
	public class GeneralResponse
	{

		[DataMember (Name="httpStatusCode")]
		public int HTTPStatusCode { get; set; }

		[DataMember (Name="developerMessage")]
		public string DeveloperMessage { get; set; }

		[DataMember(Name = "status")]
		public string Status { get; set; }

		[DataMember(Name = "statusMessage")]
		public string StatusMessage { get; set; }

		public GeneralResponse()
		{
		}
	}
}
