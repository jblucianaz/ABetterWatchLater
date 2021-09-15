using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ABetterWatchLaterAPI.Controllers
{
    public class YouTubeController : ControllerBase
    {   
        public string CreateGetURL(string queryType, string id)
        {
            return Constants.YouTube.BASE_API_URL + queryType + "?part=snippet" + 
                (queryType == Constants.YouTube.VIDEOS ? "&part=contentDetails" : "") + 
                "&key=" + Constants.YouTube.API_KEY +
                "&id=" + id;
        }
    }
}