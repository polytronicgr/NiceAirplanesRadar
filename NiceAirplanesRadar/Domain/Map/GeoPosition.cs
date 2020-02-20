﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NiceAirplanesRadar;
using NiceAirplanesRadar.Util;

namespace NiceAirplanesRadar
{
    public class GeoPosition
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string Description { get; set; }

        public GeoPosition(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
        
        public GeoPosition(string latitude, string longitude)
        {
            latitude = !String.IsNullOrEmpty(latitude) ? latitude : "0";
            longitude = !String.IsNullOrEmpty(longitude) ? longitude : "0";
            
            this.Latitude = double.Parse(latitude);
            this.Longitude = double.Parse(longitude);
        }
        
        public double Distance(GeoPosition target) {
            var distanceKilomoters = MathHelper.GetGPSDistance(this.Latitude,target.Latitude,this.Longitude, target.Longitude);
            return distanceKilomoters < 0 ? distanceKilomoters * -1 : distanceKilomoters;
        }

        public override string ToString()
        {
            return $"Lat: {this.Latitude} Lon: {this.Longitude}";
        }

    }
}
