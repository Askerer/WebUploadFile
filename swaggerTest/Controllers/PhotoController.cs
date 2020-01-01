using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace swaggerTest.Controllers
{
    public class PhotoController : ApiController
    {
		[HttpPost]
		public async Task<HttpResponseMessage> PostFormData()
		{
			if (!Request.Content.IsMimeMultipartContent())
			{
				throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
			}

			string root = HttpContext.Current.Server.MapPath("~/App_Data");
			var provider = new MultipartFormDataStreamProvider(root);

			try
			{
				await Request.Content.ReadAsMultipartAsync(provider);

				foreach (MultipartFileData file in provider.FileData)
				{
					Trace.WriteLine(file.Headers.ContentDisposition.FileName);
					Trace.WriteLine("Server file path: " + file.LocalFileName);
				}

				var response = Request.CreateResponse(HttpStatusCode.OK);
				response.Content = new StringContent("Upload Success");

				return response;
				//return Request.CreateResponse(HttpStatusCode.OK);
			}
			catch (Exception ex) {

				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
			}

		}



    }
}
