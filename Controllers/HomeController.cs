using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Provider;

namespace MomCom.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		//Throughout this whole process it's good to have this open: http://jsonviewer.stack.hu/
		//Simply feed it your API URL and see what comes back, you can use it to keep testing your 
		//URL and to see what data is availible once you have the right URL
		public ActionResult GetData()
		{
			//Make a request but don't send it yet
			//With this movie API, you put certain parameters into the URL, pay close attention to the documentation 
			//What does s=movie mean? 
			HttpWebRequest request = WebRequest.CreateHttp("http://places.demo.api.here.com/places/v1/discover/explore?in=42.921%2C-85.5944%3Br%3D45033&cat=leisure-outdoor&Accept-Language=en-US%2Cen%3Bq%3D0.8&/application/json&app_id=EVCVXo26lL5LrdGgDEK5&app_code=J9PlnJ5WvHIuxtYvVMwiYw");
			//Tell it the list of browsers we're using
			request.UserAgent = @"User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36";
			//If you need to use OAuth or Keys there will be a few extra steps right around here you go on to 
			//grab a response.
			//push the request over to the remote server 
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			//Parse the response data (this looks a lot like reading in a text file, file I/O)
			StreamReader rd = new StreamReader(response.GetResponseStream());
			//Return the data in string format 
			String data = rd.ReadToEnd();
			//This is where things change based upon whether we're using XML or Json
			//Personally I prefer JSON, but they're equivalent to each other
			JObject o = JObject.Parse(data);
			//Now we can step through the JSON data 
			//the way to approach this is to think of every tag either contains a string array or points 
			//to another list. As you try to construct this always always have the JSON viewer open
			//With the array portion you can use  the .ToList() or ToArray() methods to make a collection
			//of JTokens
			for (int i = 0; i < o["results"].Count(); i++)
			{
				ViewBag.Production += o["results"]["items"]["category"];
			}
			//ViewBag.Time = o["time"]["startPeriodName"][0];
			//ViewBag.ApiText = "Tomorrow's Temperature is " + o["data"]["temperature"][2]+" as of " +o["creationDateLocal"];
			//ViewBag.JSONData =""+ o["productionCenter"];
			////You can step through data just like an array
			//ViewBag.Temp = o["data"]["temperature"][4];
			//List<JToken> times =  o["time"].ToList();
			//List<string> temps = new List<string>();
			////https://stackoverflow.com/questions/9198426/mvc3-putting-a-newline-in-viewbag-text
			////You want the front end to care about presenting data, so we do our newlines there
			//for (int i = 0; i<o["data"]["temperature"].Count(); i++)
			//{
			//    //string timeLabel = times[i].ToString();
			//    string input =o["time"]["startPeriodName"][i]+" "+ o["time"]["tempLabel"][i] +" "+ o["data"]["temperature"][i].ToString();
			//    temps.Add(input);
			//}
			//ViewBag.AllTemps = temps;

			return View();
			
		}
	}
}