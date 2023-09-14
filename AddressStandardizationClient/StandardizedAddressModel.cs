﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressStandardizationClient
{
    [Serializable]
    public class Metro
    {
        public double distance { get; set; }
        public string line { get; set; }
        public string name { get; set; }
    }

    [Serializable]
    public class Root
    {
        public string source { get; set; }
        public string result { get; set; }
        public string postal_code { get; set; }
        public string country { get; set; }
        public string country_iso_code { get; set; }
        public string federal_district { get; set; }
        public string region_fias_id { get; set; }
        public string region_kladr_id { get; set; }
        public string region_iso_code { get; set; }
        public string region_with_type { get; set; }
        public string region_type { get; set; }
        public string region_type_full { get; set; }
        public string region { get; set; }
        public object area_fias_id { get; set; }
        public object area_kladr_id { get; set; }
        public object area_with_type { get; set; }
        public object area_type { get; set; }
        public object area_type_full { get; set; }
        public object area { get; set; }
        public object city_fias_id { get; set; }
        public object city_kladr_id { get; set; }
        public object city_with_type { get; set; }
        public object city_type { get; set; }
        public object city_type_full { get; set; }
        public object city { get; set; }
        public string city_area { get; set; }
        public object city_district_fias_id { get; set; }
        public object city_district_kladr_id { get; set; }
        public string city_district_with_type { get; set; }
        public string city_district_type { get; set; }
        public string city_district_type_full { get; set; }
        public string city_district { get; set; }
        public object settlement_fias_id { get; set; }
        public object settlement_kladr_id { get; set; }
        public object settlement_with_type { get; set; }
        public object settlement_type { get; set; }
        public object settlement_type_full { get; set; }
        public object settlement { get; set; }
        public string street_fias_id { get; set; }
        public string street_kladr_id { get; set; }
        public string street_with_type { get; set; }
        public string street_type { get; set; }
        public string street_type_full { get; set; }
        public string street { get; set; }
        public object stead_fias_id { get; set; }
        public object stead_kladr_id { get; set; }
        public object stead_cadnum { get; set; }
        public object stead_type { get; set; }
        public object stead_type_full { get; set; }
        public object stead { get; set; }
        public string house_fias_id { get; set; }
        public string house_kladr_id { get; set; }
        public string house_cadnum { get; set; }
        public string house_type { get; set; }
        public string house_type_full { get; set; }
        public string house { get; set; }
        public object block_type { get; set; }
        public object block_type_full { get; set; }
        public object block { get; set; }
        public object entrance { get; set; }
        public object floor { get; set; }
        public string flat_fias_id { get; set; }
        public string flat_cadnum { get; set; }
        public string flat_type { get; set; }
        public string flat_type_full { get; set; }
        public string flat { get; set; }
        public string flat_area { get; set; }
        public string square_meter_price { get; set; }
        public string flat_price { get; set; }
        public object postal_box { get; set; }
        public string fias_id { get; set; }
        public string fias_code { get; set; }
        public string fias_level { get; set; }
        public string fias_actuality_state { get; set; }
        public string kladr_id { get; set; }
        public string capital_marker { get; set; }
        public string okato { get; set; }
        public string oktmo { get; set; }
        public string tax_office { get; set; }
        public string tax_office_legal { get; set; }
        public string timezone { get; set; }
        public string geo_lat { get; set; }
        public string geo_lon { get; set; }
        public string beltway_hit { get; set; }
        public object beltway_distance { get; set; }
        public int qc_geo { get; set; }
        public int qc_complete { get; set; }
        public int qc_house { get; set; }
        public int qc { get; set; }
        public object unparsed_parts { get; set; }
        public List<Metro> metro { get; set; }
    }

}
