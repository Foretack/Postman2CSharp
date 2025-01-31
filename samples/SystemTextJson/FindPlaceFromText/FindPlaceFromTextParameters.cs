using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<FindPlaceFromTextParameters>(myJsonResponse);
namespace SystemTextJson
{
    public class FindPlaceFromTextParameters : IQueryParameters
    {
        /// <summary>
        /// > Caution: Place Search requests and Place Details requests do not return the same fields. Place Search requests return a subset of the fields that are returned by Place Details requests. If the field you want is not returned by Place Search, you can use Place Search to get a place_id, then use that Place ID to make a Place Details request. For more information on the fields that are unavailable in a Place Search request, see Places API fields support
        /// 
        /// Use the fields parameter to specify a comma-separated list of place data types to return. For example: `fields=formatted_address,name,geometry`. Use a forward slash when specifying compound values. For example: `opening_hours/open_now`.
        /// 
        /// Fields are divided into three billing categories: Basic, Contact, and Atmosphere. Basic fields are billed at base rate, and incur no additional charges. Contact and Atmosphere fields are billed at a higher rate. See the [pricing sheet](https://cloud.google.com/maps-platform/pricing/sheet/) for more information. Attributions, `html_attributions`, are always returned with every call, regardless of whether the field has been requested.
        /// 
        /// **Basic**
        /// 
        /// The Basic category includes the following fields: `address_component`, `adr_address`, `business_status`, `formatted_address`, `geometry`, `icon`, `icon_mask_base_uri`, `icon_background_color`, `name`, `permanently_closed` ([deprecated](https://developers.google.com/maps/deprecations)), `photo`, `place_id`, `plus_code`, `type`, `url`, `utc_offset`, `vicinity`.
        /// 
        /// **Contact**
        /// 
        /// The Contact category includes the following fields: `formatted_phone_number`, `international_phone_number`, `opening_hours`, `website`
        /// 
        /// **Atmosphere**
        /// 
        /// The Atmosphere category includes the following fields: `price_level`, `rating`, `review`, `user_ratings_total`.
        /// 
        /// </summary>
        public string Fields { get; set; }
        
        /// <summary>
        /// (Required) The text string on which to search, for example: "restaurant" or "123 Main Street". This must be a place name, address, or category of establishments. Any other types of input can generate errors
        /// and are not guaranteed to return valid results. The Places API will return candidate matches based on this string and order the results based on their perceived relevance.
        /// 
        /// </summary>
        public string Input { get; set; }
        
        /// <summary>
        /// (Required) The type of input. This can be one of either `textquery` or `phonenumber`. Phone numbers must be in international format (prefixed by a plus sign ("+"), followed by the country code, then the phone number itself). See [E.164 ITU recommendation](https://en.wikipedia.org/wiki/E.164) for more information.
        /// 
        /// </summary>
        public string Inputtype { get; set; }
        
        /// <summary>
        /// Prefer results in a specified area, by specifying either a radius plus lat/lng, or two lat/lng pairs representing the points of a rectangle. If this parameter is not specified, the API uses IP address biasing by default.
        /// - IP bias: Instructs the API to use IP address biasing. Pass the string `ipbias` (this option has no additional parameters).
        /// - Point: A single lat/lng coordinate. Use the following format: `point:lat,lng`.
        /// - Circular: A string specifying radius in meters, plus lat/lng in decimal degrees. Use the following format: `circle:radius@lat,lng`.
        /// - Rectangular: A string specifying two lat/lng pairs in decimal degrees, representing the south/west and north/east points of a rectangle. Use the following format:`rectangle:south,west|north,east`. Note that east/west values are wrapped to the range -180, 180, and north/south values are clamped to the range -90, 90.
        /// 
        /// </summary>
        public string Locationbias { get; set; }
        
        /// <summary>
        /// The language in which to return results.
        /// 
        /// * See the [list of supported languages](https://developers.google.com/maps/faq#languagesupport). Google often updates the supported languages, so this list may not be exhaustive.
        /// * If `language` is not supplied, the API attempts to use the preferred language as specified in the `Accept-Language` header.
        /// * The API does its best to provide a street address that is readable for both the user and locals. To achieve that goal, it returns street addresses in the local language, transliterated to a script readable by the user if necessary, observing the preferred language. All other addresses are returned in the preferred language. Address components are all returned in the same language, which is chosen from the first component.
        /// * If a name is not available in the preferred language, the API uses the closest match.
        /// * The preferred language has a small influence on the set of results that the API chooses to return, and the order in which they are returned. The geocoder interprets abbreviations differently depending on language, such as the abbreviations for street types, or synonyms that may be valid in one language but not in another. For example, _utca_ and _tér_ are synonyms for street in Hungarian.
        /// </summary>
        public string Language { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "fields",
                    Fields
                },
                {
                    "input",
                    Input
                },
                {
                    "inputtype",
                    Inputtype
                },
                {
                    "locationbias",
                    Locationbias
                },
                {
                    "language",
                    Language
                }
            };
        }
    }
}