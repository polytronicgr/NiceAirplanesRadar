﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NiceAirplanesRadar.Util;

namespace NiceAirplanesRadar.Services
{

    internal class OpenSkyService : ServiceAPI
    {
        private const string cacheFile = "openSky.cache.json";
        private const string url = "https://opensky-network.org/api/states/all";

        public OpenSkyService() : base(url,cacheFile, new TimeSpan(0,1,0))
        {
        }
        
        protected override IEnumerable<Aircraft> Conversor(string data)
        {
            var jsonData = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            var lastAirplanesRaw = JsonConvert.DeserializeObject<List<string[]>>(jsonData["states"].ToString());

            var raw = lastAirplanesRaw.FirstOrDefault();

            var lastAirplanes = lastAirplanesRaw.Select(s => new Aircraft(
                                                        hexCode: s[0],
                                                        flightName: s[1], // flightname
                                                        altitude: String.IsNullOrEmpty(s[7]) ? 0 : Convert.ToDouble(s[7], CultureInfo.InvariantCulture), // 
                                                        latitude: String.IsNullOrEmpty(s[6]) ? 0 : Convert.ToDouble(s[6], CultureInfo.InvariantCulture), // 
                                                        longitude: String.IsNullOrEmpty(s[5]) ? 0 : Convert.ToDouble(s[5], CultureInfo.InvariantCulture), // 
                                                        speed: String.IsNullOrEmpty(s[9]) ? 0 : Convert.ToDouble(s[9], CultureInfo.InvariantCulture), // 
                                                        verticalSpeed: String.IsNullOrEmpty(s[11]) ? 0 : Convert.ToDouble(s[11], CultureInfo.InvariantCulture), // vertical speed
                                                        direction: String.IsNullOrEmpty(s[10]) ? 0 : Convert.ToDouble(s[10], CultureInfo.InvariantCulture), // direction
                                                        registration: s[2], // registration
                                                        isOnGround: Boolean.Parse(s[8]),//is on groud
                                                        from: String.Empty,
                                                        to: String.Empty,
                                                        model: String.Empty
                                                    )).ToList();

            return lastAirplanes;
        }
    }
}